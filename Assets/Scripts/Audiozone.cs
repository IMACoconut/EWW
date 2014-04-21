using UnityEngine;
using System.Collections;

public class Audiozone : MonoBehaviour {

    public bool triggered;
    public string audiofile;
    public bool alert; 

	// Use this for initialization
	void Start () {
        triggered = false; 
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            triggered = true; 
        }

    }
}
