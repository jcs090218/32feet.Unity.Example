/**
 * $File: Test_Bluetooth.cs $
 * $Date: 2021-03-20 $
 * $Revision: $
 * $Creator: Jen-Chieh Shen $
 * $Notice: See LICENSE.txt for modification and distribution information 
 *	                 Copyright © 2021 by Shen, Jen-Chieh $
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InTheHand.Net;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Bluetooth.Sdp;
using InTheHand.Net.Bluetooth.AttributeIds;
using System;
using System.Text;
using System.IO;
using System.Net.Sockets;

/// <summary>
/// Test for 32feet library.
/// </summary>
public class Test_Bluetooth
    : MonoBehaviour
{
    /* Variables */

    private BluetoothDeviceInfo mDevice = null;

    /* Setter & Getter */

    /* Functions */

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Test();
    }

    private void Test()
    {
        var cli = new BluetoothClient();
        IReadOnlyCollection<BluetoothDeviceInfo> peers = cli.DiscoverDevices();

        print("Peers Length: " + peers.Count);

        if (peers.Count == 0)
        {
            Debug.Log("No device detected");
            return;
        }

        foreach (var deviceInfo in peers)
        {
            print("DN: " + deviceInfo.DeviceName);

            if (deviceInfo.DeviceName == "raspberrypi")
                mDevice = deviceInfo;
        }

        if (mDevice != null)
            print("Address: " + mDevice.DeviceAddress);

        cli.Connect(mDevice.DeviceAddress, BluetoothService.SerialPort);

        NetworkStream stream = cli.GetStream();

        string msg = "Hello World!~";
        byte[] data = Encoding.ASCII.GetBytes(msg);
        stream.Write(data, 0, data.Length);
    }
}
