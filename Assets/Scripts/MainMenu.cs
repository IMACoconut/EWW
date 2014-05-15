using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Camera cam;
    public TextMesh quit;
    public TextMesh play;
    private TextMesh selected;
	// Use this for initialization
	void Start () {
        Screen.lockCursor = false; 
	
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction*1000, Color.red);
        //Debug.Log("toto");
	    if(Physics.Raycast(ray, out hit, 1000f, 1 << 8)) {
            if(hit.collider.name.StartsWith("Quit")) {
                quit.color = Color.red;
                play.color = Color.white;
                selected = quit;
            }
            
            if (hit.collider.name.StartsWith("Play"))
            {
                play.color = Color.red;
                quit.color = Color.white;
                selected = play;
            }
        }
        else
        {
            play.color = Color.white;
            quit.color = Color.white;
            selected = null;
        }

        if (Input.GetMouseButton(0))
        {
            if (selected == null)
                return;
            if (selected == quit)
                Application.Quit();
            else
                Application.LoadLevel("greyBox2");
        }
	}
}
