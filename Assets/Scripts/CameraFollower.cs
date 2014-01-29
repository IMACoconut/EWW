using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {
	
	public BallPlayer Player;
    public Transform posCam, lookAtCam;
    private float rotateY = 0;
    public Texture reticle;

    private float smooth = 7f;
    private bool iron = false;


	float theta, phi;

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

    float ContAngle(Vector3 fwd, Vector3 targetDir, Vector3 upDir)
    {

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

	// Use this for initialization
	void Start () {
		theta = ContAngle(Player.transform.forward, Vector3.right, Vector3.up);
        if (theta >= 0)
            theta = 360 - theta;
		phi = 0;
        //maxDist = Constants.camDist;

	}
	
	// Update is called once per frame
	void Update () {

        float tmp = 0f, tmp1 = 0f;

        if (Input.GetJoystickNames().Length > 0)
        {
            if (Input.GetAxis("Right Analog Horizontal") < -0.2f || Input.GetAxis("Right Analog Horizontal") > 0.2f)
                tmp = Input.GetAxis("Right Analog Horizontal");
            if (Input.GetAxis("Right Analog Vertical") < -0.2f || Input.GetAxis("Right Analog Vertical") > 0.2f)
                tmp1 = Input.GetAxis("Right Analog Vertical");
        }
        else
        {
            tmp = Input.GetAxis("Mouse X") * Constants.sensitivity;
            tmp1 = Input.GetAxis("Mouse Y") * Constants.sensitivity;
        }
        
        if (Input.GetAxis("lock") == 1)
        {
            //print(posCam.position);
            transform.position = Vector3.Lerp(transform.position, posCam.position, smooth * Time.deltaTime);
            transform.LookAt(lookAtCam);
            Player.angle = tmp;
            rotateY += tmp1;
            transform.Rotate(-rotateY, 0, 0);
            iron = true;
        }
        else
        {
            if (iron)
            {
                theta = ContAngle(Player.transform.forward, Vector3.right, Vector3.up);
                if (theta >= 0)
                    theta = 360 - theta;
                Debug.Log(theta);
                phi = 0;
                iron = false;
            }
            rotateY = 0;

            if (tmp > 0.2f || tmp < -0.2f)
                theta += 2 * tmp;

            if (tmp1 > 0.2 || tmp1 < -0.2)
                phi -= 2 * tmp1;

            if (phi >= 179.9f)
                phi = 179.9f;
            else if (phi < 90.0f)
                phi = 90f;
            
            if (Player.currentRotate)
            {
                theta -= Player.angle;
            }

            Vector3 look = Player.transform.position;

            look.y += Constants.camHeight;

            transform.position = Vector3.Lerp(transform.position, look - CalculateOrbit(Constants.camDist), smooth * Time.deltaTime);
            
            transform.LookAt(look);
        }
	}

    void OnCollisionEnter(Collision h)
    {
       /* Debug.Log("toto");
        RaycastHit hit;
        Vector3 dir = transform.position - ball.transform.position;
        if(Physics.Raycast(ball.transform.position, dir, out hit)) {
            Debug.Log("collide");
            Vector3 look = ball.transform.position;
            transform.position = look-CalculateOrbit(hit.distance-10f);
            maxDist = hit.distance;
            //transform.position = ball.transform.position;// (hit.point - ball.transform.position) * 0.8f + ball.transform.position;
        }
        else
        {
            maxDist = Constants.camDist;
        }*/
    }

    Vector3 CalculateOrbit(float dist)
    {
        float x = dist * Mathf.Cos(theta * Mathf.PI / 180f) * Mathf.Sin(phi * Mathf.PI / 180f);
        float z = dist * Mathf.Sin(theta * Mathf.PI / 180f) * Mathf.Sin(phi * Mathf.PI / 180f);
        float y = dist * Mathf.Cos(phi * Mathf.PI / 180f);
        return new Vector3(x, y, z);
    }

    void OnGUI()
    {
        if (Time.time != 0 && Time.timeScale != 0 && gameObject.camera.enabled && Input.GetAxis("lock") > 0)
            GUI.DrawTexture(new Rect(Screen.width / 2 - (reticle.width * 0.5f), Screen.height / 2 - (reticle.height * 0.5f), reticle.width, reticle.height), reticle);
    }

}
