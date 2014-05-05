using UnityEngine;
using System.Collections;
using System.Timers;
using System.Collections.Generic;
using Assets;

public class GameScript : MonoBehaviour {

    public int roomsDone = 0;
	public int maxRooms = 4;

	public BallPlayer player;

    public Room dortoir;
    public Room[] rooms;

	public StreetScript[] streets;
	
	public GameObject door;
	
    public GameObject[] posters;
    public StreetRule[] rules;
    public FadeInOut ScreenFader;  
	
	public RealTimer globalTimer;

    protected DoorScript doorScript;
    protected ValveScript valveScript;

    public GameObject currentDoor;
    public Room currentLocation;
    private List<StreetScript> generatedStreets;

    public Compteur compteur;

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
        beginIntro();

        ScreenFader = GameObject.Find("ScreenFader").GetComponent<FadeInOut>();

        Constants.pause = false;
        initialSize = player.transform.localScale;
        m_menu = Menu.Main;
        Screen.lockCursor = true;
        compteur.SetTime(1000*3*60);
        
	}

    void beginIntro()
    {
        player.LoadAudio = true;
        player.clearAudio = true;
        currentLocation = GameObject.Instantiate(dortoir) as Room;
        currentLocation.setGameScript(this);
        currentLocation.started = true;
        currentLocation.player = player.gameObject;

        Vector3 pos = currentLocation.start.transform.position;
        pos.y += 2;
        player.transform.position = pos;
        player.transform.localRotation = currentLocation.start.transform.localRotation;
    }

    void beginGame()
    {
        player.LoadAudio = true;
        EnterRoom();
        globalTimer.delay = 1000 * 60 * 3;
        globalTimer.Start();
    }

	void reshuffle()
	{
		
		// Knuth shuffle algorithm :: courtesy of Wikipedia :)
		
		for (int t = 0; t < rooms.Length; t++ )
			
		{
			
			Room tmp = rooms[t];
			
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
        if (!Constants.pause)
            return;
        else if (m_menu == Menu.Main)
        {
            int w = Screen.width / 2;
            int h = Screen.height / 2;
            if (GUI.Button(new Rect(w - 75, h - 60, 150, 40), "Resume"))
                Resume();
            if (GUI.Button(new Rect(w - 75, h - 10, 150, 40), "Options"))
                m_menu = Menu.Options;
            if (GUI.Button(new Rect(w - 75, h + 70, 150, 40), "Back to main menu"))
                Application.LoadLevel("menu");
        }
        else if (m_menu == Menu.Options)
        {
            int w = Screen.width / 2;
            int h = Screen.height / 2;
            Constants.useController = GUI.Toggle(new Rect(w - 80, h - 110, 80, 20), Constants.useController, "Controller");
            Constants.useController = !GUI.Toggle(new Rect(w, h - 110, 75, 20), !Constants.useController, "Mouse");

            GUI.Label(new Rect(w - 75, h - 80, 150, 40), "Sensitivity");
            Constants.sensitivity = GUI.HorizontalSlider(new Rect(w - 75, h - 60, 150, 20), Constants.sensitivity, .1f, 3.0f);
            GUI.Label(new Rect(w + 80, h - 65, 80, 40), "" + Constants.sensitivity);

            Constants.invertCamera = GUI.Toggle(new Rect(w - 75, h - 45, 150, 20), Constants.invertCamera, "Inverse camera");

            GUI.Label(new Rect(w - 75, h - 10, 150, 40), "Volume");
            Constants.volume = GUI.HorizontalSlider(new Rect(w - 75, h + 10, 150, 40), Constants.volume, 0f, 1f);
            GUI.Label(new Rect(w + 80, h + 5, 80, 40), "" + Constants.volume);

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
            currentLocation.started = false;
			GameObject.Destroy(currentLocation.gameObject);
			currentLocation = null;
            addTime(1000 * 60 * 3);
			EnterRoom();
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
        player.LoadAudio = true;
        player.clearAudio = true;
        Debug.Log("enterstreet");
		generateStreets();

        
        //player.transform.localScale = initialSize*0.60f;

	}
	
	void EnterRoom() {
        player.LoadAudio = true;
        player.clearAudio = true;
        ScreenFader.sceneStarting = true; 
		int re = Random.Range(0, rooms.Length);
		currentLocation = GameObject.Instantiate(rooms[re]) as Room;
        currentLocation.started = true;
        Vector3 pos = currentLocation.start.transform.position;

       // Vector3 forw = currentLocation.transform.Find("StartPointScript").transform.right;
		pos.y += 2;
		player.transform.position = pos;
        player.transform.localRotation = currentLocation.start.transform.localRotation;
        currentLocation.player = player.gameObject;
        currentLocation.setGameScript(this);
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
