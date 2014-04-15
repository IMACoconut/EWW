using UnityEngine;
using System.Collections;

public class LightRoom1Script : MonoBehaviour
{
    SoundBankManager SoundBank; 
    public float lastAngle, currAngle;
    public int loop;
    public bool updateLoopValue;
    private GameObject alert; 
    public GameObject player;
    public GameObject doorStart;
    public GameObject doorEnd;
    public GameObject[] Lights;
    public GameObject bulblight;
    public GameObject roomCenter;
    private Light bulb; 
    public bool startRoom;
    bool solved;
    bool step = true ;
    bool played = false; 
    private float alertTime = 0f; 

    // Use this for initialization
    void Start()
    {
        /* cubes = new GameObject[3];
        cubes[0] = GameObject.Find("Cube1");
        cubes[1] = GameObject.Find("Cube2");
        cubes[2] = GameObject.Find("Cube3"); */

        Lights = new GameObject[7];
        Lights[0] = GameObject.Find("Light1");
        Lights[1] = GameObject.Find("Light2");
        Lights[2] = GameObject.Find("Light3");
        Lights[3] = GameObject.Find("Light4");
        Lights[4] = GameObject.Find("Light5");
        Lights[5] = GameObject.Find("Light6");
        Lights[6] = GameObject.Find("Light7");

        doorStart = GameObject.Find("DoorEntry");
        alert = GameObject.Find("alert");
        bulblight = GameObject.Find("bulblight");
        bulb = bulblight.GetComponentInChildren<Light>();
        bulb.color = Color.red;
        doorEnd = GameObject.Find("DoorExit");
        doorEnd.collider.enabled = false;
        
        player = GameObject.Find("Player");
        //player.transform.Translate(-15, 0, 0);
        //player.transform.Rotate(0 , -90 , 0);
        roomCenter = GameObject.Find("RoomCenter");
        startRoom = false;
        lastAngle = 0;//Vector3.Angle(player.transform.position, roomCenter.transform.position);
        loop = 0;
        updateLoopValue = false;
        solved = false;
        SoundBank = GameObject.Find("GameGeneralScript").GetComponent<SoundBankManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!startRoom)
            return;

        solved = isSolved();

        if (solved) { doorEnd.collider.enabled = true;
        bulb.color = Color.green;  
            
        }

        alertTime += Time.deltaTime;
        if (alertTime > 4f && !played) { Alert(); played = true;  }

       
        

        
    }

    void Alert()
    {
        string tmp = "we are sorry to announce"; 
        step = false;
        alert.audio.clip = SoundBank.SoundBank[tmp];
        alert.audio.Play();
        step = true;

    }

    bool isSolved()
    {
        if (Lights[0].GetComponent<LightRoom1Script_Light>().off == false &&
            Lights[1].GetComponent<LightRoom1Script_Light>().off == true &&
            Lights[2].GetComponent<LightRoom1Script_Light>().off == false &&
            Lights[3].GetComponent<LightRoom1Script_Light>().off == true &&
            Lights[4].GetComponent<LightRoom1Script_Light>().off == true &&
            Lights[5].GetComponent<LightRoom1Script_Light>().off == false &&
            Lights[6].GetComponent<LightRoom1Script_Light>().off == false)
        {
            //Debug.Log("Problem solved"); //table 1, table 3, table 6, table 7
            return true;
        }
        else {// Debug.Log("Problem not solved, noob");  
            return false; }
             

    }


    
    void OnTriggerEnter()
    {
        startRoom = true;
        updateLoopValue = true;
        //Debug.Log("start room");
    }

    void OnTriggerExit()
    {
        //updateLoopValue = false;	
    }

}
