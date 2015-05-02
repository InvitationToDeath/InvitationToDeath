using UnityEngine;
using System.Collections;

public class CapsuleCtrl : MonoBehaviour {

	// Use this for initialization
    private Transform tr;

    private Transform leftWing;
    private Transform rightWing;
    //private Transform prevTr;

    public float x, y, z;
    public float prevX, prevY, prevZ;

    public float delay = 0.1f;
    public float nextSave = 0.0f;

    //객차 액셀, 브레이크 속도.
    public float speed = 15.0f;
    

    //void Start () {
        
    //}

    void Awake()
    {
        tr = GetComponent<Transform>();
        rightWing = GameObject.FindGameObjectWithTag("RIGHTWING").transform;
        leftWing = GameObject.FindGameObjectWithTag("LEFTWING").transform;
        //prevTr = tr;
    }
	
	// Update is called once per frame
	void Update () {
        //rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;


        if (Input.GetKey("space"))
        {
            //로컬좌표기준 힘을 가함.
            rigidbody.AddForce(transform.up * speed);
            //rigidbody.AddRelativeForce(Vector3.forward * speed);

            //전역좌표기준 힘을 가함.
            //rigidbody.AddForce (Vector3.forward * speed);
        }
        else if (Input.GetKey("b"))
        {
            //로컬좌표기준 힘을 가함.
            rigidbody.AddForce(transform.up * (-speed));
            //rigidbody.AddRelativeForce(Vector3.forward * speed);

            //전역좌표기준 힘을 가함.
            //rigidbody.AddForce (Vector3.forward * speed);
        }
        else if (Input.GetKey("a"))
        {
            tr.Rotate(new Vector3(0, 1, 0));
        }
        else if (Input.GetKey("d"))
        {
            tr.Rotate(new Vector3(0, -1, 0));
        }


        //급격한 각도 변화 제한.
        if (30 <= tr.eulerAngles.x - prevX || tr.eulerAngles.x - prevX <= -30)
            ;
        else
            x = tr.eulerAngles.x - prevX;

        if (30 <= tr.eulerAngles.y - prevY || tr.eulerAngles.y - prevY <= -30)
            ;
        else
            y = tr.eulerAngles.y - prevY;

        if (30 <= tr.eulerAngles.z - prevZ || tr.eulerAngles.z - prevZ <= -30)
            ;
        else
            z = tr.eulerAngles.z - prevZ;


        

        /*
        //기울지는않지만 너무 자주 Rotate하는 바람에 느려짐.
        //만약 왼쪽으로 기울었다면
        if (leftWing.position.y < rightWing.position.y)
        {
            tr.Rotate(new Vector3(0, -0.1f, 0));
        }
        ////오른쪽으로 기울었다면
        else if (rightWing.position.y < leftWing.position.y)
        {
            tr.Rotate(new Vector3(0, 0.1f, 0));
        }
        */

        //만약 왼쪽으로 기울었다면
        if ( 0.1f <= (rightWing.position.y - leftWing.position.y) )
        {
            tr.Rotate(new Vector3(0, -0.6f, 0));
        }
        ////오른쪽으로 기울었다면
        else if ( 0.1f <= (leftWing.position.y - rightWing.position.y) )
        {
            tr.Rotate(new Vector3(0, 0.6f, 0));
        }


        Debug.Log(rightWing.position.y.ToString() + "\t" + leftWing.position.y.ToString());

        //급격한 각도 제한 X
        //x = (tr.eulerAngles.x - prevX) % 180;
        //y = (tr.eulerAngles.y - prevY) % 180;
        //z = (tr.eulerAngles.z - prevZ) % 180;


        //Debug.Log("tr.eulerAngles.x : " + tr.eulerAngles.x + "\tprevTr.eulerAngles.x : " + prevTr.eulerAngles.x);
        //Debug.Log("tr.eulerAngles.x : " + tr.eulerAngles.x + "\tprevX : " + prevX);



        //Debug.Log("x : " + x + "\ty : " + y + "\tz : " + z);

        //Debug.Log(Time.time.ToString());
        //if (Time.time / 0.2 == 0)
        //{
        //    //prevTr = tr;
        //    prevX = tr.eulerAngles.x;
        //    prevY = tr.eulerAngles.y;
        //    prevZ = tr.eulerAngles.z;

        //}

        
	}

    //Trigger 구간 충돌 체크.
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "ADDFORCE")
        {
            tr.rigidbody.AddForce(tr.transform.up * 340);
            //Destroy(coll.gameObject);

        }
    }

    void LateUpdate()
    {
        //delay만큼 시간이 흐를 때 마다 변화된 각도 계산, 출력.
        if (Time.time > nextSave)
        {
            nextSave = Time.time + delay;

            prevX = tr.eulerAngles.x;
            prevY = tr.eulerAngles.y;
            prevZ = tr.eulerAngles.z;



            //Debug.Log("x : " + x + "\ty : " + y + "\tz : " + z);
        }
    }
}
