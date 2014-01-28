using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {
	
	public BallPlayer Player;
    public Transform posCam, lookAtCam;
    private float rotateY = 0;
    public Texture reticle;


	float theta, phi;
	// Use this for initialization
	void Start () {
		theta = 0;
		phi = 0;
        //maxDist = Constants.camDist;

	}
	
	// Update is called once per frame
	void Update () {

        float tmp, tmp1;

        if (Input.GetJoystickNames().Length > 0)
        {
            tmp = Input.GetAxis("Right Analog Horizontal");
            tmp1 = Input.GetAxis("Right Analog Vertical");
        }
        else
        {
            tmp = Input.GetAxis("Mouse X") * Constants.sensitivity;
            tmp1 = Input.GetAxis("Mouse Y") * Constants.sensitivity;
        }
        
        if (Input.GetMouseButton(1))
        {
            //print(posCam.position);
            transform.position = posCam.position;
            transform.LookAt(lookAtCam);
            Player.angle = tmp;
            rotateY += tmp1;
            transform.Rotate(-rotateY, 0, 0);
        }
        else
        {
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

            transform.position = look - CalculateOrbit(Constants.camDist);

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
        if (Time.time != 0 && Time.timeScale != 0 && gameObject.camera.enabled && Input.GetMouseButton(1))
            GUI.DrawTexture(new Rect(Screen.width / 2 - (reticle.width * 0.5f), Screen.height / 2 - (reticle.height * 0.5f), reticle.width, reticle.height), reticle);
    }

}
