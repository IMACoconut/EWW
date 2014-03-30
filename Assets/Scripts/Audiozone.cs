using UnityEngine;
using System.Collections;

public class Audiozone : MonoBehaviour {

    public bool triggered;
    public string audiofile; 

	// Use this for initialization
	void Start () {
        triggered = false; 
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter()
    {
        triggered = true;

    }
}
