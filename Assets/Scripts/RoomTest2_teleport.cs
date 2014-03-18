using UnityEngine;
using System.Collections;

public class RoomTest2_teleport : MonoBehaviour {

    private GameObject player;
    private GameObject teleporter1, teleporter2, target1;

	// Use this for initialization
	void Start () {

        player = GameObject.Find("Player");
        target1 = GameObject.Find("target1");

	
	}
	
	// Update is called once per frame
	void Update () {
        if (this.name == "teleporter2") { this.GetComponentInChildren<Renderer>().material.color = Color.red; }
	
	}

    void OnTriggerEnter(Collider player)
    {
        if (player.tag.Equals("Player")) player.transform.position = target1.transform.position;
    }
}
