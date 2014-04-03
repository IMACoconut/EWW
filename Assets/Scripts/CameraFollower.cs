using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {
	
	public BallPlayer Player;
    public Transform /*posCam,*/ lookAtCam;
    private float rotateY = 0;

    private float smooth = 7f;
    private float maxDist = Constants.camDist;
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
		
        transform.position = Player.repere.transform.position;
        Vector3 look = Player.transform.position;
        look.y += Constants.camHeight;
        transform.LookAt(look);
        //maxDist = Constants.camDist;

	}
	
	// Update is called once per frame
	void Update () {

        if (Constants.pause)
            return;

        Collision();

        float tmp1 = 0;
        if (Input.GetJoystickNames().Length > 0)
        {
            if (Input.GetAxis("Right Analog Vertical") < -0.2f || Input.GetAxis("Right Analog Vertical") > 0.2f)
                tmp1 = -1 * Input.GetAxis("Right Analog Vertical") * Constants.sensitivity;
        }
        else
            tmp1 = Input.GetAxis("Mouse Y") * Constants.sensitivity;

        if (Input.GetAxis("LB") > 0)
            rotateY = 0;

        else
            rotateY += tmp1;

        if (Player.lockVar)
        {

            if (rotateY >= 45)
                rotateY = 45;
            else if (rotateY < -45)
                rotateY = -45;

            transform.position = Vector3.Lerp(transform.position, Player.repere.transform.position, smooth * Time.deltaTime);
            transform.LookAt(lookAtCam);

            transform.Rotate(-rotateY, 0, 0);
        }
        else
        {
            if (rotateY >= 170)
                rotateY = 170;
            else if (rotateY < 10)
                rotateY = 10;

            Vector3 look = Player.transform.position;
            look.y += Constants.camHeight;

            transform.position = Vector3.Lerp(transform.position, Orbit(), smooth * Time.deltaTime);
            transform.LookAt(look);

           
        }
      //  maxDist = Constants.camDist;
	}

    void OnCollisionEnter(Collision h)
    {
      /*  Debug.Log("toto");
        RaycastHit hit;
        Vector3 look = Player.transform.position;
        Vector3 dir = transform.position - look;
        if (Physics.Raycast(look, dir, out hit))
        {
            Debug.Log("collide");
            // transform.position = look-CalculateOrbit(hit.distance-10f);
            maxDist = hit.distance;
            //transform.position = ball.transform.position;// (hit.point - ball.transform.position) * 0.8f + ball.transform.position;
        }
        else
        {
            // maxDist = Constants.camDist;
        }*/
    }

    void Collision()
    {
        float wDistance = 4f;

        RaycastHit hit;
        Vector3 look = Player.transform.position;
        Vector3 dir = transform.position - look;

        if (Physics.Raycast(look, dir, out hit))
        {
            //Debug.Log(hit.distance);
            //Debug.Log(hit.collider.gameObject.name);
            if (hit.distance < Constants.camDist + wDistance)
            {
                maxDist = hit.distance - wDistance;
                if (maxDist < 2f)
                {
                    //if (hit.collider.transform.position != Player.transform.position)
                    maxDist = 2f;
                }
            }
            else
                maxDist = Constants.camDist;
        }
        else
            maxDist = Constants.camDist;
    }

    Vector3 CalculateOrbit(float dist)
    {
        float x = dist * Mathf.Cos(Player.repere.theta * Mathf.PI / 180f) * Mathf.Sin(rotateY * Mathf.PI / 180f);
        float z = dist * Mathf.Sin(Player.repere.theta * Mathf.PI / 180f) * Mathf.Sin(rotateY * Mathf.PI / 180f);
        float y = dist * Mathf.Cos(rotateY * Mathf.PI / 180f);
        return new Vector3(x, y, z);
    }

    Vector3 Orbit()
    {
        Vector3 result = Player.repere.transform.position - Player.transform.position;
        //result.y = 0;
        result.Normalize();
        result = Quaternion.FromToRotation(result, CalculateOrbit(1)) * result;

        Vector3 look = Player.transform.position;
        look.y += Constants.camHeight;

        return look + result * maxDist;
    }


}
