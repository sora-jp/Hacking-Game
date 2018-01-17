using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* NOTE: Use IDevice in GetComponent when you want a device (may be hackable)
         and use IHackable when you want a device that is hackable */

/// <summary>
/// A generic device, unknown type
/// </summary>
public interface IDevice
{
    void Activate();
    void Deacticate();
    bool GetStatus();
}

/// <summary>
/// A generic hackable device, unknown type
/// </summary>
public interface IHackable : IDevice
{
    void Hack();
}

/// <summary>
/// A device that is connected to another one
/// </summary>
public interface IConnectedDevice : IDevice
{
    IDevice GetConnectedDevice();
}

/// <summary>
/// Base implementation of IDevice
/// </summary>
[System.Serializable]
public abstract class Device : MonoBehaviour, IDevice
{
    bool status; // Basically this varible changes state whether or not the device is active

    /// <summary>
    /// Activates the device
    /// </summary>
    public virtual void Activate()
    {
        status = true;
    }

    /// <summary>
    /// Deactivates the device
    /// </summary>
    public virtual void Deacticate()
    {
        status = false;
    }

    /// <summary>
    /// Returns the status of the device ( whether it is active or not )
    /// </summary>
    /// <returns>The status of the device</returns>
    public virtual bool GetStatus()
    {
        return status;
    }
}

/// <summary>
/// Base implementation of IHackable
/// </summary>
public abstract class HackableDevice : Device, IHackable
{
    /// <summary>
    /// Hacks the device
    /// </summary>
    public abstract void Hack();
}

/// <summary>
/// Base implementation of IConnectedDevice
/// </summary>
public abstract class ConnectedDevice : Device, IConnectedDevice
{
    public Device connectedDevice; // You HAVE to assign this!

    /// <summary>
    /// Returns the connected device <para></para>
    /// NOTE: SHOULD NEVER BE USED WITHOUT NULL-CHECK!
    /// </summary>
    /// <returns>The connected device</returns>
    public virtual IDevice GetConnectedDevice()
    {
        return connectedDevice;
    }
}

/// <summary>
/// Describes the files used by IDevice to describe their properties, NOT IMPLEMENTED YET!!!
/// </summary>
[System.Serializable]
public struct File
{
    public string name;
    public string contents;

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

/// <summary>
/// Helps with ids and objects
/// </summary>
public static class DeviceHelper
{
    public static IDevice DeviceFromId(string id)
    {
        return null; //FIXME
    }

    public static string IdFromDevice(IDevice device)
    {
        return ""; //FIXME
    }
}