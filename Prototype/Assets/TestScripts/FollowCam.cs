using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
    public Transform target; //추적할 타겟 게임오브젝트의 Transform 변수.
    private Vector3 pre_target; //타겟의 이전 프레임에서의 Transform.

	public float dist = 10.0f; //카메라와의 일정거리.
	public float height = 5.0f; //카메라의 높이설정.
	public float dampRotate = 5.0f; //부드러운 회전을 위한 변수.

	private Transform tr; //카메라 자신의 Transform 변수.

    private float angle = 0.0f; 


	// Use this for initialization
	void Start () {
		//카메라 자신의 Transform 컴포넌트를 tr에 할당.
		tr = GetComponent<Transform> ();
	}

	//Update 함수호출 이후 한번씩 호출되는 함수인 LateUpdate 사용.
	//추적할 타겟의 이동이 종료된 이후에 카메라가 추적하기 위해 LateUpdate 사용.
	void LateUpdate () {
		//카메라 Y축을 타겟의 Y축 회전각도로 부드럽게 회전
		float currYAngle = Mathf.LerpAngle (tr.eulerAngles.y
		                                    , target.eulerAngles.y
		                                    , dampRotate * Time.deltaTime);

		//쿼터니언 데이터 형으로 변환.
		//Quaternion rot = Quaternion.Euler (0, currYAngle, 0);

        Vector3 target_notY = target.position;
        target_notY.y = 0;
        pre_target.y = 0;


        angle = Vector3.Angle(pre_target, target_notY);
        //angle -= 90;

        //Debug.Log(target_notY.x.ToString() + ", " + target_notY.y.ToString() + ", " + target_notY.z.ToString());
        //Debug.Log(pre_target.x.ToString() + ", " + pre_target.y.ToString() + ", " + pre_target.z.ToString());


        Quaternion rot = Quaternion.Euler(0, angle, 0);

        //angle += 90;

		//카메라의 위치를 타겟이 회전한 각도만큼 회전한 이후로.
		//dist만큼 뒤쪽으로 배치하고 height만큼 위로 올림
		tr.position = target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);
        
        //tr.Rotate(0,angle,0);
        //angle += 90;
        //tr.position = target.position - (Vector3.forward * dist) + (Vector3.up * height);

		//카메라가 타겟 오브젝트를 바라보게 설정.
		//tr.LookAt (target);

        pre_target = target.position;
	}
}