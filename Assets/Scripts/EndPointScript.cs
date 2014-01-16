using UnityEngine;
using System.Collections;

public class EndPointScript : MonoBehaviour {
	
	
	public GameScript mainScript;
	private bool collided;
	
	// Use this for initialization
	void Start () {
		mainScript = GameObject.Find("GameGeneralScript").GetComponent<GameScript>();
		enabled = true;
		collided = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(collided) {
			if(Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.E))
			{
				mainScript.LeaveRoom();
			}
		}
	}
	
	void OnGUI() {
		if(!collided)
			return;
		
		GUI.Box(new Rect(0,Screen.height-50,Screen.width,50),"Press 'A' to leave the room");
	}
	
	void OnTriggerEnter(Collider p) {
		collided = true;
	}
	
	void OnTriggerExit(Collider player) {
		collided = false;
    }
}
