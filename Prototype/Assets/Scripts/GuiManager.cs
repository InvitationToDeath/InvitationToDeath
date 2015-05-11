using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour {
    public GameObject ovrCamera;
    public GameObject mainCamera;

    private float lastSecond;
    private float nowSecond;

    void Awake()
    {
        ovrCamera = GameObject.FindGameObjectWithTag("OVRCAMERA");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {

        //Debug.Log("TimeScale : " + Time.timeScale);
        //Time.timeScale 값이 0일 때 일시정지, 1일 때 정상 게임 재생.
        if(Input.GetKey("q"))
        {
            //일시정지
            Time.timeScale = 0;
        }
        else if(Input.GetKey("w"))
        {
            //재시작.
            Time.timeScale = 1;
        }
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 100, 25), "Restart"))
        {
            lastSecond = Time.time;
            Application.LoadLevel("Rail-Cross");
        }
        //nowSecond = float.Parse(Time.time.ToString());
        //nowSecond -= lastSecond;

        nowSecond += Time.deltaTime;


        GUI.Box(new Rect(20, 50, 200, 25), nowSecond.ToString());
        //GUI.Box(new Rect(20, 50, 200, 25), Time.time.ToString());
        
    }

}
