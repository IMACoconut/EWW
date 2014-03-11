using UnityEngine;
using System.Collections;

public class RoomTest2_teleport : MonoBehaviour {

    private GameObject player;
    private GameObject teleporter1, teleporter2;

	// Use this for initialization
	void Start () {

        player = GameObject.Find("Player");
        teleporter2 = GameObject.Find("teleporter2");

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider player)
    {
        if (player.tag.Equals("Player")) player.transform.position = teleporter2.transform.position;
    }
}
