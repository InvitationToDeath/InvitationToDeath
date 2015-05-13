using UnityEngine;
using System.Collections; 

public class Move : MonoBehaviour
{
	public float MoveSpeed;
	Vector3 lookDirection;
		
	void Update ()
	{
		if (Input.GetKey (KeyCode.LeftArrow)  ||
		    Input.GetKey (KeyCode.RightArrow) ||
		    Input.GetKey (KeyCode.UpArrow)    ||
		    Input.GetKey (KeyCode.DownArrow)) 
		{
			float xx = Input.GetAxisRaw ("Vertical");
			float zz = Input.GetAxisRaw ("Horizontal");	
			lookDirection = xx * Vector3.forward + zz * Vector3.right;		
			
			this.transform.rotation = Quaternion.LookRotation (lookDirection);
			this.transform.Translate (Vector3.forward * MoveSpeed * Time.deltaTime);	
		}

        //if (Input.GetKey("z"))
        //    rigidbody.AddRelativeForce(Vector3.forward * 10.0f);
        //if (Input.GetKey("x"))
        //    rigidbody.AddRelativeForce(Vector3.right * 10.0f);
        //if (Input.GetKey("y"))
        //    rigidbody.AddRelativeForce(Vector3.up * 10.0f);

        if (Input.GetKey("z"))
            rigidbody.AddRelativeForce(new Vector3(0, 0, 1) * 10.0f);
        if (Input.GetKey("x"))
            rigidbody.AddRelativeForce(new Vector3(1, 0, 0) * 10.0f);
        if (Input.GetKey("y"))
            rigidbody.AddRelativeForce(new Vector3(0, 1, 0) * 10.0f);

        //if (Input.GetKey("z"))
        //    rigidbody.AddRelativeForce(transform.right * 10.0f);
        //if (Input.GetKey("x"))
        //    rigidbody.AddRelativeForce(transform.forward * 10.0f);
        //if (Input.GetKey("y"))
        //    rigidbody.AddRelativeForce(transform.up * 10.0f);
	}
	
}