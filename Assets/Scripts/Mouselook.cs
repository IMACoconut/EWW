using UnityEngine;
using System.Collections;

public class Mouselook : MonoBehaviour {


    public GameObject ball;
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationY = 0F;
	public Texture reticle;


    // Use this for initialization
    void Start()
    {


    }

    void Update (){
 
        if (axes == RotationAxes.MouseXAndY){

            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

        }

        else if (axes == RotationAxes.MouseX){

            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);

        }
        else{

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }

        Vector3 look = ball.transform.position + ball.transform.right * 0.15f * ball.transform.lossyScale.x + ball.transform.forward * (-0.3f) * ball.transform.lossyScale.x;
        look.y += 0.5f * ball.transform.lossyScale.y;
        transform.position = look ;
        transform.LookAt(look);
        

    }

	void OnGUI () {
		if (Time.time != 0 && Time.timeScale != 0 && gameObject.camera.enabled )
			GUI.DrawTexture(new Rect(Screen.width/2-(reticle.width*0.5f), Screen.height/2-(reticle.height*0.5f), reticle.width, reticle.height), reticle);
	}

    

}