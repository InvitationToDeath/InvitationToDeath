using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour {

	private float h = 0.0f;
	private float v = 0.0f;

	//자주 사용하는 컴포넌트는 반드시 변수에 할당한 후 사용.
	private Transform tr;
	//이동속도 변수
	public float moveSpeed = 10.0f;
	//회전속도 변수
	public float rotSpeed = 100.0f;

	/*
	클래스는 System.serializable이라는 어트리뷰트(attribute)를 명시해야
	Inspector 뷰에 노출됨.
	*/
	[System.Serializable]
	//애니메이션 클립을 저장할 클래스
	public class Anim{
		public AnimationClip idle;
		public AnimationClip runForward;
		public AnimationClip runBackward;
		public AnimationClip runRight;
		public AnimationClip runLeft;
	}

	//인스펙터뷰(Inspector view)에 표시할 애니메이션 클래스 변수.
	public Anim anim;

	//하위에 있는 3D 모델의 Animation 컴포넌트에 접근하기 위한 변수.
	public Animation _animation;


	// Use this for initialization
	void Start () {
		//스크립트 처음에 Transform 컴포넌트 할당.
		tr = GetComponent<Transform> ();
		//자신의 하위에 있는 Animation 컴포넌트를 찾아와 변수에 할당.
		_animation = GetComponentInChildren<Animation> ();
		//Animation 컴포넌트의 애니메이션 클립을 지정하고 실행.
		_animation.clip = anim.idle;
		_animation.Play ();
		// == _animation.Play("idle");
	}
	
	// Update is called once per frame
	void Update () {
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");

		Debug.Log ("H = " + h.ToString ());
		Debug.Log ("V = " + v.ToString ());

		//전후좌우 이동방향 벡터 계산
		Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

		//Transform 클래스에 정의된 이동관련 Translate 메서드.
		//Translate(이동방향 * 속도 * 변위값 * Time.deltaTime, 기준좌표)
		tr.Translate (moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

		//Vector3.up 축 기준으로 rotSpeed만큼의 속도로 회전.
		tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));

		//키보드 입력값을 기준으로 동작할 애니메이션 수행.
		if (v >= 0.1f) {
						//전진 애니메이션
						_animation.CrossFade (anim.runForward.name, 0.3f);
				} else if (v <= -0.1f) {
						//후진 애니메이션
						_animation.CrossFade (anim.runBackward.name, 0.3f);
				} else if (h >= 0.1f) {
						//오른쪽 이동 애니메이션
						_animation.CrossFade (anim.runRight.name, 0.3f);
				} else if (h <= -0.1f) {
						//왼쪽 이동 애니메이션
						_animation.CrossFade (anim.runLeft.name, 0.3f);
				} else {
						//정지시 idle 애니메이션
						_animation.CrossFade (anim.idle.name, 0.3f);
				}

	}
}
