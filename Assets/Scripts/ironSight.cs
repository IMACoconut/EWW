﻿using UnityEngine;
using System.Collections;

public class ironSight : MonoBehaviour
{

    public GameObject ball;
    
    float theta, phi, distance;
    // Use this for initialization
    void Start()
    {
        distance = 0.001f;
        theta = 0;
        phi = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Constants.pause)
            return;

        if (Input.GetJoystickNames().Length > 0)
        {
            float tmp = Input.GetAxis("Right Analog Horizontal");
            float tmp1 = Input.GetAxis("Right Analog Vertical");
            if (tmp > 0.2f || tmp < -0.2f)
                theta += 2 * tmp;


            if (tmp1 > 0.2 || tmp1 < -0.2)
                phi -= 2 * tmp1;

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
            else if (phi < 30.0f)
                phi = 30f;


        }


        float x = distance * Mathf.Cos(theta * Mathf.PI / 180f) * Mathf.Sin(phi * Mathf.PI / 180f);
        float z = distance * Mathf.Sin(theta * Mathf.PI / 180f) * Mathf.Sin(phi * Mathf.PI / 180f);
        float y = distance * Mathf.Cos(phi * Mathf.PI / 180f);

        Vector3 origin = ball.transform.position;
        origin.x += 5;
		origin.z += 2.5f;
        Vector3 look = origin ;
        look.y += 8.5f;
		
        transform.position = look - new Vector3(x, y, z);

        transform.LookAt(look);
    }
}