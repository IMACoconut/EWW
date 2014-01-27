using UnityEngine;
using System.Collections;

public class RoomGameplayTest : MonoBehaviour {

    public float lastAngle, currAngle;
    public int loop;
    public bool updateLoopValue;
    public GameObject player;
    public GameObject doorStart;
    public GameObject doorEnd;
   
    public GameObject roomCenter;

    public bool startRoom;

    // Use this for initialization
    void Start()
    {
        
        doorStart = GameObject.Find("DoorEntry");
        doorEnd = GameObject.Find("DoorExit");
        player = GameObject.Find("Player");
     
    
        startRoom = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!startRoom)
            return;

       
    }

   

    void OnTriggerEnter()
    {
        if (startRoom)
            return;

        startRoom = true;
        updateLoopValue = true;
        Debug.Log("start room");
    }

    void OnTriggerExit()
    {
        //updateLoopValue = false;	
    }
}
