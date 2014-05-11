using UnityEngine;
using System.Collections;

public class EndMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.lockCursor = false; 
        
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            Application.LoadLevel("menu");
        }
	
	}
}
