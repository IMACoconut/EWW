using UnityEngine;
using System.Collections;

public class DoorTeleport : MonoBehaviour {

    public GameObject to;
    private bool collided;

    private GameScript mainScript;
    public RoomFoldedSpace_01_script script;

	// Use this for initialization
	void Start () {
        collided = false;
        mainScript = GameObject.Find("GameGeneralScript").GetComponent<GameScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (collided)
        {
            if (Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.E))
            {
                mainScript.teleportPlayer(to);
                script.Restart();
            }
        }
	}

    void OnGUI()
    {
        if (!collided)
            return;

        GUI.Box(new Rect(0, Screen.height - 50, Screen.width, 50), "Press 'A' to leave the room");
    }

    void OnTriggerEnter()
    {
        collided = true;
    }

    void OnTriggerExit()
    {
        collided = false;
    }
}
