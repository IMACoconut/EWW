using UnityEngine;
using System.Collections;
using System;

public class Compteur : MonoBehaviour {

    private float angle;
    public float min = 0, max = 3*60;
    private RealTimer time = new RealTimer();
    private UILabel text;

	// Use this for initialization
	void Awake () {
        text = GetComponent<UILabel>();
        time.Pause();
	}
	
	// Update is called once per frame
	void Update () {
        if (time.IsPaused())
        {
            text.text = "";
            return;
        }
        TimeSpan left = time.TimeLeft();
        text.text = left.Minutes + ":" + ((left.Seconds < 10) ? "0"+left.Seconds : ""+left.Seconds);

        if (left.Minutes == 0 && left.Seconds == 0) Application.LoadLevel("menudead");
	}

    public void SetTime(int t)
    {
        time.delay = t;
        time.Start();
    }

    public void Pause()
    {
        time.Pause();
    }

    public void Resume()
    {
        time.Resume();
    }

    public void Start()
    {
        Debug.Log("timer start");
        time.Start();
    }

    public bool IsPaused()
    {
        return time.IsPaused();
    }
}
