using UnityEngine;

public class Constants : MonoBehaviour {
	public static float charSpeed = 10f;
	private static float baseSpeed = 10f;
	public static float camDist = 10f;

    public static float camHeight = 8.5f;
	private static float baseCamDist = 10f;
    public static float cityScale = 1f/0.6f;

    public static float camRotateSpeed = 2f;
    private static bool street = false;

    private static GameScript generalScript;
    public static bool pause = false;
	
	public static bool debugMode = false;
    public static float sensitivity = 2.0f;
    public static float volume = 1.0f;
    public static bool invertCamera = false;
    public static bool useController = true;

    public static float worldScale = 25.6f;
    
    

  /*  public enum Action
    {
        Pause   = 0,
        Use     = 1,
        Jump    = 2,
        Aim     = 3,
        Left    = 4,
        Up      = 5,
        Down    = 6,
        Right   = 7
    }

    private struct ActionMap
    {
        virtual bool isDown() { return false; };
    }

    private struct ControllerMap : ActionMap
    {
        public string button;
        public bool isDown() {
            return Input.GetButtonDown(button);
        }
    }
   */


    void Start()
    {
        generalScript = GameObject.Find("GameGeneralScript").GetComponent<GameScript>();
        
    }
	
	void Update() {
        if (Constants.pause)
            return;

		if(Input.GetKeyDown(KeyCode.F2))
			EnableDebug(!debugMode);

        if (Input.GetKeyDown(KeyCode.P))
            NextStep();
	}
	
	void EnableDebug(bool b) {
		Debug.Log("Enable debug mode");
		if(b) {
			charSpeed = baseSpeed*10f;
			camDist = baseCamDist*15f;
		} else {
			charSpeed = baseSpeed;
			camDist = baseCamDist;
		}
		debugMode = b;
	}


    public static float RealAngle(Vector3 a, Vector3 b, Vector3 c)
    {
        int scal = 0;
        if (Vector3.Dot(a, c) < 0)
            scal = 1;

        if (scal == 0)
            return Vector3.Angle(a, b);
        else
            return 360 - Vector3.Angle(a, b);
    }

    public static void NextStep()
    {
        if (street)
            generalScript.LeaveStreet();
        else
            generalScript.LeaveRoom();

        street = !street;
    }

   /* public bool GetButton(Button b)
    {
        if (useController)
        {
            return controllerMapping[(int)b];
        }
        else
        {
            return mouseKeyboardMapping[(int)b];
        }
    }*/

}