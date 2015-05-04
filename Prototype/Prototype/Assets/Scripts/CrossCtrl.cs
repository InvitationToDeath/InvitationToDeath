using UnityEngine;
using System.Collections;

public class CrossCtrl : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        if (0 < Input.GetAxis("Horizontal"))
        {
            if (this.tag == "LEFTCROSS")
            {
                this.collider.enabled = false;
            }
            else if (this.tag == "RIGHTCROSS")
            {
                this.collider.enabled = true;
            }
            //GameObject.FindGameObjectWithTag("LEFTCROSS").collider.enabled = false;
            //GameObject.FindGameObjectWithTag("RIGHTCROSS").collider.enabled = true;
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            if (this.tag == "LEFTCROSS")
            {
                this.collider.enabled = true;
            }
            else if(this.tag == "RIGHTCROSS")
            {
                this.collider.enabled = false;
            }
            //GameObject.FindGameObjectWithTag("LEFTCROSS").collider.enabled = true;
            //GameObject.FindGameObjectWithTag("RIGHTCROSS").collider.enabled = false;
        }
        //Debug.Log(Input.GetAxis("Horizontal").ToString());
    
        
    }
}
