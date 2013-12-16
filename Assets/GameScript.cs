using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {
	
	public int roomsDone;
	public int maxRooms;
	public GameObject player;
	public GameObject[] rooms;
	public StreetScript[] streets;
	public GameObject currentLocation;
	public ArrayList generatedStreets;
	// Use this for initialization
	void Start () {
		roomsDone = 0;
		maxRooms = 3;
		player = GameObject.Find("Player");
		EnterRoom();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void LeaveRoom() {
		roomsDone++;
		GameObject.Destroy(currentLocation);
		currentLocation = null;
		
		EnterStreet();
	}
	
	public void LeaveStreet() {
		foreach(StreetScript go in generatedStreets) {
			Destroy(go);
		}
		generatedStreets = null;

		EnterRoom();
	}
	
	void EnterStreet() {
		generateStreets();
	}
	
	void EnterRoom() {
		Debug.Log("enter room");
		int re = Random.Range(0, rooms.GetLength(0));
		currentLocation = GameObject.Instantiate(rooms[re]) as GameObject;

		Vector3 pos = currentLocation.transform.Find("StartPointScript").transform.position;
		pos.y += 5;
		player.transform.position = pos;
	}
	
	void generateStreets() {
		StreetGenerator generator = new StreetGenerator();
		
		generatedStreets = generator.Generate(streets, 3);
	}
}
