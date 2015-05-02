using UnityEngine;
using System.Collections;

public class CameraRot : MonoBehaviour {

    private Transform tr;
    private float rotSpeed = 150.0f;

    //Bullet 프리팹 할당.
    public GameObject bullet;
    //총알 발사 위치.
    public Transform firePos;
    
	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        //rotSpeed만큼의 속도로 회전
        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
        //tr.Rotate(Vector3.left * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse Y"));

        //정규화벡터 사용 시도.
        //Vector3 rotateDir = (Vector3.up * Input.GetAxis("Mouse X")) + (Vector3.left * Input.GetAxis("Mouse Y"));
        //tr.Rotate(rotateDir.normalized * Time.deltaTime * rotSpeed, Space.Self);

        if (Input.GetMouseButtonDown(0))
        {
            //총알 발사.
            Fire();
        }
	}

    void Fire()
    {
        //총알 생성 루틴은 코루틴을 활용.
        StartCoroutine(this.CreateBullet());
    }

    //총알 생성 코루틴 함수.
    IEnumerator CreateBullet()
    {
        GameObject.Instantiate(bullet, firePos.position, firePos.rotation);
        yield return null;
    }
}
