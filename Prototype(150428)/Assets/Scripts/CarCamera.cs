using UnityEngine;
using System.Collections;

public class CarCamera : MonoBehaviour
{
	public Transform target = null;
	public float height = 1f;
	public float positionDamping = 3f;
	public float velocityDamping = 3f;
	public float distance = 4f;
	public LayerMask ignoreLayers = -1;

	private RaycastHit hit = new RaycastHit();

	private Vector3 prevVelocity = Vector3.zero;
	private LayerMask raycastLayers = -1;
	
	private Vector3 currentVelocity = Vector3.zero;

    //카메라의 Transform을 담을 변수.
    private Transform tr;
    //회전 속도 변수.
    public float rotSpeed = 100.0f;
	
	void Start()
	{
		raycastLayers = ~ignoreLayers;

        //Transform 컴포넌트를 할당.
        tr = GetComponent<Transform>();
	}

    void Update()
    {
        Debug.Log(target.root.rigidbody.velocity.ToString());
        //rotSpeed만큼의 속도로 회전
        //tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
        //tr.Rotate(Vector3.left * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse Y"));

        //정규화벡터 사용 시도.
        //Vector3 rotateDir = (Vector3.up * Input.GetAxis("Mouse X")) + (Vector3.left * Input.GetAxis("Mouse Y"));
        //tr.Rotate(rotateDir.normalized * Time.deltaTime * rotSpeed, Space.Self);
    }

	void FixedUpdate()
	{
		currentVelocity = Vector3.Lerp(prevVelocity, target.root.rigidbody.velocity, velocityDamping * Time.deltaTime);
		currentVelocity.y = 0;
		prevVelocity = currentVelocity;
	}
	
	void LateUpdate()
	{
		float speedFactor = Mathf.Clamp01(target.root.rigidbody.velocity.magnitude / 70.0f);
		camera.fieldOfView = Mathf.Lerp(55, 72, speedFactor);
		float currentDistance = Mathf.Lerp(7.5f, 6.5f, speedFactor);
		
		currentVelocity = currentVelocity.normalized;
		
		Vector3 newTargetPosition = target.position + Vector3.up * height;
        Vector3 newPosition = newTargetPosition -(currentVelocity);// * currentDistance);
		newPosition.y = newTargetPosition.y;
		
		Vector3 targetDirection = newPosition - newTargetPosition;
		if(Physics.Raycast(newTargetPosition, targetDirection, out hit, currentDistance, raycastLayers))
			newPosition = hit.point;
		
		transform.position = newPosition;
		transform.LookAt(newTargetPosition);
	}
}
