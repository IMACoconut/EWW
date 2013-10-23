using UnityEngine;
using System.Collections;

public class BallPlayer : MonoBehaviour {

	// Use this for initialization
    public CatchableObject objectTaken;
    public CatchableObject canBeTaken;
	void Start () {
        objectTaken = null;
        canBeTaken = null;
	}
	
	
	// Update is called once per frame
	void Update () {
		
		var controlCameraObject = GameObject.Find("Main Camera");
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Left Analog Vertical") < -0.2f)
            rigidbody.AddForce(controlCameraObject.transform.forward * 3);
        
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Left Analog Vertical") > 0.2f)
            rigidbody.AddForce(-1 * controlCameraObject.transform.forward * 3);

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Left Analog Horizontal") < -0.2f)
            rigidbody.AddForce(controlCameraObject.transform.right * 3 * -1 );
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Left Analog Horizontal") > 0.2f)
            rigidbody.AddForce(controlCameraObject.transform.right * 3);



        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("X"))
            GrabObject();
        
		
		
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
