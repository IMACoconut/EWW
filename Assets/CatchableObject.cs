using UnityEngine;
using System.Collections;

public class CatchableObject : MonoBehaviour {

    public bool taken;
    private GameObject text;
	// Use this for initialization
	void Start () {
        taken = false;
        text = GameObject.FindGameObjectWithTag("LabelTake");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider player)
    {
        if (player.tag.Equals("Player") && !taken)
        {
            player.GetComponent<BallPlayer>().canBeTaken = this;
            text.guiText.text = "Press 'E' to take";
            text.guiText.enabled = true;
        }
    }

    void OnTriggerExit(Collider player)
    {
        ClearMessage();
    }

    public void ClearMessage()
    {
        text.guiText.text = "";
        text.guiText.enabled = false;
    }

    public void Take(BallPlayer taker)
    {
        taker.objectTaken = this;
        taker.canBeTaken = null;
        taken = true;
        transform.parent = taker.transform;
        //rigidbody.detectCollisions = false;
        //rigidbody.mass = 0.001f;
        rigidbody.isKinematic = true;
        //GetComponents<Collider>()[0].enabled = false;
        ClearMessage();
    }

    public void Drop(BallPlayer player)
    {
        player.objectTaken = null;
        player.canBeTaken = null;
        transform.parent = null;
        taken = false;
        //rigidbody.mass = mass;
        //rigidbody.detectCollisions = true;
        rigidbody.isKinematic = false;
        //GetComponents<Collider>()[0].enabled = true;
    }
}
