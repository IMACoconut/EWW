using UnityEngine;
using System.Collections;

public class EndStreetScript : MonoBehaviour {

	public GameScript mainScript;
	
	// Use this for initialization
	void Start () {
		mainScript = GameObject.Find("GameGeneralScript").GetComponent<GameScript>();
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerEnter() {
		Debug.Log("end street");
		mainScript.LeaveStreet();
	}
}
