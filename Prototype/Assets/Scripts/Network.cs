using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;


using System.Net.Sockets;

using System.Diagnostics;
using System.Runtime.InteropServices;
//public class SocketFloat : MonoBehaviour
//{

//    public float a;
//   public float b;
//  public  float c;
//};

//public struct socketTest
//{
//    public float a;
//}

public class Network : MonoBehaviour
{

    private float temp;
    private Transform player;

    public struct SocketTest
    {
        public float a;
        public float b;
        public float c;
        public float d;
    }

    //public void Main(string[] args)
    //{
    //    SocketTest abc;
    //    abc.a = 3.0f;

    //    temp = abc.a;
    //}
    
    //struct abc{
    //    public float a;
    //    float b;
    //    float c;
    //};
    //abc a;
    //socketTest adg = new socketTest(0.0f);
    //adg.SetA(2);  
    //adg.a=0.0f;
    //SocketFloat a = new SocketFloat();

    // SocketFloat ac = new SocketFloat();

    // port number 

    

    private const int portNum = 20777;
    float a = 1;
    float b, c, d;
    byte[] buffer;
    byte[] recvBuffer = new byte[1024];

    SocketTest abc;

    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Loopback, portNum);
    IPEndPoint tempEP = new IPEndPoint(IPAddress.Any, 0);
    //EndPoint rEP = (EndPoint)tempEP;
    Socket udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

    
            


    // Use this for initialization
    void Start()
    {
        //player = GameObject.FindWithTag("Player").GetComponent<

        //SokectFloat ss = (SokectFloat)ScriptableObject.CreateInstance("SokectFloat");
    }

    // Update is called once per frame
    void Update()
    {
        
        //abc.a = 3.0f;
        //abc.b = 2.0f;
        //abc.c = 1.0f;

        abc.d = 1;
        abc.a = GameObject.FindWithTag("Player").GetComponent<CapsuleCtrl>().x;
        abc.b = GameObject.FindWithTag("Player").GetComponent<CapsuleCtrl>().y;
        abc.c = GameObject.FindWithTag("Player").GetComponent<CapsuleCtrl>().z;

        buffer = StructToByte(abc);
        //buffer = System.BitConverter.GetBytes(abc);
        //System.BitConverter.GetBytes(abc);
        //buffer = (byte*)&abc;
        udpSocket.SendTo(buffer, remoteEP);
        //Marshal.StructureToPtr(this, (IntPtr)fixed_buffer, false);
    }

    void OnApplicationQuit()
    {
        abc.a = 0;
    }

    public static byte[] StructToByte(object st)
    {
        byte[] buffer = new byte[Marshal.SizeOf(st)];
        unsafe
        {
            fixed (byte* fixed_buffer = buffer)
            {
                Marshal.StructureToPtr(st, (IntPtr)fixed_buffer, false);
            }
        }
        return buffer;
    }
    
}








/////////////////////////////////////////////////////////////////////////////////


//public class InputUDP : MonoBehaviour
//{


//    // UDP packet store
//    public string lastReceivedPacket = "";
//    public string allReceivedPackets = ""; // this one has to be cleaned up from time to time

//    // start from unity3d
//    void Start()
//    {
//        // create thread for reading UDP messages

//    }

//    // Unity Update Function
//    void Update()
//    {
//        // check button "s" to abort the read-thread
//        if (Input.GetKeyDown("q"))
//            stopThread();
//    }

//    // Unity Application Quit Function
//    void OnApplicationQuit()
//    {
//        stopThread();
//    }

//    // Stop reading UDP messages
//    private void stopThread()
//    {
//        if (readThread.IsAlive)
//        {
//            readThread.Abort();
//        }
//        client.Close();
//    }

//    // receive thread function
//    private void ReceiveData()
//    {
//        client = new UdpClient(port);
//        while (true)
//        {
//            try
//            {
//                // receive bytes
//                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
//                byte[] data = client.Receive(ref anyIP);

//                // encode UTF8-coded bytes to text format
//                string text = Encoding.UTF8.GetString(data);

//                // show received message
//                print(">> " + text);

//                // store new massage as latest message
//                lastReceivedPacket = text;

//                // update received messages
//                allReceivedPackets = allReceivedPackets + text;

//            }
//            catch (Exception err)
//            {
//                print(err.ToString());
//            }
//        }
//    }

//    // return the latest message
//    public string getLatestPacket()
//    {
//        allReceivedPackets = "";
//        return lastReceivedPacket;
//    }
//}