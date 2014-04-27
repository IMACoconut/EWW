using UnityEngine;
using System.Collections;

public class Compteur : MonoBehaviour {

    private float angle;
    public float min = 0, max = 3*60;
    private RealTimer time;
	// Use this for initialization
	void Start () {
        time = new RealTimer();
        SetTime(1000 * 60);
	}
	
	// Update is called once per frame
	void Update () {
        int s = time.TimeLeft().Seconds + time.TimeLeft().Minutes*60;
        
        float angle = s / (float)(max - min) * 360f;
        Debug.Log(time.TimeLeft().ToString() + " " + angle);
        transform.localEulerAngles = new Vector3(0,0,angle);
	}

    public void SetTime(int t)
    {
        time.delay = t;
        time.Start();
    }
}
