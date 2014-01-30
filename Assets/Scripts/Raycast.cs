using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Raycast : MonoBehaviour
{
    public BallPlayer Player;
    public RaycastHit hit;
    private Ray ray, ray2;
    private Vector3 vec;
    //private LayerMask layerMask; à voir si on a un système de layer
    public static Collider collider1 = new Collider();
    public Transform grabbed;
    private float grabDistance = 1f;
    private float limGrab = 20f;
    private bool alt = false;
    private float holdingTime = 0f ;
    private int direction = 0;

    void Start()
    {


    }

    void Update()
    {
        if (Input.GetAxis("lock") == 1)
        {
            // Find the centre of the Screen
            vec.x = (float)Screen.width / 2;
            vec.y = (float)Screen.height / 2;
            vec.z = 0;

            if (gameObject.camera.enabled)
            {

                ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); 


                if (Physics.Raycast(ray, out hit))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    //Debug.Log("hit"); 
                    if ((Input.GetAxis("fire") > 0) && (hit.transform.tag == "Grabable" || hit.transform.tag == "Curvable"))
                    {
                        grabbed = hit.transform;
                        grabDistance = hit.distance;
                        Debug.Log(hit.distance);

                    }

                    collider1 = hit.collider;

                    //Debug.Log(collider1.name); 
                }

            }
            /* if (Input.GetKeyDown(KeyCode.LeftControl)) alt = true;
            else if (Input.GetKeyUp(KeyCode.LeftControl)) alt = false; */

            if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Left Analog Vertical") > 0.2f) direction = 0; //haut
            else if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Left Analog Horizontal") > 0.2f) direction = 1; // gauche
            else if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Left Analog Horizontal") < -0.2f) direction = 2; // droite
            else if (Input.GetAxis("Vertical") < 0 || Input.GetAxis("Left Analog Vertical") < -0.2f) direction = 3; // bas


            UpdateHoldDrag();
        }
    }

    void Drag() {
           
        Vector3 position = transform.position + transform.forward * grabDistance ;
        Plane plane = new Plane(-transform.forward, position);
        float distance ;
            if (plane.Raycast(ray, out distance)) {
                grabbed.rigidbody.position = ray.origin + ray.direction * limGrab;
                grabbed.rigidbody.rotation = transform.rotation;
            }
    }

    void Curve()
    {
        Vector3 size = grabbed.collider.bounds.size * 3f;

        Vector3 rotationaxis;

        if (direction == 0 || direction == 3) rotationaxis = Vector3.Cross(ray.direction, Vector3.up);
        else { rotationaxis = ray.direction; rotationaxis.y = 0f; }

        /* On part du principe qu'en y on a la hauteur de l'objet */
        //Debug.Log(direction);
        float radius = 2f * Mathf.PI * (size.y);
        List<Transform> bones;
        bones = new List<Transform>();
        // Debug.Log(Input.mousePosition.x + " " + Input.mousePosition.y + " " + Input.mousePosition.z);

        bones.Add(grabbed);
        bones.Add(grabbed.GetComponentInChildren<Transform>());
        bones.Add(grabbed.GetComponentInChildren<Transform>().GetComponentInChildren<Transform>());


        float alpha = -Mathf.Atan(bones[0].collider.bounds.size.y) / (radius * 10f);
        alpha *= 2f;
        //Debug.Log(holdingTime);
        if (direction == 3 || direction == 2) alpha = alpha * -1f;
        for (int i = 0; i < bones.Count; i++)
        {
            Debug.Log("Bone n°" + i + " = " + bones[i].localEulerAngles.z);

            bones[i].RotateAround(rotationaxis, alpha);

        }

    }


    void UpdateHoldDrag()
    {
        if (Input.GetAxis("fire") >0)
        {
            holdingTime += Time.deltaTime;
            if (grabbed)
            {
                if (grabbed.tag == "Grabable" && hit.distance <= limGrab)
                    Drag();
                else if (grabbed.tag == "Curvable")
                {
                    Curve();
                    Player.curve = true;
                }
            }
            else
                Grab();
        }



        else { grabbed = null;
        holdingTime = 0f;
        }
                
    }

    void Grab() {
    if (grabbed) 
       grabbed = null;
   
    }
}