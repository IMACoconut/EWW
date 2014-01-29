using UnityEngine;
using System.Collections;

public class BallPlayer : MonoBehaviour
{

    // Use this for initialization
    public CatchableObject objectTaken;
    public CatchableObject canBeTaken;
    private GameObject controlCameraObject;
    private GameObject MainCam;
    private GameObject ironSight;
    public int currentCam = 1;
	public bool run;
    public bool walk;
    public bool jump;
    public bool jumpAnime;
    public bool idle = false;
	public bool lockVar = false;
    public bool currentRotate;
    public bool curve = false;
    public float angle;

    private float idleTime = 0;

    private Vector3 lastDir;
    private float distToGround;
    private const float jumpPower = 15.0f;
    private float currentJump;

    private float velocity = 1f;
    private float maniability = 2f;
    private float jumpTime = 1f;
    private float fallTime = 0f;

    private float debug = 0;

    bool IsGrounded(){
         CharacterController controller = GetComponent<CharacterController>();
      //return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
         return controller.velocity.y < 0.0001 && controller.velocity.y > -0.0001;
    }

    //returns -1 when to the left, 1 to the right, and 0 for forward/backward
    public float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);
        if (dir > 0.0f)
        {
            return 1.0f;
        }
        else if (dir < 0.0f)
        {
            return -1.0f;
        }
        else
        {
            return 0.0f;
        }
    }

    float ContAngle(Vector3 fwd, Vector3 targetDir, Vector3 upDir) {

        float result = Vector3.Angle(fwd, targetDir);    

        //The AngleDir function is the one from the other thread.
        if (AngleDir(fwd, targetDir, upDir) == 1) 
        {
            return result - 360f;
        } 
        else 
        {
            return result;
        }
    }

    void Start()
    {
        objectTaken = null;
        canBeTaken = null;

        currentJump = 0f;
        jump = false;

        camSwap(1);
        //rigidbody.mass = 30;
        MainCam = GameObject.Find("Main Camera");
        ironSight = GameObject.Find("ironSight");
        controlCameraObject = MainCam;
        lastDir = Vector3.zero;
        Screen.lockCursor = true;

        // get the distance to ground
       // distToGround = 0;
       // distToGround = collider.bounds.extents.y;

    }


    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        if (currentCam == 1) controlCameraObject = MainCam;
        else controlCameraObject = ironSight;
       
        float forw = 0f;
        float lat = 0f;

        if (Input.GetAxis("lock") != 1) angle = 0f;
        currentRotate = false;
        Vector3 mov = Vector3.zero;

        if (!curve)
        {
            //récupération des input clavier

            //le perso court ?
            run = true;
            float runFactor = 1f;
            if (Input.GetAxisRaw("run") == 1)
            {
                run = false;
                runFactor = 0.7f;
            }
            //direction
            forw = -1 * velocity * runFactor * Input.GetAxisRaw("Vertical");
            lat = velocity * runFactor * Input.GetAxisRaw("Horizontal");

            if (angle != 0)
                currentRotate = true;

            //input manettes
            if (Input.GetAxis("Left Analog Vertical") > 0.2f || Input.GetAxis("Left Analog Vertical") < -0.2f)
                forw = Input.GetAxis("Left Analog Vertical") * velocity;

            if (Input.GetAxis("Left Analog Horizontal") > 0.2f || Input.GetAxis("Left Analog Horizontal") < -0.2f)
            {
                lat = Input.GetAxis("Left Analog Horizontal") * velocity;
            }
        }


        float tmpSpeedF = 1f;
        float tmpSpeedL = 1f;
        if (Mathf.Abs(forw) > 0.7)
            tmpSpeedF = 3;
        if (Mathf.Abs(lat) > 0.7)
            tmpSpeedL = 2;

        //roatation input appliqué au personnage
        transform.Rotate(0, angle, 0);
        Vector3 lateral = controlCameraObject.transform.right;
        lateral *= lat * Constants.charSpeed * Time.deltaTime;
        //Debug.Log(lat);
        controller.Move(lateral * tmpSpeedL);

        //réorientation du personnage par rapport à la caméra
        Vector3 forwardVec = controlCameraObject.transform.forward;
        forwardVec.y = 0;
        forwardVec.Normalize();

        float angleCamera = ContAngle(transform.forward, forwardVec, transform.up);

        if ((angleCamera < 10f && angleCamera > -10f) || (angleCamera <= 360f && angleCamera > 350f) || (angleCamera < -350f && angleCamera >= -360f))
        {
            mov = forwardVec * forw * -1;
            mov.Normalize();
            mov *= Constants.charSpeed * tmpSpeedF;
            mov *= Time.deltaTime;

            Vector3 tmp = mov;
            if (forw > 0)
            {
                tmp.x = -mov.x;
                tmp.z = -mov.z;
            }
            transform.LookAt(transform.position + tmp);
            lastDir = mov;
        }
        else if (forw != 0 && !lockVar)
        {
            //Debug.Log(angleCamera);
            if (angleCamera > 0)
                transform.Rotate(0, -10f, 0);
            else
                transform.Rotate(0, 10f, 0);
        }

        //jump
        if (!lockVar)
        {
            if (IsGrounded())
            {
                jump = false;
                jumpAnime = false;
                currentJump = 0f;
                jumpTime += Time.deltaTime;
            }
            if ((Input.GetButton("X") || Input.GetAxis("Jump") > 0) && jumpTime >= 0.2f)
            {
                // rigidbody.AddForce(Vector3.up * 10.0f);
                jump = true;
                jumpTime = 0f;
                jumpAnime = true;
            }
            if (!(Input.GetButton("X") || Input.GetAxis("Jump") > 0))
            {
                jump = false;
                currentJump = 0f;
            }
            if (jump && currentJump < jumpPower)
            {
                currentJump += 9.81f * Time.deltaTime * 6f;
                mov.y += 9.81f * Time.deltaTime * 6f;
            }
        }
		// gravity
        if (!(IsGrounded()))
            fallTime += Time.deltaTime;
        else
            fallTime = 0;
        mov.y -= 9.81f * Time.deltaTime * 3f;

        //Debug.Log(controller.bounds.max.y);
        //Debug.Log(collider.bounds.extents.y);

        // application du mouvement du personnage
        controller.Move(mov);

        //vue iron sight
        Vector3 rightVec = controlCameraObject.transform.right;
        rightVec.y = 0;
        rightVec.Normalize();
        if (currentCam == 2) transform.right = rightVec;
        if (Input.GetMouseButtonUp(1))
        {
            lockVar = false;
            curve = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            lockVar = true;
        }

        //gestion des animations
        idle = false;
        if(fallTime > 2f)
            animation.CrossFade("fall", 0.1f);
        else if (lockVar)
            animation.CrossFade("posLock", 0.1f);
        else if (jumpAnime)
            animation.CrossFade("jump", 0.1f);
        else if (Mathf.Abs(forw) > 0.1f || Mathf.Abs(lat) > 0.1)
        {
            if (Mathf.Abs(forw) > 0.7f * velocity)
            {
                animation.CrossFade("run", 0.1f);
                animation["run"].speed = 2f;
            }
            else
                animation.CrossFade("walk", 0.1f);
        }
        else if (Mathf.Repeat(idleTime, 10 + animation["idleAss"].length) >= 0 && Mathf.Repeat(idleTime, 10 + animation["idleAss"].length) <= animation["idleAss"].length)
        {
            animation.CrossFade("idleAss", 0.1f);
            idle = true;
        }
        else
        {
            animation.CrossFade("idle", 0.1f);
            idle = true;
        }
        if (idle)
            idleTime += Time.deltaTime;
        else
            idleTime = animation["idleAss"].length;

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

        if (canBeTaken != null)
        {
            Debug.Log("catch");
            canBeTaken.Take(this);
        }
    }
}
