using UnityEngine;
using System.Collections;


public class KeyInput : MonoBehaviour {
   
   
    void Update()
    {
        string text = Input.inputString;
        // 매 프레임마다 눌렸는지 검사해서 리스트에 누른 키를 추가합니다. 
       
        Debug.Log(text);
    } 
}
