using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Hacking
{
    /// <summary>
    /// The different types of file formats. It's this that makes sure we get the right parser for the right file
    /// </summary>
    public enum FileType
    {
        Connections
    }

    public enum DataType
    {
        Connections
    }

    /// <summary>
    /// Describes the files used by IDevice to describe their properties, NOT IMPLEMENTED YET!!!
    /// </summary>
    [System.Serializable]
    public struct File
    {
        public string name;
        public string content;
        public FileType type;

        /// <summary>
        /// The initializer for the File. This reads the filedata and sets its values correspondingly. FLAWED
        /// </summary>
        /// <param name="asset">The file to load</param>
        public File(TextAsset asset)
        {
            string contents = asset.text; // Read the filedata
            string[] lines = contents.Trim().Split('*'); // Set lines with the lines in the text and convert to a list
            type = (FileType)System.Enum.Parse(typeof(FileType), lines[0]); // Set the type to the corresponding FileType
            name = lines[1]; // Set the name to corresponding string
            content = lines[2].Trim(); ; // Set the content to the last string wich wont include the data of the parser and the name
        }

        public static File? LoadFile(IDevice device, string name)
        {
            return FileDatabase.LoadFile(device, name);
        }

        public void SaveFile(File file, IDevice device)
        {
            FileDatabase.SaveFile(file, device);
        }
    }

    /// <summary>
    /// Loads files and deals with parsers
    /// </summary>
    public static class FileHelper
    {

        /// <summary>
        /// A Dictionary wich maps from an enum filetype to the correct parser
        /// </summary>
        public static Dictionary<FileType, IParser> parserMap = new Dictionary<FileType, IParser>() {
        {FileType.Connections, new ConnectionsParser()}
    };

        /// <summary>
        /// Get the corresponding parser to the right file
        /// </summary>
        /// <param name="file">The file to get the parser from</param>
        /// <returns></returns>
        public static IParser GetParser(File file)
        {
            return parserMap[file.type];
        }

        /// <summary>
        /// Parses a file from the database
        /// </summary>
        /// <param name="name">The name of the file to parse</param>
        /// <param name="device">The device this file is on</param>
        public static Data[] ParseFile(string name, IDevice device)
        {
            File file = FileDatabase.LoadFile(device, name); //Loads the file from the database
            Data[] data = GetParser(file).ParseFile(file); // Finds the parser and parses the file
            return data;
        }

        /// <summary>
        /// Parses a bunch of files from an array of names
        /// </summary>
        /// <param name="names">The names of the files to parse</param>
        /// <param name="device">The device theese files are on</param>
        public static FileData ParseFiles(string[] names, IDevice device)
        {
            List<Data> data = new List<Data>(); //The array wich the data is stored in

            foreach (string name in names)
            {
                data.AddRange(ParseFile(name, device)); //Add the currently parsed data to the array
            }

            return new FileData(data.ToArray()); // Return a new filedata with the parsed data
        }
    }

    /// <summary>
    /// Represents a database of files, stored in PlayerPrefs???
    /// The database should consist of links between device ids and file arrays. Because files are serializable 
    /// </summary>
    public class FileDatabase
    {
        public static Dictionary<IDevice, Dictionary<string, File>> files = new Dictionary<IDevice, Dictionary<string, File>>();

        /// <summary>
        /// Load a file stored on <paramref name="device"/>, with name <paramref name="name"/>
        /// </summary>
        /// <param name="device">The device that the file is linked to</param>
        /// <param name="name">The name of the file on the device</param>
        /// <returns>The loaded file, which may be null if the file does not exist. ALWAYS NULL CHECK!</returns>
        public static File LoadFile(IDevice device, string name)
        {
            return files[device][name];
        }

        /// <summary>
        /// Saves a file <paramref name="file"/> to device <paramref name="device"/>
        /// </summary>
        /// <param name="file">The file to save</param>
        /// <param name="device">The device to save the file to</param>
        /// <param name="name">The name to save the file as</param>
        public static void SaveFile(File file, IDevice device)
        {
            //Do something here
        }

        /// <summary>
        /// Adds a file to the global database on a specified IDevice. PATH SHOULD NOT BE AN ACTUAL PATH CAUSE YOU CANT DO DAT SHIT
        /// </summary>
        /// <param name="file">The file to add to the database</param>
        /// <param name="device">The device this file is located on</param>
        /// <returns>The name of the file</returns>
        public static string AddFile(TextAsset asset, IDevice device)
        {
            File file = new File(asset); //Creates a new file from path

            if (!files.ContainsKey(device))
            {
                files.Add(device, new Dictionary<string, File>()); //Adds a new dictionery in the device if it doesn't exist already
            }

            files[device].Add(file.name, file); //Adds the file to the database
            return file.name;
        }

        /// <summary>
        /// Adds an array of files to the database
        /// </summary>
        /// <param name="assets">The files to add</param>
        /// <param name="device">The device to save the files on</param>
        public static string[] AddFiles(TextAsset[] assets, IDevice device)
        {
            List<string> names = new List<string>();
            foreach (TextAsset asset in assets)
            {
                names.Add(AddFile(asset, device));
            }
            return names.ToArray();
        }
    }

    /// <summary>
    /// The object that stores all of the parsed file data through keys.
    /// NOTE that when pulling keys you allways have to check if it exits
    /// </summary>
    public class FileData
    {
        Dictionary<DataType, Data> data = new Dictionary<DataType, Data>();

        public FileData(Data[] theData)
        {
            foreach (Data data in theData)
            {
                this.data.Add(data.GetDataType(), data);
            }
        }

        /// <summary>
        /// Gets a piece of data from the dictionary with the correct key.
        /// NOTE always check if the data exists before with the DataExists method.
        /// </summary>
        /// <param name="key">The name of the key of the data</param>
        /// <returns></returns>
        public Data GetData(DataType type)
        {
            return data[type];
        }

        /// <summary>
        /// Checks if the certain data exists in the dictionary
        /// </summary>
        /// <param name="key">The name of the key of the data</param>
        /// <returns></returns>
        public bool DataExists(DataType type)
        {
            return data.ContainsKey(type);
        }
    }

    /// <summary>
    /// The basic interface for all parsers
    /// </summary>
    public interface IParser
    {
        Data[] ParseFile(File file);
    }

    /// <summary>
    /// The parser for connection files
    /// The thing is lit fam
    /// </summary>
    public class ConnectionsParser : IParser
    {
        public Data[] ParseFile(File file)
        {
            bool inConnectionBlock = false;
            List<string> connections = new List<string>();

            string[] lines = Regex.Split(file.content, "\n");
            foreach (string line in lines)
            {
                if (Regex.IsMatch(line, "END")) //If the keyword END shows up the connections list is over
                {
                    inConnectionBlock = false;
                }

                if (inConnectionBlock) // If the in a connections list we add those conections to the list
                {
                    connections.Add(line); //Add the connection to the list
                }

                if (Regex.IsMatch(line, "connections:")) //If the keyword "connections:" the connection list is begining
                {
                    inConnectionBlock = true;
                }
            }
            return new Data[1] { new ConnectionsData(connections.ToArray()) };
        }
    }

    /// <summary>
    /// The basic class for all data. Data should contain only one type of information like connections.
    /// </summary>
    public abstract class Data
    {
        private DataType type;

        /// <summary>
        /// Basic constructor for Data
        /// </summary>
        /// <param name="type"></param>
        public Data(DataType type)
        {
            this.type = type;
        }

        /// <summary>
        /// Returns the name of the type of data. This is used for saving in a FileData
        /// </summary>
        /// <returns>The name of this data</returns>
        public DataType GetDataType()
        {
            return type;
        }
    }

    /// <summary>
    /// The basic data for connections
    /// </summary>
    public class ConnectionsData : Data
    {
        public string[] connections;

        /// <summary>
        /// The base constructor wich assigns the connected devices
        /// </summary>
        /// <param name="connections"></param>
        public ConnectionsData(string[] connections) : base(DataType.Connections)
        {
            this.connections = connections;
        }
    }
}