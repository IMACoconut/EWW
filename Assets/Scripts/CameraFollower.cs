using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {
	
	public GameObject ball;
	float theta, phi;
	// Use this for initialization
	void Start () {
		theta = 0;
		phi = 0;
        //maxDist = Constants.camDist;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetJoystickNames().Length > 0)
        {
            float tmp = Input.GetAxis("Right Analog Horizontal");
            float tmp1 = Input.GetAxis("Right Analog Vertical");

            if (tmp > 0.2f || tmp < -0.2f)
                theta += 2 * tmp;


            if (tmp1 > 0.2 || tmp1 < -0.2)
                phi += 2 * tmp1;

            if (phi >= 179.9f)
                phi = 179.9f;
            else if (phi < 90.0f)
                phi = 90f;


        }
        else
        {
            float tmp = Input.GetAxis("Mouse X") * 0.8f;
            float tmp1 = Input.GetAxis("Mouse Y") * 0.8f;


            if (tmp > 0.2f || tmp < -0.2f)
                theta += 2 * tmp;


            if (tmp1 > 0.2 || tmp1 < -0.2)
                phi -= 2 * tmp1;

            if (phi >= 179.9f)
                phi = 179.9f;
            else if (phi < 90.0f)
                phi = 90f;

        }
		
		
		
		
		Vector3 look = ball.transform.position;
		look.y += Constants.camHeight;
        
		transform.position = look-CalculateOrbit(Constants.camDist);
		
		transform.LookAt(look);
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
}
