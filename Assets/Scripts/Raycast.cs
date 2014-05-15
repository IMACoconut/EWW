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
    private float grabDistance = 1f *0.1f;
    private float limGrab = 20f * 0.1f;
    private bool alt = false;
    private float holdingTime = 0f ;
    private int direction = -1;
    public WrenchRay particleRay;
    public Aiguille aiguille;
    
    void Start()
    {
        aiguille = GameObject.Find("Aiguille").GetComponent<Aiguille>();

    }

    void Update()
    {
        if (Player.lockVar)
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
                    if(hit.transform.tag == "Grabable" || hit.transform.tag == "Curvable") {
                        aiguille.SetAngle(aiguille.max);
                        //Debug.Log("hit : " + hit.transform.name); 
                        if (Input.GetAxis("fire") > 0.2f)
                        {
                        
                            grabbed = hit.transform;
                            grabDistance = hit.distance;
                            particleRay.target = hit.point;
                        
                            particleRay.StartEmit();
                            //Debug.Log(hit.distance);

                        }
                    } else
                        aiguille.SetAngle(aiguille.min);

                    collider1 = hit.collider;

                    //Debug.Log(collider1.name); 
                } else
                    aiguille.SetAngle(aiguille.min);

            }
            /* if (Input.GetKeyDown(KeyCode.LeftControl)) alt = true;
            else if (Input.GetKeyUp(KeyCode.LeftControl)) alt = false; */
            if (!Player.curve) direction = -1;
            else {
                if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Left Analog Vertical")< -0.2f ) direction = 0; //haut
                else if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Left Analog Horizontal") > 0.2f) direction = 1; // gauche
                else if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Left Analog Horizontal") < -0.2f) direction = 2; // droite
			    else if (Input.GetAxis("Vertical") < 0 || Input.GetAxis("Left Analog Vertical") > 0.2f ) direction = 3; // bas
            }
			


            UpdateHoldDrag();
        }
        else
        {
            particleRay.StopEmit();
            //if (grabbed != null && grabbed.tag == "Grabable") grabbed.rigidbody.useGravity = true; 
        }
    }

    void Drag() {
           
        Vector3 position = transform.position + transform.forward * grabDistance ;
        Plane plane = new Plane(-transform.forward, position);
        float distance ;
            if (plane.Raycast(ray, out distance)) {
                /*
                
                grabbed.rigidbody.position = ray.origin + ray.direction * limGrab;
                grabbed.rigidbody.rotation = transform.rotation; */
                //grabbed.rigidbody.useGravity = false; 
                Vector3 v = ray.origin + ray.direction * limGrab;
                //v.y = 0;
                grabbed.rigidbody.MovePosition(v);
                
            }
            //grabbed.rigidbody.useGravity = true; 
    }

    void Curve()
    {
        if (direction != -1) {

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
            alpha *= 2f * 0.3f;
            //Debug.Log(holdingTime);
            if (direction == 3 || direction == 2) alpha = alpha * -1f ;
            for (int i = 0; i < bones.Count; i++)
            {
                //Debug.Log("Bone n°" + i + " = " + bones[i].localEulerAngles.z);

                bones[i].RotateAround(rotationaxis, alpha);

            }
        
        
        
        }  

    }


    void UpdateHoldDrag()
    {

        if (Input.GetAxis("fire") >0.2f)
        {
            holdingTime += Time.deltaTime;
            if (grabbed)
            {
                if (grabbed.tag == "Grabable" && hit.distance <= limGrab)
                    Drag();
                else if (grabbed.tag == "Curvable" && holdingTime > 0.02f)
                {
                    holdingTime = 0f;
                    Curve();
                    Player.curve = true;
                }
            }
            else
            {
                Grab();

            }
        }



        else {
			//if(grabbed != null && grabbed.tag == "Grabable") grabbed.rigidbody.useGravity = true; 
            grabbed = null;
        particleRay.StopEmit();
        
        }
                
    }

    void Grab() {
    if (grabbed) 
       grabbed = null;
        particleRay.StopEmit();
   
    }
}