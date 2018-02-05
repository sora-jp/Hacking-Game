using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* NOTE: Use IDevice in GetComponent when you want a device (may be hackable)
         and use IHackable when you want a device that is hackable */

namespace Hacking
{
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
        void Hack(Player player);
        bool CanBeHacked(bool camera);
        bool HasBeenHacked();
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
        public TextAsset[] assets; // The paths to the files. This is edited in the editor
        string[] fileNames; //The names of the files

        public string id;
        public string parent;
        public string[] children;

        private void Awake()
        {
            DeviceHelper.RegisterDevice(id, this);

            fileNames = FileDatabase.AddFiles(assets, this); //Add the files to the database
            fileData = FileHelper.ParseFiles(fileNames, this); //Parse he files and save them in the filedata
            var data = ((ConnectionsData)fileData.GetData(DataType.Connections));
            if (data == null) return;

            foreach (string connection in data.connections)
            {
                print(connection);
            }
        }

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
        public abstract void Hack(Player player);
        public virtual bool CanBeHacked(bool camera)
        {
            return true;
        }
        public virtual bool HasBeenHacked()
        {
            return true;
        }
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
        static Dictionary<string, IDevice> m_DeviceFromID;
        static Dictionary<IDevice, string> m_IDFromDevice;

        public static void RegisterDevice(string id, IDevice device)
        {
            if (m_DeviceFromID == null) m_DeviceFromID = new Dictionary<string, IDevice>();
            if (m_IDFromDevice == null) m_IDFromDevice = new Dictionary<IDevice, string>();

            m_DeviceFromID.Add(id, device);
            m_IDFromDevice.Add(device, id);
        }

        public static IDevice DeviceFromID(string id)
        {
            if (m_DeviceFromID == null || !m_DeviceFromID.ContainsKey(id)) return null;
            return m_DeviceFromID[id];
        }

        public static string IDFromDevice(IDevice device)
        {
            if (m_IDFromDevice == null || !m_IDFromDevice.ContainsKey(device)) return null;
            return m_IDFromDevice[device];
        }
    }
}