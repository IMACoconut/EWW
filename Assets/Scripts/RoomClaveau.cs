using UnityEngine;
using System.Collections;

public class RoomClaveau : Room {
    public bool endgame;
	// Use this for initialization
	void Start () {
        endgame = false;  
	}
	
	// Update is called once per frame
	void Update () {
        endgame = this.GetComponentInChildren<ValveScript>().used;
        if (endgame) { Application.LoadLevel("end"); }
       
        
	}
}
