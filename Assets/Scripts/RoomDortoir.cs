using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomDortoir : Room {
    public ScreenMovie screen;
    private float elapsed = 0f;
    private bool startPlay = false;
    public GameObject[] lightScreen;
    

    void Awake()
    {
    }
    // Use this for initialization
	void Start () {
        screen.dortoir = this;
        lightScreen[0].renderer.enabled = false;
        lightScreen[1].renderer.enabled = false;
        lightScreen[2].renderer.enabled = false; 
        lightScreen[3].renderer.enabled = false;
        lightScreen[4].light.enabled = false; 
	}
	
	// Update is called once per frame
	void Update () {
        if (Constants.pause)
            return;

        elapsed += Time.deltaTime;
        if (!startPlay && elapsed > 15)
        {
            Debug.Log("start");
            screen.Play();
            startPlay = true;
            lightScreen[0].renderer.enabled = true;
            lightScreen[1].renderer.enabled = true;
            lightScreen[2].renderer.enabled = true;
            lightScreen[3].renderer.enabled = true;
            lightScreen[4].light.enabled = true; 

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
