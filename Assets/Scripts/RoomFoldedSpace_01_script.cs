using UnityEngine;
using System.Collections;

public class RoomFoldedSpace_01_script : MonoBehaviour {

    public bool startRoom;
    private GameObject player;
    private GameObject roomCenter, roomCenter2;

    public int loop1, loop2;
    public float currAngle1, lastAngle1, currAngle2, lastAngle2;

    private GameObject firstHalf, firstRoom, secondRoom, passage1, passage2;

    // Use this for initialization
	void Start () {
        startRoom = false;
        Restart();
    }

    public void Restart() {
        

        firstHalf = GameObject.Find("FirstHalf");
        firstRoom = GameObject.Find("FirstRoom");
        secondRoom = GameObject.Find("SecondRoom");
        passage1 = GameObject.Find("Passage1");
        passage2 = GameObject.Find("Passage2");
        ShowRoom(passage1, true);
        ShowRoom(passage2, true);
        ShowRoom(firstRoom, true);
        ShowRoom(firstHalf, false);
        ShowRoom(secondRoom, false);        
        
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
	void Update () {
        if (!startRoom)
            return;

        Vector3 pos = player.transform.position - roomCenter.transform.position;
        currAngle1 = Constants.RealAngle(pos, -roomCenter.transform.right, -roomCenter.transform.forward);
        pos = player.transform.position - roomCenter2.transform.position;
        currAngle2 = Constants.RealAngle(pos, -roomCenter2.transform.right, -roomCenter2.transform.forward);
        IncreaseLoop();
        UpdateLoop();
        Debug.Log(lastAngle1);
        Debug.Log(currAngle1);
        Debug.Log(loop1);
        lastAngle1 = currAngle1;
        lastAngle2 = currAngle2;
    }

    void UpdateLoop()
    {
        if (loop1 == 0 && currAngle1 > 10 && currAngle1 < 20)
        {
            ShowRoom(passage1, false);
        }
        else if (loop1 == 0 && currAngle1 < 10)
        {
            ShowRoom(passage1, true);
        }

        if (loop1 == 0 && currAngle1 > 80 && currAngle1 < 90)
        {
            ShowRoom(firstHalf, true);
            ShowRoom(passage2, false);
        }
        else if (loop1 == 0 && currAngle1 < 80 && currAngle1 > 70)
        {
            ShowRoom(firstHalf, false);
            ShowRoom(passage2, true);
        }

        if (loop2 == -1 && currAngle2 > 280 && currAngle2 < 290)
        {
            ShowRoom(firstHalf, false);
            ShowRoom(secondRoom, true);
            ShowRoom(secondRoom.transform.Find("Passage").gameObject, false);
        }
        else if (loop2 == -1 && currAngle2 < 280 && currAngle2 > 270)
        {
            ShowRoom(firstHalf, true);
            ShowRoom(secondRoom, false);
        }

        if (loop2 == 0 && currAngle2 < 10)
        {
            ShowRoom(secondRoom.transform.Find("Passage").gameObject, true);
        }
        else if (loop2 == -1 && currAngle2 > 350)
        {
            ShowRoom(secondRoom.transform.Find("Passage").gameObject, false);
        }
        /*if (loop2 == 0 && currAngle1 > 265 && currAngle1 < 270)
        {
            ShowRoom(firstHalf, false);
            ShowRoom(secondRoom, true);
            ShowRoom(firstRoom, false);
            ShowRoom(secondRoom.transform.Find("Passage").gameObject, false);
        }

        if (loop1 == 0 && currAngle1 < 280 && currAngle1 > 270)
        {
            ShowRoom(secondRoom.transform.Find("Passage").gameObject, true);
        }*/
        /*else if (loop == 0 && currAngle < 265 && currAngle > 255)
        {
            ShowRoom(firstHalf, true);
            ShowRoom(secondRoom, false);
            ShowRoom(firstRoom, true);
            ShowRoom(secondRoom.transform.Find("Passage").gameObject, true);
        }*/
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
