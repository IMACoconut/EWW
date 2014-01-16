using UnityEngine;
using System.Collections;
using System.Timers;

public class GameScript : MonoBehaviour {
	
	public int roomsDone;
	public int maxRooms;
	public GameObject player;
	public GameObject[] rooms;
	public StreetScript[] streets;
	public GameObject currentLocation;
	public ArrayList generatedStreets;
	public ValveScript valve;
	public ValveScript currentValve;
	public DoorScript door;
	public DoorScript currentDoor;
	
	public CTimer globalTimer;
	
	// Use this for initialization
	void Start () {
		roomsDone = 0;
		maxRooms = 3;
		//player = GameObject.Find("Player");
		EnterRoom();
		globalTimer = new CTimer();
		globalTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
		globalTimer.Interval=1000*60*3;
	    globalTimer.Enabled=true;
		globalTimer.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.Box(new Rect(0,0,120,25), globalTimer.TimeLeft.ToString());
	}
	
	public void LeaveRoom() {
		roomsDone++;
		GameObject.Destroy(currentLocation);
		currentLocation = null;
		
		EnterStreet();
	}
	
	public void LeaveStreet() {
		foreach(StreetScript go in generatedStreets) {
			GameObject.Destroy(go.gameObject);
		}
		GameObject.Destroy(currentValve.gameObject);
		GameObject.Destroy(currentDoor.gameObject);
		generatedStreets.Clear();
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
		pos.y += 2;
		player.transform.position = pos;
	}
	
	void generateStreets() {
		StreetGenerator generator = new StreetGenerator();
		generatedStreets = generator.Generate(this, streets, roomsDone+2);
		player.transform.position = new Vector3(0,2,0);
		Debug.Log("generated "+generatedStreets.Count+" streets");
	}
	
	public void addTime(int time) {
		Debug.Log("addtime");
		globalTimer.Interval = time;
		globalTimer.Start();
		currentDoor.locked = false;
	}
	
	// Specify what you want to happen when the Elapsed event is raised.
	private void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		Debug.Log("Timeout noob!");
		globalTimer.Stop();
	}
}
