using UnityEngine;
using System.Collections;

public class Room1Script : MonoBehaviour {
	
	public float lastAngle, currAngle;
	public int loop;
	public bool updateLoopValue;
	public GameObject player;
	public GameObject doorStart;
	public GameObject doorEnd;
	public GameObject[] cubes;
	public GameObject etagere;
	public GameObject roomCenter;
    public GameObject pillar;
	public bool startRoom;
	
	// Use this for initialization
	void Start () {
		cubes = new GameObject[3];
		cubes[0] = GameObject.Find("FlowerPot1");
        cubes[1] = GameObject.Find("FlowerPot2");
        cubes[2] = GameObject.Find("FlowerPot3");
		doorStart = GameObject.Find("DoorEntry");
		doorEnd = GameObject.Find("DoorExit");
		player = GameObject.Find("Player");
		etagere = GameObject.Find("Etagere");
		roomCenter = GameObject.Find("RoomCenter");
        pillar = GameObject.Find("Pillar");
		startRoom = false;
		lastAngle = Vector3.Angle(player.transform.position, roomCenter.transform.position);
		loop = -1;
		updateLoopValue = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!startRoom)
			return;
        if (Constants.pause)
            return;


        Vector3 pos = player.transform.position - roomCenter.transform.position;
        currAngle = Constants.RealAngle(pos, -roomCenter.transform.right, -roomCenter.transform.forward);
        IncreaseLoop();
        loopUpdate();
		lastAngle = currAngle;
	}
	
	void IncreaseLoop() {
        if (lastAngle > 350 && currAngle < 50)
            loop++;
        else if (currAngle > 350 && lastAngle < 50)
            loop--;
	}

    void loopUpdate()
    {
        if (loop == 0 && currAngle < 20)
        {
            ShowRoom(etagere, false);
        }
        else if (loop == -1 && currAngle > 350)
        {
            ShowRoom(etagere, true);
        }

        if (loop == 2 && currAngle > 350)
        {
            ShowRoom(etagere, false);
        }
        else if (loop == 3 && currAngle < 20)
        {
            ShowRoom(etagere, true);
        }

        for (int i = 0; i < 3; ++i)
        {
            if (loop == i && currAngle > 160)
            {
                cubes[i].renderer.enabled = false;
                cubes[i].collider.enabled = false;
            }
            else if (loop == i && currAngle < 140)
            {
                cubes[i].renderer.enabled = true;
                cubes[i].collider.enabled = true;
            }
        }


        if (loop == -1 && currAngle < 240)
        {
            doorEnd.transform.Find("door").renderer.enabled = true;
            doorEnd.collider.enabled = true;
        }
        else if (loop == -1 && currAngle > 240)
        {
            doorEnd.transform.Find("door").renderer.enabled = false;
            doorEnd.collider.enabled = false;
        }

        if (loop == 2 && currAngle > 240)
        {
            doorEnd.transform.Find("door").renderer.enabled = true;
            doorEnd.collider.enabled = true;
        }
        else if (loop == 2 && currAngle < 240)
        {
            doorEnd.transform.Find("door").renderer.enabled = false;
            doorEnd.collider.enabled = false;
        }


        if (loop == 0 && currAngle < 50)
        {
            doorStart.transform.Find("door").renderer.enabled = true;
            doorStart.collider.enabled = true;
        }
        else if (loop == 0 && currAngle > 50)
        {
            doorStart.transform.Find("door").renderer.enabled = false;
            doorStart.collider.enabled = false;
        }

        if (loop == 3 && currAngle > 50)
        {
            doorStart.transform.Find("door").renderer.enabled = true;
            doorStart.collider.enabled = true;
        }
        else if (loop == 3 && currAngle < 50)
        {
            doorStart.transform.Find("door").renderer.enabled = false;
            doorStart.collider.enabled = false;
        }
    }

    void ShowRoom(GameObject room, bool show)
    {
        Renderer[] childsR = room.GetComponentsInChildren<Renderer>();
        Collider[] childsC = room.GetComponentsInChildren<Collider>();

        foreach (Renderer r in childsR)
            r.enabled = show;
        foreach (Collider c in childsC)
            c.enabled = show;
    }
	
	void OnTriggerEnter() {
        if (startRoom)
            return;
		
        startRoom = true;
		updateLoopValue = true;
		Debug.Log ("start room");
	}
	
	void OnTriggerExit() {
		//updateLoopValue = false;	
	}
}
