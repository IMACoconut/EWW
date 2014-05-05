using UnityEngine;
using System.Collections;
using System;

public class Compteur : MonoBehaviour {

    private float angle;
    public float min = 0, max = 3*60;
    private RealTimer time;
    private UILabel text;

	// Use this for initialization
	void Start () {
        text = GetComponent<UILabel>();
        time = new RealTimer();
        //SetTime(1000 * 60);
	}
	
	// Update is called once per frame
	void Update () {
        TimeSpan left = time.TimeLeft();
        text.text = left.Minutes + ":" + left.Seconds;
	}

    public void SetTime(int t)
    {
        time.delay = t;
        time.Start();
    }
}
