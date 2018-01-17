using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Describes the files used by IDevice to describe their properties, NOT IMPLEMENTED YET!!!
/// </summary>
[System.Serializable]
public struct File
{
    public string name;
    public string contents;

    public File(string path)
    {

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
/// Loads files
/// </summary>
public static class FileHelper {

    public static string LoadFileFromComputer(string path)
    {
        return System.IO.File.ReadAllText(path);
    }

}

/// <summary>
/// Represents a database of files, stored in PlayerPrefs???
/// The database should consist of links between device ids and file arrays. Because files are serializable 
/// </summary>
public class FileDatabase
{
    /// <summary>
    /// Load a file stored on <paramref name="device"/>, with name <paramref name="name"/>
    /// </summary>
    /// <param name="device">The device that the file is linked to</param>
    /// <param name="name">The name of the file on the device</param>
    /// <returns>The loaded file, which may be null if the file does not exist. ALWAYS NULL CHECK!</returns>
    public static File? LoadFile(IDevice device, string name)
    {
        return null; //FIXME
    }

    /// <summary>
    /// Saves a file <paramref name="file"/> to device <paramref name="device"/>
    /// </summary>
    /// <param name="file">The file to save</param>
    /// <param name="device">The device to save the file to</param>
    public static void SaveFile(File file, IDevice device)
    {
        //TODO: Make this actually save the file
    }
}