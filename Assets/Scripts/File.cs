using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The different types of file formats. It's this that makes sure we get the right parser for the right file
/// </summary>
public enum FileType {
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
    /// <param name="path">The path of the file in text</param>
    public File(string path)
    {
        string contents = FileHelper.LoadFileFromComputer(path); // Read the filedata
        string[] lines = contents.Remove(' ').Split('*'); // Set lines with the lines in the text and convert to a list
        type = (FileType) System.Enum.Parse(typeof(FileType), lines[0]); // Set the type to the corresponding FileType
        name = lines[1]; // Set the name to corresponding string
        content = lines[2]; // Set the content to the last string wich wont include the data of the parser and the name
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
public static class FileHelper {

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
    /// Reads a file from the computer. Make sure when using this method to have a correct path
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string LoadFileFromComputer(string path)
    {
        return System.IO.File.ReadAllText(path); // FLAWED
    }

}

/// <summary>
/// Represents a database of files, stored in PlayerPrefs???
/// The database should consist of links between device ids and file arrays. Because files are serializable 
/// </summary>
public class FileDatabase
{
    public static Dictionary<IDevice, Dictionary<string, File>> files;

    /// <summary>
    /// Load a file stored on <paramref name="device"/>, with name <paramref name="name"/>
    /// </summary>
    /// <param name="device">The device that the file is linked to</param>
    /// <param name="name">The name of the file on the device</param>
    /// <returns>The loaded file, which may be null if the file does not exist. ALWAYS NULL CHECK!</returns>
    public static File? LoadFile(IDevice device, string name)
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
    /// <param name="path">The path of the file to add to the database</param>
    /// <param name="device">The device this file is located on</param>
    public static void AddFile(string path, IDevice device)
    {
        File file = new File(path); //Creates a new file from path
        files[device][file.name] = file; // Adds or creates a new key with the file. FLAWED. Actually throws an error when key doesn't exist!!!
    }
}

/// <summary>
/// The object that stores all of the parsed file data through keys.
/// NOTE that when pulling keys you allways have to check if it exits
/// </summary>
public class FileData
{
    Dictionary<string, IData> data;

    /// <summary>
    /// Gets a piece of data from the dictionary with the correct key.
    /// NOTE always check if the data exists before with the DataExists method.
    /// </summary>
    /// <param name="key">The name of the key of the data</param>
    /// <returns></returns>
    public IData GetData(string key)
    {
        return data[key];
    }

    /// <summary>
    /// Checks if the certain data exists in the dictionary
    /// </summary>
    /// <param name="key">The name of the key of the data</param>
    /// <returns></returns>
    public bool DataExists(string key)
    {
        return data.ContainsKey(key);
    }
}

/// <summary>
/// The basic interface for all parsers
/// </summary>
public interface IParser
{
    FileData ParseFile(File file);
}

/// <summary>
/// The thing is lit fam
/// </summary>
public class ConnectionsParser : IParser
{
    public FileData ParseFile(File file)
    {
        throw new System.NotImplementedException();
    }
}

/// <summary>
/// The basic empty interface for all data. Data should contain only one type of information like connections. FLAWED
/// </summary>
public interface IData
{
    //IData is an interface, which means it can't contain variables by itself. Consider using System.Object instead
}

/// <summary>
/// The basic data for connections
/// </summary>
public class ConnectionsData : IData
{
    
}
