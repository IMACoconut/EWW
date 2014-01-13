using UnityEngine;

public class Constants : MonoBehaviour {
	public static float charSpeed = 10f;
	private static float baseSpeed = 10f;
	public static float camDist = 10f;
	private static float baseCamDist = 10f;
	
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
}