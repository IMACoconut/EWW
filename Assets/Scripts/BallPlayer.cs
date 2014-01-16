using UnityEngine;
using System.Collections;

public class BallPlayer : MonoBehaviour {

	// Use this for initialization
    public CatchableObject objectTaken;
    public CatchableObject canBeTaken;
	private Vector3 lastDir;
	
	void Start () {
        objectTaken = null;
        canBeTaken = null;
		lastDir = Vector3.zero;
		//rigidbody.mass = 30;
	}
	
	
	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();
		
		var controlCameraObject = GameObject.Find("Main Camera");
		float tmpSpeed = 1f;
		Vector3 mov = Vector3.zero;
		//if() {
	        //if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Left Analog Vertical") < -0.2f)
			float forw = 0f;
			if(Input.GetAxis("Left Analog Vertical") > 0.2f || Input.GetAxis("Left Analog Vertical") < -0.2f)
				forw = Input.GetAxis("Left Analog Vertical");
			float right = 0f;
			if(Input.GetAxis("Left Analog Horizontal") > 0.2f || Input.GetAxis("Left Analog Horizontal") < -0.2f)
				right = Input.GetAxis("Left Analog Horizontal");
		
			if(Mathf.Abs(forw) > 0.6 || Mathf.Abs(right) > 0.6)
				tmpSpeed = 3;
			
			Vector3 forwardVec = controlCameraObject.transform.forward;
			forwardVec.y = 0;
			forwardVec.Normalize();
			Vector3 rightVec = controlCameraObject.transform.right;
			rightVec.y = 0;
			rightVec.Normalize();
			
			mov = forwardVec * forw * -1 + rightVec*right;
			mov.Normalize();
			mov *= Constants.charSpeed * tmpSpeed;
			mov *= Time.deltaTime;
			transform.LookAt(transform.position + mov);
			lastDir = mov;
			if(controller.isGrounded && Input.GetButton("X"))
				mov.y = 10f;
			
			
		//} else {
		//	mov = lastDir;	
		//}
		
		mov.y -= 9.81f*Time.deltaTime;
		
		controller.Move(mov);
        
		
        
       /* else if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Left Analog Vertical") > 0.2f)*/
            //controller.Move(-1 * tr.transform.forward * speed);

        /*if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Right Analog Horizontal") < -0.2f)
            transform.Rotate(0, 10*speed,0);
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Right Analog Horizontal") > 0.2f)
            transform.Rotate(0, 10*speed*-1,0);*/



        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("X"))
            GrabObject();
        
		if(controller.velocity.magnitude > 0.1f && !animation.IsPlaying("walk")) {
			animation.Play("walk");
		} else if(controller.velocity.magnitude < 0.1f){
				animation.Stop();
		}
		
		/*
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Left Analog Horizontal") < -0.2f)
			rigidbody.AddForce(Vector3.left);
		else if(Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Left Analog Horizontal") > 0.2f)
			rigidbody.AddForce(Vector3.right);
		
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Left Analog Vertical") < -0.2f)
			rigidbody.AddForce(Vector3.forward);
		else if(Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Left Analog Vertical") > 0.2f)
			rigidbody.AddForce(Vector3.back);

        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("X"))
            GrabObject();
       // if (objectTaken != null)
       //     objectTaken.rigidbody.MovePosition(move);
       */
	}

    void GrabObject()
    {
        if (objectTaken != null)
        {
            Debug.Log("drop");
            objectTaken.Drop(this);
        }
        if(canBeTaken != null)
        {
            Debug.Log("catch");
            canBeTaken.Take(this);
        }
    }
}
