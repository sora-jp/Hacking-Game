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
    IDevice GetConnectedDevice();
}

/// <summary>
/// A generic hackable device, unknown type
/// </summary>
public interface IHackable : IDevice
{
    void Hack();
}

/// <summary>
/// Base implementation of IDevice
/// </summary>
public abstract class Device : IDevice
{
    bool status;
    IDevice connectedDevice; // You don't have to assign this, as long as you make sure that you never use GetConnectedDevice on it!

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

    /// <summary>
    /// Returns the connected device <para></para>
    /// NOTE: SHOULD NEVER BE USED WITHOUT NULL-CHECK!
    /// </summary>
    /// <returns></returns>
    public abstract IDevice GetConnectedDevice();
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