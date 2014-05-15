using UnityEngine;
using System.Collections;

public class ScreenMovie : MonoBehaviour {

    public MovieTexture intro, alert;
    public AudioClip introSound, alertSound;
    public GameObject alarmSound; 
    public RoomDortoir dortoir;

    private bool wasPaused = false;
    public bool started = false, alertStarted = false;


    void Awake()
    {
        intro.Stop();
    }
	// Use this for initialization
	void Start () {
        renderer.material.mainTexture = intro;
        alert.loop = true;
        alarmSound.audio.loop = true;
        alarmSound.audio.Stop(); 
	}
	
	// Update is called once per frame
	void Update () {
        if (Constants.pause)
        {
            if (started && !wasPaused)
            {
                if (!alertStarted)
                    intro.Pause();
                else
                {
                    alert.Pause();
                    alarmSound.audio.Pause();
                }
                audio.Pause();

                wasPaused = true;
            }
            return;
        }

        if (!started)
            return;

        if (wasPaused)
        {
            if (!alertStarted)
                intro.Play();
            else
            {
                alert.Play();
                alarmSound.audio.Play();
            }
            audio.Play();
            wasPaused = false;
        }

        if (!intro.isPlaying && !alertStarted)
        {
            alertStarted = true;
            renderer.material.mainTexture = alert;
            audio.clip = alertSound;
            audio.Play();
            alert.Play();
            dortoir.alert();
            alarmSound.audio.Play(); 

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
