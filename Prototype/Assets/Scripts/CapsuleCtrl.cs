﻿using UnityEngine;
using System.Collections;

public class CapsuleCtrl : MonoBehaviour {


    //단위 벡터.
    private Vector3 standardX = new Vector3(1, 0, 0);
    private Vector3 standardY = new Vector3(0, 1, 0);
    private Vector3 standardZ = new Vector3(0, 0, 1);

    //캡슐의 위치를 저장할 변수.
    private Transform tr;
    //delay (0.1초)전의 위치를 저장할 변수.
    private Vector3 prevTr;

    //이전 회전각도(스크립트로 구한)값을 저장할 변수.
    private float prevPitch;
    private float prevYaw;
    private float prevRoll;

    //z축 기준 양의 방향으로 가고있는지 음의 방향으로 가고있는지 판단하기 위한 변수.
    public bool isForwardDirection;


    //두 포지션 사이의 거리를 저장할 변수.
    private float dist;
    private float distForYAxis;

    //균형을 잡기 위한 양 날개.
    private Transform leftWing;
    private Transform rightWing;

    //지속적인 Addforce bool 변수.
    public bool addForce;
    public float addForceSpeed = 2200.0f;

    private int count = 0;

    //이동 방향과 객차의 forward 방향 벡터 사이의 각도를 저장할 변수.
    private float betweenAngle;
    //히브 값(y축 포지션 값만을 통한 속도 값))을 저장할 변수.
    public float heaveVelocity;


    
    //
    public float changeValueX, changeValueY, changeValueZ;
    //이전의 x,y,z Rotation값을 저장할 변수.
    public float prevX, prevY, prevZ;

    public float delay = 0.25f;
    private float nextSave = 0.0f;

    //객차 액셀, 브레이크 속도.
    private float speed = 2200.0f;
    public bool rotatingRailLeft = false;
    public bool rotatingRailRight = false;
    
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
            rigidbody.AddForce(transform.up * speed * Time.deltaTime);
            //rigidbody.AddRelativeForce(Vector3.forward * speed);

