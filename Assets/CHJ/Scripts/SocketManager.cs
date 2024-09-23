using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine;

[Serializable]
public class GestureData
{
    public bool v_gesture;
    public bool palm_visible;
    public bool v_sustained;
    public bool palm_sustained;

    public override string ToString()
    {
        if (v_sustained)
            return "V자 포즈";
        else if (palm_sustained)
            return "손바닥 포즈";
        else
            return "";
    }
}

public class SocketManager : MonoBehaviour
{
    public int port = 5005; // Port to listen on
    private UdpClient udpClient;
    IPEndPoint remoteEndPoint;
    private bool isListening = true;
    public static SocketManager instance;

    public GestureData gesture;
    public TMP_Text log;
    //string logText = "";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartListening();
    }

    void Update()
    {
        //log.text = logText;
    }
    void OnApplicationQuit()
    {
        StopListening();
    }

    private void StartListening()
    {
        try
        {
            udpClient = new UdpClient(port);
            udpClient.BeginReceive(ReceiveCallback, null);
            remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
            Debug.Log($"Listening for UDP packets on port {port}.");
            //logText = $"Listening for UDP packets on port {port}.";
        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception: {ex.Message}");
            //logText = ex.ToString();
        }
    }

    private void StopListening()
    {
        isListening = false;
        udpClient?.Close();
    }

    private void ReceiveCallback(IAsyncResult result)
    {
        if (!isListening)
            return;

        try
        {
            byte[] receivedData = udpClient.EndReceive(result, ref remoteEndPoint);
            string message = Encoding.UTF8.GetString(receivedData);
            gesture = JsonUtility.FromJson<GestureData>(message);
            Debug.Log($"Received message: {message} from {remoteEndPoint}");
            //logText = $"Received message: {message} from {remoteEndPoint}";

            // Continue listening for more data
            udpClient.BeginReceive(ReceiveCallback, null);
        }
        catch (Exception ex)
        {
            Debug.LogError($"ReceiveCallback Exception: {ex.Message}");
            //logText = ex.ToString();
        }
    }
}