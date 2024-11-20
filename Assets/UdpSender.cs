using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class UdpSender : MonoBehaviour
{
    public int DestinationPort = 25000;
    public string DestinationIP = "127.0.0.1";
    public Payload payload;
    public GameObject player;
    private float timeout = - 0.5f;

    UdpClient udp;
    IPEndPoint localEP;

    public void SendUDPMessage(Payload obj)
    {

        byte[] bytes = ObjectToByteArray(obj);
        SendUDPBytes(bytes);
    }

    public void Close()
    {
        CloseUDP();
    }

    private byte[] ObjectToByteArray<T>(T obj)
    {
        if (obj != null)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        return null;
    }

    private void SendUDPBytes(byte[] bytes)
    {
        if (udp == null)
        {
            udp = new UdpClient();
            localEP = new IPEndPoint(IPAddress.Any, 0);
            udp.Client.Bind(localEP);
        }

        try
        {
            IPEndPoint destEP = new IPEndPoint(IPAddress.Parse(DestinationIP), DestinationPort);
            udp.Send(bytes, bytes.Length, destEP);

        }
        catch (SocketException e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    void OnDisable()
    {
        CloseUDP();
    }

    private void CloseUDP()
    {
        if (udp != null)
        {
            udp.Close();
            udp = null;
        }
    }

    private void Update()
    {
        if (Time.time > timeout)
        {
            Payload payloadData = new Payload
            {
                type = "playerPosition"
            };
            payloadData.SetPosition(player.transform.position);
            SendUDPMessage(payloadData);

            timeout = Time.time + 0.5f;
        }
        
    }

}
