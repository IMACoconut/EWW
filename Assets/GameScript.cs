using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {
	
	public int roomsDone;
	public int maxRooms;
	public GameObject player;
	public GameObject[] rooms;
	public StreetScript[] streets;
	public GameObject currentLocation;
	public StreetScript[] generatedStreets;
	// Use this for initialization
	void Start () {
		roomsDone = 0;
		maxRooms = 3;
		player = GameObject.Find("Player");
		/*rooms = GameObject.FindGameObjectsWithTag("Room");
		foreach(GameObject go in rooms) {
			Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
				foreach(Renderer r in renderers)
					r.enabled = false;
		}
		streets = GameObject.FindGameObjectsWithTag("Street");
		foreach(GameObject go in streets) {
			Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
				foreach(Renderer r in renderers)
					r.enabled = false;
		}*/
		EnterRoom();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void LeaveRoom() {
		roomsDone++;
		Renderer[] renderers = currentLocation.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in renderers)
			r.enabled = false;
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
		/*Debug.Log("enter street");
		
		int s = Random.Range(0, streets.GetLength(0));
		currentLocation = streets[s];

		Renderer[] renderers = currentLocation.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in renderers)
			r.enabled = true;
		
		Vector3 pos = currentLocation.transform.Find("StartPointScript").transform.position;
		pos.y += 10;
		player.transform.position = pos;*/
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
		Vector3 startPos = new Vector3(0,0,0);
		generatedStreets = new StreetScript[5*5];
		while(generatedStreets[3] == null) {
			int s = Random.Range(0, streets.GetLength(0));
			//StreetScript tmp = GameObject.Instantiate(streets[s]) as StreetScript;
			if(streets[s].getPathsCount() < 2) {
				//Destroy(tmp);
				continue;			
			}
			
			generatedStreets[3] = GameObject.Instantiate(streets[s]) as StreetScript;//tmp;
			generatedStreets[3].transform.position = startPos + new Vector3(3*25.6f*3, 0f, 0*25.6f*3);
			
		};
		for(int i = 0; i<5; ++i) {
			for(int j = 0; j<5; ++j) {
				if(generatedStreets[i*5+j] != null)
					continue;
				
				int s = Random.Range(0, streets.GetLength(0));
				generatedStreets[i] = GameObject.Instantiate(streets[s]) as StreetScript;
				generatedStreets[i].transform.position = startPos + new Vector3(i*25.6f*3, 0f, j*25.6f*3);
				generatedStreets[i].transform.parent = transform;
			}
		}
		Vector3 pos = generatedStreets[3].transform.Find("StartPointScript").transform.position;
		pos.y += 10;
		player.transform.position = pos;
	}
	
}
