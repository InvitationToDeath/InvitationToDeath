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
    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 100, 25), "Restart"))
        {
            lastSecond = Time.time;
            Application.LoadLevel("jeongsoover");
        }
        //nowSecond = float.Parse(Time.time.ToString());
        //nowSecond -= lastSecond;

        nowSecond += Time.deltaTime;


        GUI.Box(new Rect(20, 50, 200, 25), nowSecond.ToString());
        //GUI.Box(new Rect(20, 50, 200, 25), Time.time.ToString());
        
    }

}
