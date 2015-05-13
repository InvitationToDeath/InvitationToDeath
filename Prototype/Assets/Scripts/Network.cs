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

    private Vector3 position;
    private Vector3 standardX = new Vector3(1, 0, 0);
    private Vector3 standardY = new Vector3(0, 1, 0);
    private Vector3 standardZ = new Vector3(0, 0, 1);

    public struct Motion
    {
        public float start;
        public float pitching;
        public float rolling;
        public float yawing;
        public float heave;
        public float rotationX;
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
    //float a = 1;
    //float b, c, d, e, f, g;
    byte[] buffer;
    byte[] recvBuffer = new byte[1024];

    Motion motion;

    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Loopback, portNum);
    IPEndPoint tempEP = new IPEndPoint(IPAddress.Any, 0);
    //EndPoint rEP = (EndPoint)tempEP;
    Socket udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

    
            


    // Use this for initialization
    void Start()
    {
        //player = GameObject.FindWithTag("Player").GetComponent<

        //SokectFloat ss = (SokectFloat)ScriptableObject.CreateInstance("SokectFloat");
        
        //게임 시작 시 start값 1로 설정.
        motion.start = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //position = GameObject.FindWithTag("Player").transform.up;

        //abc.a = 3.0f;
        //abc.b = 2.0f;
        //abc.c = 1.0f;
        //motion.start = 1;

        motion.start = Time.timeScale;

        /*
        //기존의 이전 rotation 값과의 차이로 전송 방법.
        motion.pitching = GameObject.FindWithTag("Player").GetComponent<CapsuleCtrl>().x;
        motion.rolling = GameObject.FindWithTag("Player").GetComponent<CapsuleCtrl>().y;
        motion.yawing = GameObject.FindWithTag("Player").GetComponent<CapsuleCtrl>().z;
        */

        //스크립트로 각도를 구하여 전송 방법. (yawing은 이전 rotation 값의 차이로 쓰는 게 좋을 듯 함.)
        motion.pitching = -Vector3.Angle(standardY, GameObject.FindWithTag("Player").transform.up) + 90.0f;
        motion.rolling = -Vector3.Angle(standardX, -GameObject.FindWithTag("Player").transform.forward) + 90.0f;
        motion.yawing = -Vector3.Angle(standardX, GameObject.FindWithTag("Player").transform.up) + 90.0f;

        motion.rotationX = Vector3.Angle(standardY, GameObject.FindWithTag("Player").transform.up);
        motion.heave = 0; //GameObject.FindWithTag("Player").GetComponent<CapsuleCtrl>().heaveVelocity;
       
        buffer = StructToByte(motion);
        //buffer = System.BitConverter.GetBytes(abc);
        //System.BitConverter.GetBytes(abc);
        //buffer = (byte*)&abc;
        udpSocket.SendTo(buffer, remoteEP);
        //Marshal.StructureToPtr(this, (IntPtr)fixed_buffer, false);
        print(motion.heave);




       

        buffer = StructToByte(motion);
        //buffer = System.BitConverter.GetBytes(abc);
        //System.BitConverter.GetBytes(abc);
        //buffer = (byte*)&abc;
        udpSocket.SendTo(buffer, remoteEP);
        //Marshal.StructureToPtr(this, (IntPtr)fixed_buffer, false);
    }

    void OnApplicationQuit()
    {
        motion.start = 0;
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