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
	
	void OnTriggerEnter(Collider player) {
		if (player.tag.Equals("Player"))
        {
			collided = true;
            if (locked)
                GameObject.Find("Instructions").GetComponent<GUISubtitle>().displaySubtitles("This door is locked. I need to find the valve");
            else
                GameObject.Find("Instructions").GetComponent<GUISubtitle>().displaySubtitles("Press 'A' to enter the room");
		}
	}
	
	void OnTriggerExit(Collider player)
    {
		collided = false;
        GameObject.Find("Instructions").GetComponent<GUISubtitle>().hideSubtitles();
    }
}
