using UnityEngine;
using System.Collections;

public class BallPlayer : MonoBehaviour
{

    // Use this for initialization
    public CatchableObject objectTaken;
    public CatchableObject canBeTaken;

    public int currentCam = 1;

    private Vector3 lastDir;


    void Start()
    {
        objectTaken = null;
        canBeTaken = null;

        camSwap(1);
        //rigidbody.mass = 30;

        lastDir = Vector3.zero;

    }


    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        if (Constants.pause)
        {
            controller.Move(new Vector3(0, 0, 0));
            return;
        }

        var controlCameraObject = GameObject.Find("Main Camera");
        if (currentCam == 1) controlCameraObject = GameObject.Find("Main Camera");
        else controlCameraObject = GameObject.Find("ironSight");

        //if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Left Analog Vertical") < -0.2f)
        float forw = 0f;

        float right = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) forw = -1;
        if (Input.GetKey(KeyCode.DownArrow)) forw = 1;
        if (Input.GetKey(KeyCode.RightArrow)) right = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) right = -1;



        float tmpSpeed = 1f;
        Vector3 mov = Vector3.zero;
        //if() {
        //if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Left Analog Vertical") < -0.2f)

        if (Input.GetAxis("Left Analog Vertical") > 0.2f || Input.GetAxis("Left Analog Vertical") < -0.2f)
            forw = Input.GetAxis("Left Analog Vertical");

        if (Input.GetAxis("Left Analog Horizontal") > 0.2f || Input.GetAxis("Left Analog Horizontal") < -0.2f)
            right = Input.GetAxis("Left Analog Horizontal");

        if (Mathf.Abs(forw) > 0.6 || Mathf.Abs(right) > 0.6)
            tmpSpeed = 3;

        Vector3 forwardVec = controlCameraObject.transform.forward;
        forwardVec.y = 0;
        forwardVec.Normalize();
        Vector3 rightVec = controlCameraObject.transform.right;
        rightVec.y = 0;
        rightVec.Normalize();

        mov = forwardVec * forw * -1 + rightVec * right;
        mov.Normalize();
        mov *= Constants.charSpeed * tmpSpeed;
        mov *= Time.deltaTime;
        transform.LookAt(transform.position + mov);
        lastDir = mov;

		if (controller.isGrounded && Input.GetButton("X") || Input.GetKeyDown("space"))
            mov.y += 10f;


        //} else {
        //	mov = lastDir;	
        //}

        mov.y -= 9.81f * Time.deltaTime*3;

        controller.Move(mov);

        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("X"))
            GrabObject();


        if (controller.velocity.magnitude > 0.1f && !animation.IsPlaying("walk"))
        {
            animation.Play("walk");
        }
        else if (controller.velocity.magnitude < 0.1f)
        {
            animation.Stop();
        }

        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Jump");


        }

        if (Input.GetKey("1"))
        {
            Debug.Log("main camera");
            camSwap(1);
            currentCam = 1;
        }
        if (Input.GetKey("2"))
        {
            Debug.Log("iron sight");
            camSwap(2);
            currentCam = 2;

        }

    }
    void camSwap(int currentCam)
    {
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");

        foreach (GameObject cams in cameras)
        {
            Camera theCam = cams.GetComponent<Camera>() as Camera;
            theCam.enabled = false;
        }
        string oneToUse = "";
        if (currentCam == 1) { oneToUse = "Main Camera"; }
        else if (currentCam == 2) { oneToUse = "ironSight"; }
        Camera usedCam = GameObject.Find(oneToUse).GetComponent<Camera>() as Camera;
        usedCam.enabled = true;


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
