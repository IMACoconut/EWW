using UnityEngine;
using System.Collections;

public class ValveScript : MonoBehaviour {
	
	public GameScript mainScript;
	
	public bool useEnabled = false;
	private bool collided; 
    public bool used;
    public RoomClaveau room; 
	
	// Use this for initialization
	void Start () {
		mainScript = GameObject.Find("GameGeneralScript").GetComponent<GameScript>();
		collided = false;
		used = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(useEnabled && collided && !used) {
			if(Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.E))
			{
				mainScript.addTime(1000*60*3);               
				used = true;
			}
		}
	}
	
	void OnTriggerEnter(Collider player) {
		if (player.tag.Equals("Player") && !used)
        {
			collided = true;
            if(Constants.useController)
                mainScript.instructions.displaySubtitles("Press 'A' to turn valve");
            else
                mainScript.instructions.displaySubtitles("Press 'E' to turn valve");
		}
	}
	
	void OnTriggerExit(Collider player)
    {
		collided = false;
        mainScript.instructions.hideSubtitles();
    }
}
