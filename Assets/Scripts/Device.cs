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
    FileData fileData; // The data of the files connected to this object
    public string[] filePaths; // The paths to the files. This is edited in the editor

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