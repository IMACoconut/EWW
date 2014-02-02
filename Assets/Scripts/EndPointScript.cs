using UnityEngine;
using System.Collections;

public class EndPointScript : MonoBehaviour {
	
	
	public GameScript mainScript;
	private bool collided;
	
	// Use this for initialization
	void Awake() {
		mainScript = GameObject.Find("GameGeneralScript").GetComponent<GameScript>();
		enabled = true;
		collided = false;
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(collided) {
			if(Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.E))
			{
                Debug.Log("Ingrid, est-ce que tu bug après le cinéma");
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
        if (p.tag.StartsWith("Player"))
            collided = true;
        
        //    Debug.Log("collide");
	}
	
	void OnTriggerExit(Collider player) {
		collided = false;
    }
}
