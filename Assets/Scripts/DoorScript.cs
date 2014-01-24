using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	
	public bool locked = true;
	public bool collided = false;
	public GameScript mainScript;
	// Use this for initialization
	void Start () {
		mainScript = GameObject.Find("GameGeneralScript").GetComponent<GameScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Constants.pause)
            return;
        if (collided && !locked)
        {
			if(Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.E))
			{
				mainScript.LeaveStreet();
			}
		}
	}
	
	void OnGUI() {
		if(!collided)
			return;
		
		if(locked)
			GUI.Box(new Rect(0,Screen.height-50,Screen.width,50), "This door is locked. I need to find the valve");
		else
			GUI.Box(new Rect(0,Screen.height-50,Screen.width,50), "Press 'A' to enter the room");
	}
	
	void OnTriggerEnter(Collider player) {
		if (player.tag.Equals("Player"))
        {
			collided = true;
		}
	}
	
	void OnTriggerExit(Collider player)
    {
		collided = false;
    }
}
