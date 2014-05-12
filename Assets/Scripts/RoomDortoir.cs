using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomDortoir : Room {
    public ScreenMovie screen;
    private float elapsed = 0f;
    private bool startPlay = false;

    void Awake()
    {
    }
    // Use this for initialization
	void Start () {
        screen.dortoir = this;
	}
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;
        if (!startPlay && elapsed > 15)
        {
            Debug.Log("start");
            screen.Play();
            startPlay = true;
        }
	}

    protected List<RedSpot> getLights()
    {
        List<RedSpot> affiches = new List<RedSpot>();
        foreach (RedSpot c in gameObject.GetComponentsInChildren<RedSpot>())
            affiches.Add(c);

        return affiches;
    }

    public void alert()
    {
        List<RedSpot> lights = getLights();
        foreach (RedSpot l in lights)
        {
            l.Alert();
        }
    }
}
