using UnityEngine;
using System.Collections;

public class Room2Script : MonoBehaviour
{

    public float lastAngle, currAngle;
    public int loop;
    public bool updateLoopValue;
    public GameObject player;
    public GameObject doorStart;
    public GameObject doorEnd;
    public GameObject[] tables;
    public GameObject etagere;
    public GameObject roomCenter;
    public bool startRoom;
    bool solved;

    // Use this for initialization
    void Start()
    {
        /* cubes = new GameObject[3];
        cubes[0] = GameObject.Find("Cube1");
        cubes[1] = GameObject.Find("Cube2");
        cubes[2] = GameObject.Find("Cube3"); */

        tables = new GameObject[7];
        tables[0] = GameObject.Find("Table1");
        tables[1] = GameObject.Find("Table2");
        tables[2] = GameObject.Find("Table3");
        tables[3] = GameObject.Find("Table4");
        tables[4] = GameObject.Find("Table5");
        tables[5] = GameObject.Find("Table6");
        tables[6] = GameObject.Find("Table7");

        doorStart = GameObject.Find("DoorEntry");

        doorEnd = GameObject.Find("DoorExit");
        doorEnd.collider.enabled = false;

        player = GameObject.Find("Player");
        etagere = GameObject.Find("Etagere");
        roomCenter = GameObject.Find("RoomCenter");
        startRoom = false;
        lastAngle = 0;//Vector3.Angle(player.transform.position, roomCenter.transform.position);
        loop = 0;
        updateLoopValue = false;
        solved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startRoom)
            return;

        solved = isSolved();

        if (solved) doorEnd.collider.enabled = true;
     

        

        
    }

    bool isSolved()
    {
        if (tables[0].GetComponent<Room2Script_Table>().off == false &&
            tables[1].GetComponent<Room2Script_Table>().off == true &&
            tables[2].GetComponent<Room2Script_Table>().off == false &&
            tables[3].GetComponent<Room2Script_Table>().off == true &&
            tables[4].GetComponent<Room2Script_Table>().off == true &&
            tables[5].GetComponent<Room2Script_Table>().off == false &&
            tables[6].GetComponent<Room2Script_Table>().off == false)
        {
            Debug.Log("Problem solved"); //table 1, table 3, table 6, table 7
            return true;
        }
        else {// Debug.Log("Problem not solved, noob");  
            return false; }
             

    }


    
    void OnTriggerEnter()
    {
        startRoom = true;
        updateLoopValue = true;
        Debug.Log("start room");
    }

    void OnTriggerExit()
    {
        //updateLoopValue = false;	
    }

}