            //전역좌표기준 힘을 가함.
            //rigidbody.AddForce (Vector3.forward * speed);
        }
        else if (Input.GetKey("b"))
        {
            //로컬좌표기준 힘을 가함.
            rigidbody.AddForce(transform.up * (-speed) * Time.deltaTime);
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

        //지속적인 힘 가하는 코드.
        if(true == addForce)
            rigidbody.AddForce(transform.up * addForceSpeed * Time.deltaTime); //시간의 개념 도입 위해 Time.deltaTime 곱해줌.


        //90도 이상이면 뒤쪽으로 이동 중인 것.
        float forwardBack = Vector3.Angle(standardZ, GameObject.FindWithTag("Player").transform.up);
        //Debug.Log("forwardBack" + forwardBack);

        if (90.0f < forwardBack)
            isForwardDirection = false;
        else if (forwardBack < 90.0f)
            isForwardDirection = true;


        //각도 변화 계산하여 대입.(스크립트로 구한 각도)
        changeValueX = (-Vector3.Angle(standardY, GameObject.FindWithTag("Player").transform.up) + 90.0f) - prevPitch;

        //z축 기준 양의 방향으로 이동 중이 아니면 양/음의 값을 반전
        if (true == isForwardDirection)
        {
            changeValueY = (-Vector3.Angle(standardX, -GameObject.FindWithTag("Player").transform.forward) + 90.0f) - prevYaw;
            changeValueZ = (Vector3.Angle(standardZ, GameObject.FindWithTag("Player").transform.right) - 90.0f) - prevRoll;
        }
        else
        {
            changeValueY = -(-Vector3.Angle(standardX, -GameObject.FindWithTag("Player").transform.forward) + 90.0f) - prevYaw;
            changeValueZ = -(Vector3.Angle(standardZ, GameObject.FindWithTag("Player").transform.right) - 90.0f) - prevRoll;
        }


        ////각도 변화 계산하여 대입.
        //if (rotatingRailLeft == false)
        //{
        //    //급격한 각도 변화 제한.
        //    if (30 <= tr.eulerAngles.x - prevX || tr.eulerAngles.x - prevX <= -30)
        //        ;
        //    else
        //        changeValueX = tr.eulerAngles.x - prevX;

        //    if (30 <= tr.eulerAngles.y - prevY || tr.eulerAngles.y - prevY <= -30)
        //        ;
        //    else
        //        changeValueY = tr.eulerAngles.y - prevY;

        //    if (30 <= tr.eulerAngles.z - prevZ || tr.eulerAngles.z - prevZ <= -30)
        //        ;
        //    else
        //        changeValueZ = tr.eulerAngles.z - prevZ;
        //}
        
        //각도 변화 계산하여 대입.
        if (rotatingRailLeft == false)
        {
            //급격한 각도 변화 제한.
            if (30 <= tr.eulerAngles.x - prevX || tr.eulerAngles.x - prevX <= -30)
                ;
            else
                changeValueX = tr.eulerAngles.x - prevX;

            if (30 <= tr.eulerAngles.y - prevY || tr.eulerAngles.y - prevY <= -30)
                ;
            else
                changeValueY = tr.eulerAngles.y - prevY;

            if (30 <= tr.eulerAngles.z - prevZ || tr.eulerAngles.z - prevZ <= -30)
                ;
            else
                changeValueZ = tr.eulerAngles.z - prevZ;
        }
        


        //float dist = Vector3.Distance(tr.position,prevTr);
        //Debug.Log(dist / delay);

        //이동하는 방향벡터와 객차의 forward 방향 벡터 사이의 각도를 구함
        betweenAngle = Vector3.Angle((tr.position - prevTr), transform.up);

        //Debug.Log("각도 : " + betweenAngle);



        Debug.Log("tr = " + tr.position.ToString() + " prevTr = " + prevTr.ToString());

        //사이 각도가 90도 이상이면 후진 중이므로.
        if(90 < betweenAngle)
            Debug.Log("시속 : " + -dist / delay * 3.6 + "km/h");
        //사이 각도가 90도 이하이면 전진 중이므로. 
        else if(betweenAngle < 90)
            Debug.Log("시속 : " + dist / delay * 3.6 + "km/h");

        //현재와 이전의 y 포지션 값 사이의 차의 값이 음수라면 하강 중이라는 뜻.
        heaveVelocity = (float)(distForYAxis / delay * 3.6);
        Debug.Log("히브 값 : " + heaveVelocity + "km/h");

        //Debug.Log(Vector3.Distance(new Vector3(0,0,1), new Vector3(0,0,2)));

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

        if (rotatingRailLeft == false && rotatingRailRight == false)
        {
            //만약 왼쪽으로 기울었다면
            if (0.1f <= (rightWing.position.y - leftWing.position.y))
            {
                tr.Rotate(new Vector3(0, -0.6f, 0));
            }
            ////오른쪽으로 기울었다면
            else if (0.1f <= (leftWing.position.y - rightWing.position.y))
            {
                tr.Rotate(new Vector3(0, 0.6f, 0));
            }
        }
        if (rotatingRailLeft == true)
        {
            if (count == 0)
            {
                tr.Rotate(new Vector3(0, 15.0f, 0));
                count++;
            }

            /*
            //만약 왼쪽으로 기울었다면
            if (0.258f <= (rightWing.position.y - leftWing.position.y))
            {
                tr.Rotate(new Vector3(0, -0.2f, 0));
            }
            ////오른쪽으로 기울었다면
            else if (0.258f >= (rightWing.position.y - leftWing.position.y))
            {
                tr.Rotate(new Vector3(0, 0.2f, 0));
            }
            */
        }
        else if (rotatingRailRight == true)
        {
            if (count == 0)
            {
                tr.Rotate(new Vector3(0, -15.0f, 0));
                count++;
            }

            /*
            //만약 왼쪽으로 기울었다면
            if (0.258f <= (leftWing.position.y - rightWing.position.y))
            {
                tr.Rotate(new Vector3(0, 0.2f, 0));
            }
            ////오른쪽으로 기울었다면
            else if (0.258f >= (leftWing.position.y - rightWing.position.y))
            {
                tr.Rotate(new Vector3(0, -0.2f, 0));
            }
            */

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
            tr.rigidbody.AddForce(tr.transform.up * 350);
            //Destroy(coll.gameObject);
        }


        if (coll.gameObject.tag == "LEFTROTATE")
        {
            if (rotatingRailLeft == true)
                rotatingRailLeft = false;
            else
                rotatingRailLeft = true; count = 0;
        }
        else if (coll.gameObject.tag == "RIGHTROTATE")
        {
            if (rotatingRailRight == true)
                rotatingRailRight = false;
            else
                rotatingRailRight = true; count = 0;
        }
        if(coll.gameObject.tag == "ADDFORCE_START")
        {
            addForce = true;
        }
        else if(coll.gameObject.tag == "ADDFORCE_END")
        {
            addForce = false;
        }
    }

    void LateUpdate()
    {
        //delay만큼 시간이 흐를 때 마다 변화된 각도 계산, 출력.
        if (Time.time > nextSave)
        {
            //0.1 초 전의 위치와의 거리 측정.
            dist = Vector3.Distance(tr.position, prevTr);
            distForYAxis = tr.position.y - prevTr.y;

            nextSave = Time.time + delay;

            //이전 각도 구하는 코드(스크립트로 구하는).
            prevPitch = -Vector3.Angle(standardY, GameObject.FindWithTag("Player").transform.up) + 90.0f;

            //앞 방향으로 가고있다면
            if (true == isForwardDirection)
            {
                prevRoll = -Vector3.Angle(standardX, -GameObject.FindWithTag("Player").transform.forward) + 90.0f;
                prevYaw = Vector3.Angle(standardZ, GameObject.FindWithTag("Player").transform.right) - 90.0f;
            }
            else
            {
                prevRoll = -(-Vector3.Angle(standardX, -GameObject.FindWithTag("Player").transform.forward) + 90.0f);
                prevYaw = -(Vector3.Angle(standardZ, GameObject.FindWithTag("Player").transform.right) - 90.0f);
            }


            //이전 각도 구하는 코드.
            prevX = tr.eulerAngles.x;
            prevY = tr.eulerAngles.y;
            prevZ = tr.eulerAngles.z;

            //이전 위치 구하는 코드.
            prevTr.x = tr.position.x;
            prevTr.y = tr.position.y;
            prevTr.z = tr.position.z;

            //Debug.Log("x : " + x + "\ty : " + y + "\tz : " + z);
        }
    }
}
