using UnityEngine;
using System.Collections;

public class PlayerRef : MonoBehaviour
{

    public BallPlayer Player;
    public Transform posCam, lookAtCam;
    private float rotateY = 0;
    public Texture reticle;

    private float tmp = 0f;
    private float smooth = 7f;
    private bool iron = false;
    private bool rotate = true;

    private bool isLock = false;


    public float theta, phi;

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
    void Start()
    {
        theta = ContAngle(Player.transform.forward, Vector3.right, Vector3.up);
        if (theta >= 0)
            theta = 360 - theta;
    }

    // Update is called once per frame
    void Update()
    {
        if (Constants.pause)
            return;

        tmp = 0f;
        int invert = Constants.invertCamera ? -1 : 1;

        if (Input.GetJoystickNames().Length > 0)
        {
            if (Input.GetAxis("Right Analog Horizontal") < -0.2f || Input.GetAxis("Right Analog Horizontal") > 0.2f)
                tmp = Input.GetAxis("Right Analog Horizontal") * invert * Constants.sensitivity;
        }
        else
            tmp = Input.GetAxis("Mouse X") * Constants.sensitivity * invert;


        if (Player.lockVar)
        {
            transform.position = posCam.position;
            transform.LookAt(lookAtCam);
            Player.angle = tmp;
            iron = true;
        }
        else
        {
            if (iron || Input.GetAxis("LB") > 0)
           {
               if (!isLock)
               {
                   isLock = true;
                   theta = ContAngle(Player.transform.forward, Vector3.right, Vector3.up);
                   if (theta >= 0)
                       theta = 180 - theta;
                   else
                       theta = -180 + theta;

               }
                
                iron = false;
                rotate = true;
            }
            else
            {
                isLock = false;

                if (tmp != 0)
                    rotate = true;



                rotateY = 0;

                theta = ContAngle(Vector3.forward, transform.right, Vector3.up);
                if (theta < 0)
                    theta *= -1;



                if (tmp > 0.2f || tmp < -0.2f)
                    theta -= 2 * tmp;

                phi = 90f;
            }

            transform.position = DefaultOrbit(Constants.camDist);

            Vector3 look = Player.transform.position;

            look.y += Constants.camHeight;

            transform.LookAt(look);

            //transform.Rotate(-phi, 0, 0);

            rotate = false;
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

    Vector3 DefaultOrbit(float Cdist)
    {
        Vector3 result = transform.position - Player.transform.position;
        result.y = 0;
        result.Normalize();
        if (rotate)
            result = Quaternion.FromToRotation(result, CalculateOrbit(1)) * result;

        Vector3 look = Player.transform.position;

        look.y += Constants.camHeight;
        return Cdist * result + look;
    }

    Vector3 CalculateOrbit(float Cdist)
    {
        float x = Cdist * Mathf.Cos(theta * Mathf.PI / 180f) * Mathf.Sin(phi * Mathf.PI / 180f);
        float z = Cdist * Mathf.Sin(theta * Mathf.PI / 180f) * Mathf.Sin(phi * Mathf.PI / 180f);
        float y = Cdist * Mathf.Cos(phi * Mathf.PI / 180f);
        return new Vector3(x, y, z);
    }

    void OnGUI()
    {
        if (Time.time != 0 && Time.timeScale != 0 && Player.lockVar)
            GUI.DrawTexture(new Rect(Screen.width / 2 - (reticle.width * 0.5f), Screen.height / 2 - (reticle.height * 0.5f), reticle.width, reticle.height), reticle);
    }

}
