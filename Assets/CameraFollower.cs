using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {
	
	public GameObject ball;
	float theta, phi, distance;
	// Use this for initialization
	void Start () {
		distance = 5;
		theta = 0;
		phi = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float tmp = Input.GetAxis("Right Analog Horizontal");
		if(tmp > 0.2f || tmp < -0.2f)
			theta += 2*tmp;
		
		tmp = Input.GetAxis("Right Analog Vertical");
		if(tmp > 0.2 || tmp < -0.2)
			phi -= 2*tmp;
		
		if(phi >= 179.9f)
			phi =  179.9f;
		else if(phi < 90.0f)
			phi = 90f;
		
		float x = distance*Mathf.Cos(theta*Mathf.PI/180f)*Mathf.Sin(phi*Mathf.PI/180f);
		float z = distance*Mathf.Sin(theta*Mathf.PI/180f)*Mathf.Sin(phi*Mathf.PI/180f);
		float y = distance*Mathf.Cos(phi*Mathf.PI/180f);
		
		transform.position = ball.transform.position-new Vector3(x,y,z);
		transform.LookAt(ball.transform.position);
	}
}
