using UnityEngine;
using System.Collections;

public class RoomTest2 : MonoBehaviour {

    public bool startRoom;
    private GameObject player;
    private GameObject roomCenter, roomCenter2;

    public int loop1, loop2;
    public float currAngle1, lastAngle1, currAngle2, lastAngle2;

    private GameObject firstRoom, secondRoom, passage1, passage2, passage3, passage4, passage5;

    // Use this for initialization
    void Start()
    {
        firstRoom = GameObject.Find("FirstRoom");
        secondRoom = GameObject.Find("SecondRoom");

        ShowRoom(firstRoom, false);
        ShowRoom(secondRoom, true);

        player = GameObject.Find("Player");
        roomCenter = GameObject.Find("RoomCenter");
        roomCenter2 = GameObject.Find("RoomCenter2");
        currAngle1 = 0;
        lastAngle1 = 0;
        loop1 = -1;
        currAngle2 = 0;
        lastAngle2 = 0;
        loop2 = -1;
    }

    void ShowRoom(GameObject room, bool show)
    {
        Renderer[] childsR = room.GetComponentsInChildren<Renderer>();
        Collider[] childsC = room.GetComponentsInChildren<Collider>();

        foreach (Renderer r in childsR)
            r.enabled = show;
        foreach (Collider c in childsC)
            c.enabled = show;
    }

    // Update is called once per frame
    void Update()
    {
        if (Constants.pause)
            return;

        if (!startRoom)
            return;

        Vector3 pos = player.transform.position - roomCenter.transform.position;
        currAngle1 = Constants.RealAngle(pos, -roomCenter.transform.right, -roomCenter.transform.forward);
        pos = player.transform.position - roomCenter2.transform.position;
        currAngle2 = Constants.RealAngle(pos, -roomCenter2.transform.right, -roomCenter2.transform.forward);
        IncreaseLoop();
        UpdateLoop();
        lastAngle1 = currAngle1;
        lastAngle2 = currAngle2;

        //Debug.Log(loop1 + "  " + currAngle1 + "  " + loop2 + "  " + currAngle2 + "  ");
    }

    void UpdateLoop()
    {
        if (loop1 == 0 && currAngle1 > 10 && currAngle1 < 20)
        {
            ShowRoom(firstRoom, true);
            ShowRoom(secondRoom, false);
        }
        else if (loop1 == 0 && currAngle1 < 10)
        {
            ShowRoom(firstRoom, false);
            ShowRoom(secondRoom, true);
        }

        if (loop1 == 0 && currAngle1 > 70 && currAngle1 < 80)
        {
            ShowRoom(secondRoom, false);
            ShowRoom(firstRoom, true);
            
        }
        else if (loop1 == 0 && currAngle1 < 70 && currAngle1 > 60)
        {
            ShowRoom(secondRoom, true);
            ShowRoom(firstRoom, false);
            
        }

        if (loop2 == -1 && currAngle2 > 280 && currAngle2 < 290)
        {
           
        }
        else if (loop2 == -1 && currAngle2 < 280 && currAngle2 > 270)
        {
            
        }

        if (loop2 == 0 && currAngle2 < 10)
        {
            
        }
        else if (loop2 == -1 && currAngle2 > 350)
        {
            
        }
        
    
    }

    void IncreaseLoop()
    {
        if (lastAngle1 > 350 && currAngle1 < 50)
            loop1++;
        else if (currAngle1 > 350 && lastAngle1 < 50)
            loop1--;

        if (lastAngle2 > 350 && currAngle2 < 50)
            loop2++;
        else if (currAngle2 > 350 && lastAngle2 < 50)
            loop2--;
    }

    void OnTriggerEnter()
    {
        if (startRoom)
            return;
        startRoom = true;
        Debug.Log("start room");
    }
}
