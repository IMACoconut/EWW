using UnityEngine;
using System.Collections;

public class Room1Script : Room {
	
	public float lastAngle = 0, currAngle = 0;
	public int loop = -1;
	public GameObject[] cubes;
	public GameObject etagere, e2_broken, e2_fixed;
	public GameObject roomCenter;
    public GameObject pillar;
    public GameObject doorStart;
    public GameObject doorEnd;
	
	// Use this for initialization
	void Start () {
		lastAngle = Vector3.Angle(player.transform.position, roomCenter.transform.position);
        Show(e2_fixed, false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Constants.pause)
            return;


        Vector3 pos = player.transform.position - roomCenter.transform.position;
        currAngle = Constants.RealAngle(pos, -roomCenter.transform.right, -roomCenter.transform.forward);
        IncreaseLoop();
        loopUpdate();
		lastAngle = currAngle;
	}
	
	void IncreaseLoop() {
        if (lastAngle > 350 && currAngle < 50)
            loop++;
        else if (currAngle > 350 && lastAngle < 50)
            loop--;
	}

    void loopUpdate()
    {
        if (loop == 0 && currAngle < 20)
        {
            Show(etagere, false);
        }
        else if (loop == -1 && currAngle > 350)
        {
            Show(etagere, true);
        }
        if (loop == 2 && currAngle > 350)
        {
            Show(etagere, false);
        }
        else if (loop == 3 && currAngle < 20)
        {
            Show(etagere, true);
        }

        for (int i = 0; i < 3; ++i)
        {
            if (loop == i && currAngle > 160)
            {
                Show(cubes[i], false);
                if (loop == 2)
                {
                    Show(e2_fixed, true);
                    Show(e2_broken, false);
                }
            }
            else if (loop == i && currAngle < 140)
            {
                Show(cubes[i], true);
                if (loop == 2)
                {
                    Show(e2_fixed, false);
                    Show(e2_broken, true);
                }
            }
        }


        if (loop == -1 && currAngle > 240 && currAngle < 260)
        {
            doorEnd.transform.Find("door").renderer.enabled = true;
            doorEnd.collider.enabled = true;
        }
        else if (loop == -1 && currAngle < 280 && currAngle > 260)
        {
            doorEnd.transform.Find("door").renderer.enabled = false;
            doorEnd.collider.enabled = false;
        }

        if (loop == 2 && currAngle > 240)
        {
            doorEnd.transform.Find("door").renderer.enabled = true;
            doorEnd.collider.enabled = true;
        }
        else if (loop == 2 && currAngle < 240)
        {
            doorEnd.transform.Find("door").renderer.enabled = false;
            doorEnd.collider.enabled = false;
        }


        if (loop == 0 && currAngle < 50)
        {
            doorStart.transform.Find("door").renderer.enabled = true;
            doorStart.collider.enabled = true;
        }
        else if (loop == 0 && currAngle > 50)
        {
            doorStart.transform.Find("door").renderer.enabled = false;
            doorStart.collider.enabled = false;
        }

        if (loop == 3 && currAngle > 50)
        {
            doorStart.transform.Find("door").renderer.enabled = true;
            doorStart.collider.enabled = true;
        }
        else if (loop == 3 && currAngle < 50)
        {
            doorStart.transform.Find("door").renderer.enabled = false;
            doorStart.collider.enabled = false;
        }
    }

    void Show(GameObject o, bool show)
    {
        Renderer[] childsR = o.GetComponentsInChildren<Renderer>();
        Collider[] childsC = o.GetComponentsInChildren<Collider>();
        if (o.renderer != null)
            o.renderer.enabled = show;
        if (o.collider != null)
            o.collider.enabled = show;

        foreach (Renderer r in childsR)
            r.enabled = show;
        foreach (Collider c in childsC)
            c.enabled = show;
    }
}
