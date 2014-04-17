using UnityEngine;
using System.Collections;

public class RoomTest2 : Room {

    public bool startRoom;
    BallPlayer Player;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<BallPlayer>();
    }


    // Update is called once per frame
    void Update()
    {
       
    }

    

    void OnTriggerEnter()
    {
        if (startRoom)
            return;
        startRoom = true;
        Debug.Log("start room");
        Player.LoadAudio = false; 

    }
}
