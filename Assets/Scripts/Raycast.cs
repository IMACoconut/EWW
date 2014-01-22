using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour
{
    public RaycastHit hit;
    private Ray ray;
    private Vector3 vec;
    //private LayerMask layerMask; à voir si on a un système de layer
    public static Collider collider1 = new Collider();
    public Transform grabbed;
    private float grabDistance = 10f;


    void Start()
    {

    }

    void Update()
    {

        // Find the centre of the Screen
        vec.x = (float)Screen.width / 2;
        vec.y = (float)Screen.height / 2;
        vec.z = 0;

        if (gameObject.camera.enabled)
        {

            ray = gameObject.camera.ScreenPointToRay(vec);


            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);
                //Debug.Log("hit"); 
                if (Input.GetMouseButtonDown(0) && hit.transform.tag == "Grabable")
                    grabbed = hit.transform;
                collider1 = hit.collider;

                //Debug.Log(collider1.name); 
            }

        }

        UpdateHoldDrag();
       
    }

    void Drag() {
        vec.x = (float)Screen.width / 2;
        vec.y = (float)Screen.height / 2;
        vec.z = 0;
        Ray ray  = Camera.main.ScreenPointToRay(vec);
        Vector3 position = transform.position + transform.forward * grabDistance;
        Plane plane = new Plane(-transform.forward, position);
        float distance ;
            if (plane.Raycast(ray, out distance)) {
                grabbed.rigidbody.position = ray.origin + ray.direction * distance;
                grabbed.rotation = transform.rotation;
            }
    }

    void UpdateHoldDrag()
    {
        if (Input.GetMouseButton(0))
            if (grabbed)
                Drag();
            else
                Grab();
        else
            grabbed = null;
    }

    void Grab() {
    if (grabbed) 
       grabbed = null;
   
    }
}