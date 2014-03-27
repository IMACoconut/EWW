using UnityEngine;
using System.Collections;

public class RoomTest2 : MonoBehaviour {

    public bool startRoom;
 
    // Use this for initialization
    void Start()
    {
     
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

    }
}
