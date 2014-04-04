using UnityEngine;
using System.Collections;
using System.Timers;
using System.Collections.Generic;
using Assets;

public class GameScript : MonoBehaviour {
	
	public int roomsDone;
	public int maxRooms;
	public GameObject player;
    private BallPlayer Player; 
	public GameObject[] rooms;
	public StreetScript[] streets;
	public GameObject currentLocation;
    public List<StreetScript> generatedStreets;
	public GameObject door;
	public GameObject currentDoor;
    public GameObject[] posters;
    public StreetRule[] rules;
    public FadeInOut ScreenFader;  
	
	public RealTimer globalTimer;

    protected DoorScript doorScript;
    protected ValveScript valveScript;

    private enum Menu {
        Main,
        Options
    };

    private Vector3 initialSize;
    private Menu m_menu;
	// Use this for initialization
    void Awake()
    {
        globalTimer = new RealTimer();
        globalTimer.elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
		reshuffle();
    }
	void Start () {
		roomsDone = 0;
		maxRooms = 4;
		//player = GameObject.Find("Player");
        Player = GameObject.Find("Player").GetComponent<BallPlayer>();
        ScreenFader = GameObject.Find("ScreenFader").GetComponent<FadeInOut>();
        Constants.pause = false;
		EnterRoom();
        globalTimer.delay = 1000 * 60 * 3;
		globalTimer.Start();

        initialSize = player.transform.localScale;
        m_menu = Menu.Main;
        Screen.lockCursor = true;
        
	}

	void reshuffle()
	{
		
		// Knuth shuffle algorithm :: courtesy of Wikipedia :)
		
		for (int t = 0; t < rooms.Length; t++ )
			
		{
			
			GameObject tmp = rooms[t];
			
			int r = Random.Range(t, rooms.Length);
			
			rooms[t] = rooms[r];
			
			rooms[r] = tmp;
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {
        globalTimer.Update();
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            Screen.lockCursor = false;
            if (!globalTimer.IsPaused())
            {
                Pause();
            }
        }
	}

    void Resume()
    {
        globalTimer.Resume();
        Constants.pause = false;
        Screen.lockCursor = true;
    }

    void Pause()
    {
        globalTimer.Pause();
        Constants.pause = true;
        Screen.lockCursor = false;
    }

	
	void OnGUI() {
        if(!Constants.pause)
    		GUI.Box(new Rect(0,0,120,25), globalTimer.TimeLeft().ToString());
        else if(m_menu == Menu.Main)
        {
            int w = Screen.width/2;
            int h = Screen.height/2;
            if (GUI.Button(new Rect(w-75, h-60, 150, 40), "Resume"))
                Resume();
            if (GUI.Button(new Rect(w - 75, h - 10, 150, 40), "Options"))
                m_menu = Menu.Options;
            if (GUI.Button(new Rect(w-75, h+70, 150, 40), "Back to main menu"))
                Application.LoadLevel("menu");
        }
        else if (m_menu == Menu.Options)
        {
            int w = Screen.width/2;
            int h = Screen.height/2;
            Constants.useController = GUI.Toggle(new Rect(w-80, h-110, 80, 20), Constants.useController, "Controller");
            Constants.useController = !GUI.Toggle(new Rect(w, h - 110, 75, 20), !Constants.useController, "Mouse");

            GUI.Label(new Rect(w-75, h-80, 150,40), "Sensitivity");
            Constants.sensitivity = GUI.HorizontalSlider(new Rect(w-75, h-60,150,20), Constants.sensitivity, .1f,3.0f);
            GUI.Label(new Rect(w + 80, h - 65, 80, 40), "" + Constants.sensitivity);
            
            Constants.invertCamera = GUI.Toggle(new Rect(w - 75, h - 45, 150, 20), Constants.invertCamera, "Inverse camera");
            
            GUI.Label(new Rect(w - 75, h - 10, 150, 40), "Volume");
            Constants.volume = GUI.HorizontalSlider(new Rect(w - 75, h+10, 150, 40), Constants.volume, 0f, 1f);
            GUI.Label(new Rect(w + 80, h+5, 80, 40), "" + Constants.volume);

            if (GUI.Button(new Rect(w - 75, h + 70, 150, 40), "Back"))
                m_menu = Menu.Main;
        }
    }
	
	public void LeaveRoom() {
        Debug.Log("leaveroom");
		roomsDone++;
		if(roomsDone >= maxRooms) {
			Application.LoadLevel("menu");
		}
		else {
			GameObject.Destroy(currentLocation);
			currentLocation = null;
            
			EnterStreet();
		}
	}
	
	public void LeaveStreet() {
		foreach(StreetScript go in generatedStreets) {
            go.OnDestroy();
			GameObject.Destroy(go.gameObject);
		}
		GameObject.Destroy(currentDoor.gameObject);
		generatedStreets.Clear();
		generatedStreets = null;


        //player.transform.localScale = initialSize;
		EnterRoom();
	}
	
	void EnterStreet() {
        ScreenFader.sceneStarting = true; 
        Player.LoadAudio = true; 
        Debug.Log("enterstreet");
		generateStreets();

        
        //player.transform.localScale = initialSize*0.60f;

	}
	
	void EnterRoom() {
        Player.LoadAudio = true;
        ScreenFader.sceneStarting = true; 
		int re = Random.Range(0, rooms.Length);
		currentLocation = GameObject.Instantiate(rooms[re]) as GameObject;

		Vector3 pos = currentLocation.transform.Find("StartPointScript").transform.position;

       // Vector3 forw = currentLocation.transform.Find("StartPointScript").transform.right;
		pos.y += 2;
		player.transform.position = pos;
        player.transform.localRotation = currentLocation.transform.Find("StartPointScript").transform.localRotation;
        

	}
	
	void generateStreets() {
		StreetGenerator generator = new StreetGenerator();
		generatedStreets = generator.Generate(this, streets, 2);
		player.transform.position = new Vector3(0,2,0);
		Debug.Log("generated "+generatedStreets.Count+" streets");
	}
	
	public void addTime(int time) {
        globalTimer.delay = time;
		globalTimer.Start();
		doorScript.locked = false;
	}
	
	// Specify what you want to happen when the Elapsed event is raised.
	private void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		Debug.Log("Timeout noob!");
		globalTimer.Stop();
	}

    public void teleportPlayer(GameObject to)
    {
        player.transform.position = to.transform.position;
        player.transform.localRotation = to.transform.localRotation;
    }

    public GameObject getRandomPoster()
    {
        int r = Random.Range(0, posters.Length);

        return GameObject.Instantiate(posters[r]) as GameObject;
    }

    public void setCurrentDoor(GameObject d)
    {
        currentDoor = d;
        doorScript = currentDoor.GetComponentInChildren<DoorScript>();
        valveScript = currentDoor.GetComponentInChildren<ValveScript>();
    }

    public DoorScript getCurrentDoor()
    {
        return doorScript;
    }

    public ValveScript getCurrentValve()
    {
        return valveScript;
    }
}
