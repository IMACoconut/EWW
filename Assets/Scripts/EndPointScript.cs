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
                GameObject.Find("Instructions").GetComponent<GUISubtitle>().hideSubtitles();
            }
        }
    }
	
	void OnTriggerEnter(Collider p) {
        if (p.tag.StartsWith("Player"))
        {
            collided = true;
            if(Constants.useController)
                GameObject.Find("Instructions").GetComponent<GUISubtitle>().displaySubtitles("Press 'A' to leave the room");
            else
                GameObject.Find("Instructions").GetComponent<GUISubtitle>().displaySubtitles("Press 'E' to leave the room");
        }
	}
	
	void OnTriggerExit(Collider player) {
		collided = false;
        GameObject.Find("Instructions").GetComponent<GUISubtitle>().hideSubtitles();
    }
}
