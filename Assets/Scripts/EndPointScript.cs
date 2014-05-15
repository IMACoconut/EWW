using UnityEngine;
using System.Collections;

public class EndPointScript : MonoBehaviour {
	
	
	public GameScript mainScript;
	private bool collided;
	// Use this for initialization
	void Awake() {
		enabled = true;
		collided = false;
	}

	void Start () {

	}
	
	// Update is called once per frame
    void Update()
    {
        if (collided)
        {
            if (Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.E))
            {
                mainScript.LeaveRoom();
                mainScript.instructions.hideSubtitles();
            }
        }
    }
	
	void OnTriggerEnter(Collider p) {
        if (p.tag.StartsWith("Player"))
        {
            collided = true;
            if(Constants.useController)
                mainScript.instructions.displaySubtitles("Press 'A' to leave the room");
            else
                mainScript.instructions.displaySubtitles("Press 'E' to leave the room");
        }
	}
	
	void OnTriggerExit(Collider player) {
		collided = false;
        mainScript.instructions.hideSubtitles();
    }
}
