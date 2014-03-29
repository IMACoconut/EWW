using UnityEngine;
using System.Collections;

public class RoomTest2_teleport : MonoBehaviour {

    private GameObject player;
    private GameObject target1, target1bis, target2, target2bis, target3, target3bis;
    private GameObject switch1, switch2, switch3; 

	// Use this for initialization
	void Start () {

        player = GameObject.Find("Player");
        target1 = GameObject.Find("target1");
        target2 = GameObject.Find("target2");
        target3 = GameObject.Find("target3");
        target1bis = GameObject.Find("target1bis");
        target2bis = GameObject.Find("target2bis");
        target3bis = GameObject.Find("target3bis");

        switch1 = GameObject.Find("switch1");
        switch2 = GameObject.Find("switch2");
        switch3 = GameObject.Find("switch3");

	
	}
	
	// Update is called once per frame
	void Update () {

        if (this.name == "teleporter1")
        {
            if (switch2.GetComponent<RoomTest2_switch>().on) this.GetComponentInChildren<Renderer>().material.color = Color.red;
            else this.GetComponentInChildren<Renderer>().material.color = Color.cyan;
        }
        if (this.name == "teleporter2")
        {
            if (switch1.GetComponent<RoomTest2_switch>().on) this.GetComponentInChildren<Renderer>().material.color = Color.red;
            else this.GetComponentInChildren<Renderer>().material.color = Color.cyan; 
        }
        if (this.name == "teleporter3")
        {
            if (switch3.GetComponent<RoomTest2_switch>().on) this.GetComponentInChildren<Renderer>().material.color = Color.red;
            else this.GetComponentInChildren<Renderer>().material.color = Color.cyan;
        }
	
	}

    void OnTriggerEnter(Collider player)
    {
        if (player.tag.Equals("Player"))
        {
            if (this.name == "teleporter1") {
                if (switch2.GetComponent<RoomTest2_switch>().on) player.transform.position = target1bis.transform.position;
                else player.transform.position = target1.transform.position;
            }
            else if (this.name == "teleporter2") {
                if (switch1.GetComponent<RoomTest2_switch>().on) player.transform.position = target2bis.transform.position;
                else player.transform.position = target2.transform.position;
            }
            else if (this.name == "teleporter3")
            {
                if (switch3.GetComponent<RoomTest2_switch>().on) player.transform.position = target3bis.transform.position;
                else player.transform.position = target3.transform.position;
            }
        }
        


    }
}
