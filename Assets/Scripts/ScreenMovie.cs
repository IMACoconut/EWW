using UnityEngine;
using System.Collections;

public class ScreenMovie : MonoBehaviour {

    public MovieTexture intro, alert;
    public bool started = false, alertStarted = false;


	// Use this for initialization
	void Start () {
        renderer.material.mainTexture = intro;
        alert.loop = true;
        Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (!started)
            return;

        if (!intro.isPlaying)
        {
            alertStarted = true;
            renderer.material.mainTexture = alert;
            alert.Play();
        }  
	}

    void Play()
    {
        started = true;
        intro.Play();
    }

}
