using UnityEngine;

public class Constants : MonoBehaviour {
	public static float charSpeed = 10f;
	private static float baseSpeed = 10f;
	public static float camDist = 10f;
<<<<<<< HEAD
	private static float baseCamDist = 10f;


       
=======
    public static float camHeight = 8.5f;
	private static float baseCamDist = 10f;
>>>>>>> origin/master
	
	private static bool debugMode = false;
	
	void Update() {
		if(Input.GetKeyDown(KeyCode.F2))
			EnableDebug(!debugMode);
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

<<<<<<< HEAD

 
=======
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
>>>>>>> origin/master
}