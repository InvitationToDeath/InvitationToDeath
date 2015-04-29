using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour {
	//총알의 파괴력.
	public int damage = 20;
	//총알 발사 속도.
	public float speed = 5.0f;


	//발사 원점.
	public Vector3 firePos = Vector3.zero;

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("space"))
        {
            //로컬좌표기준 힘을 가함.
            rigidbody.AddForce(transform.up * speed);
            //rigidbody.AddRelativeForce(Vector3.forward * speed);

            //전역좌표기준 힘을 가함.
            //rigidbody.AddForce (Vector3.forward * speed);

            //총알이 생성된 위치가 FirePos 위치와 같음.
            firePos = transform.position;
        }
        if (Input.GetKey("b"))
        {
            //로컬좌표기준 힘을 가함.
            rigidbody.AddForce(transform.up * (-speed));
            //rigidbody.AddRelativeForce(Vector3.forward * speed);

            //전역좌표기준 힘을 가함.
            //rigidbody.AddForce (Vector3.forward * speed);

            //총알이 생성된 위치가 FirePos 위치와 같음.
            firePos = transform.position;
        }
	}
}
