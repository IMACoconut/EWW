using UnityEngine;
using System.Collections;

public class ScreenMovie : MonoBehaviour {

    public MovieTexture intro, alert;
    public AudioClip introSound, alertSound;

    public RoomDortoir dortoir;

    public bool started = false, alertStarted = false;


    void Awake()
    {
        intro.Stop();
    }
	// Use this for initialization
	void Start () {
        renderer.material.mainTexture = intro;
        alert.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (!started)
            return;

        if (!intro.isPlaying && !alertStarted)
        {
            alertStarted = true;
            renderer.material.mainTexture = alert;
            audio.clip = alertSound;
            audio.Play();
            alert.Play();
            dortoir.alert();
        }  
	}

    public void Play()
    {
        started = true;
        intro.Play();
        audio.clip = introSound;
        audio.Play();
    }

}
