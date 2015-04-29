using UnityEngine;
using System.Collections;

public class BulletFire : MonoBehaviour {
    //총알의 생명 시간.
    private float LIFETime = 2.0f;
	void Start () {
	    //총알을 Forward 방향으로 힘을 가함.
        rigidbody.AddRelativeForce(Vector3.forward * 1500.0f);
        //일정 시간 지난 후 제거.
        Destroy(gameObject, LIFETime);
	}
}
