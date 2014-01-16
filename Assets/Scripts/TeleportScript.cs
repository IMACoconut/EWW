using UnityEngine;
using System.Collections;

public class TeleportScript : MonoBehaviour {

    public TeleportScript to;
    public bool justTp;
	// Use this for initialization
	void Start () {
        justTp = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider player)
    {
        if (player.tag.Equals("Player") && !justTp)
        {
            Debug.Log("teleport");
            Vector3 delta = transform.position - player.transform.position;
            player.transform.position = to.transform.position - delta;
            to.justTp = true;
        }
    }

    void OnTriggerExit(Collider player)
    {
        justTp = false;
    }
}
