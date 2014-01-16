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
	public bool startRoom;
	
	// Use this for initialization
	void Start () {
		cubes = new GameObject[3];
		cubes[0] = GameObject.Find("Cube1");
		cubes[1] = GameObject.Find("Cube2");
		cubes[2] = GameObject.Find("Cube3");
		doorStart = GameObject.Find("DoorEntry");
		doorEnd = GameObject.Find("DoorExit");
		player = GameObject.Find("Player");
		etagere = GameObject.Find("Etagere");
		roomCenter = GameObject.Find("RoomCenter");
		startRoom = false;
		lastAngle = 0;//Vector3.Angle(player.transform.position, roomCenter.transform.position);
		loop = 0;
		updateLoopValue = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!startRoom)
			return;

		currAngle = Vector3.Angle(player.transform.position, roomCenter.transform.position);
		
		if(updateLoopValue) {
			if(currAngle > lastAngle) {
				
			} else {
				
			}
			
		}
		//lastAngle = currAngle;
	}
	
	void IncreaseLoop() {
		
	}
	
	void OnTriggerEnter() {
		startRoom = true;
		updateLoopValue = true;
		Debug.Log ("start room");
	}
	
	void OnTriggerExit() {
		//updateLoopValue = false;	
	}
}
