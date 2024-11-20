using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UdpReceiver : MonoBehaviour
{
    private UdpClient udp;
    private IPEndPoint remoteEndPoint;
    private IPEndPoint sourceEP;
    public delegate void UDPMessageReceive(string message);
    private UDPMessageReceive OnMessageReceive;
    private int ListenPort = 13000;
    public Squeleton squeleton;

    private void Start()
    {
        Listen((string message) =>
        {
            Debug.Log(message);
        });
    }

    public bool Listen(UDPMessageReceive handler)
    {
        if (udp != null)
        {
            Debug.LogWarning("Socket already initialized! Close it first.");
            return false;
        }

        try
        {
            // Local End-Point
            remoteEndPoint = new IPEndPoint(IPAddress.Any, ListenPort);
            sourceEP = new IPEndPoint(IPAddress.Any, 0);

            // Create the listener
            udp = new UdpClient();
            udp.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udp.ExclusiveAddressUse = false;
            udp.Client.Bind(remoteEndPoint);

            Debug.Log("Server listening on port: " + ListenPort);

            OnMessageReceive = handler;
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("Error creating UDP listener on port: " + ListenPort + ": " + ex.Message);
            CloseUDP();
            return false;
        }
    }

    private void Update()
    {
        ReceiveUDP();
    }

    private void ReceiveUDP()
    {
        if (udp == null) { return; }

        while (udp.Available > 0)
        {
            byte[] data = udp.Receive(ref sourceEP);

            try
            {
                Payload receivedPayload = FromByteArray<Payload>(data);
                if (receivedPayload.type == "squeletonMotion")
                {
                    Debug.Log(receivedPayload.type);
                    squeleton.Moove(receivedPayload.GetPosition());
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Error receiving UDP message: " + ex.Message);
            }
        }
    }

    private void CloseUDP()
    {
        if (udp != null)
        {
            udp.Close();
            udp = null;
        }
        OnMessageReceive = null;
    }

    void OnDisable()
    {
        CloseUDP();
    }

    public T FromByteArray<T>(byte[] data)
    {
        if (data == null)
            return default(T);
        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream(data))
        {
            object obj = bf.Deserialize(ms);
            return (T)obj;
        }
    }

}