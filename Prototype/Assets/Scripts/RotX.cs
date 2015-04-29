using UnityEngine;
using System.Collections;

public class RotX : MonoBehaviour {

    private float rotSpeed = 150.0f;
    private Transform tr;

	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        tr.Rotate(Vector3.left * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse Y"));
	}
}
