using UnityEngine;
using System.Collections;

public class BallPlayer : MonoBehaviour
{

    // Use this for initialization
    public CatchableObject objectTaken;
    public CatchableObject canBeTaken;

    public int currentCam = 1;
	public bool run;
    public bool walk;
    public bool jump;
    public bool jumpAnime;
	public bool lockVar;
    public bool currentRotate;
    public float angle;

    private Vector3 lastDir;
    //private float distToGround;
    private const float jumpPower = 10.0f;
    private float currentJump;

    private float velocity = 1f;
    private float maniability = 2f;
    private float jumpTime = 1f;

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

        lastDir = Vector3.zero;

        // get the distance to ground
       // distToGround = 0;
        //distToGround = collider.bounds.extents.y;

    }


    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        var controlCameraObject = GameObject.Find("Main Camera");
        if (currentCam == 1) controlCameraObject = GameObject.Find("Main Camera");
        else controlCameraObject = GameObject.Find("ironSight");

        //if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Left Analog Vertical") < -0.2f)
        float forw = 0f;

        angle = 0f;
        currentRotate = false;

        //récupération des input clavier

            //le perso court ?
        run = false;
        float runFactor = 0.7f;
        if (Input.GetAxis("run")>0f)
        {
            run = true;
            runFactor = 1f;
        }
            //direction
   
        forw = -1 * velocity * runFactor * Input.GetAxis("Vertical");
        angle = 1 * maniability * Input.GetAxis("Horizontal");
        if(angle != 0)
            currentRotate = true;

        float tmpSpeed = 1f;
        Vector3 mov = Vector3.zero;

        //input manettes
        if (Input.GetAxis("Left Analog Vertical") > 0.2f || Input.GetAxis("Left Analog Vertical") < -0.2f)
            forw = Input.GetAxis("Left Analog Vertical") * velocity;

        if (Input.GetAxis("Left Analog Horizontal") > 0.2f || Input.GetAxis("Left Analog Horizontal") < -0.2f)
        {
            angle = Input.GetAxis("Left Analog Horizontal") * maniability;
            currentRotate = true;
        }

       

        if (jumpAnime)
        {
            mov = lastDir;
        }
        else
        {
            if (Mathf.Abs(forw) > 0.6 || Mathf.Abs(angle) > 0.6)
                tmpSpeed = 3;

            //roatation input appliqué au personnage
            transform.Rotate(0, angle, 0);

            //réorientation du personnage par rapport à la caméra
            Vector3 forwardVec = controlCameraObject.transform.forward;
            forwardVec.y = 0;
            forwardVec.Normalize();

            float angleCamera = ContAngle(transform.forward, forwardVec, transform.up);

            if ((angleCamera < 10f && angleCamera > -10f) || (angleCamera <= 360f && angleCamera > 350f) || (angleCamera < -350f && angleCamera >= -360f))
            {
                mov = forwardVec * forw * -1;
                mov.Normalize();
                mov *= Constants.charSpeed * tmpSpeed;
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
            else if (forw != 0)
            {
                //Debug.Log(angleCamera);
                if (angleCamera > 0)
                    transform.Rotate(0, -10f, 0);
                else
                    transform.Rotate(0, 10f, 0);
            }
        }
		//jump
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
            currentJump += 9.81f * Time.deltaTime * 2f;
            mov.y += 9.81f * Time.deltaTime * 2f;
        }

		// gravity
        if (!(IsGrounded()))
        {
            mov.y -= 9.81f * Time.deltaTime;
        }
        
        // application du mouvement du personnage
        controller.Move(mov);

        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("X"))
            GrabObject();

        //gestion des animations
        if (jumpAnime)
            animation.CrossFade("jump", 0.1f);
		else if(!IsGrounded())
			animation.CrossFade("fall", 0.1f);
		else if (controller.velocity.magnitude > 0.1f)
        {
			if(Mathf.Abs(forw) > 0.7f * velocity)
            	animation.CrossFade("run", 0.1f);
			else
				animation.CrossFade("walk", 0.1f);
        }
		else
			animation.CrossFade("idle", 0.1f);

        //changement de vue camera
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
