using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Voice : MonoBehaviour {
    private bool step; 
    BallPlayer Player;
    GameObject grunt; 
    SoundBankManager SoundBank; 
    private List <GameObject> AudiozoneTab;
    AudioClip ToPlay;
    public string[] breathTab;
    public string[] breathTabSlow;
    public string[] idleTab;
    public string[] jumpTab;
    private float idleTime = 0;
   
    
    
	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player").GetComponent<BallPlayer>();
        grunt = GameObject.Find("grunt");
        SoundBank = GameObject.Find("GameGeneralScript").GetComponent<SoundBankManager>(); 
        AudiozoneTab = new List<GameObject>();
        step = true; 
        	
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(Player.LoadAudio);
        if (Player.clearAudio)
        {
            clearList();
            Player.clearAudio = false;
        }
       if (Player.LoadAudio) {
        
            foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("audiozone")){
                AudiozoneTab.Add(fooObj);
             }
        }
        for (int j = 0; j < AudiozoneTab.Count; j++)
        {
            if (AudiozoneTab[j].GetComponent<Audiozone>().triggered)
            {
                Debug.Log("Audio triggered n°" + AudiozoneTab[j].GetComponent<Audiozone>().audiofile); 
                /* On play le son une seule fois */ 
                grunt.audio.clip = SoundBank.SoundBank[AudiozoneTab[j].GetComponent<Audiozone>().audiofile];
                grunt.audio.Play(); 
                

                AudiozoneTab.Remove(AudiozoneTab[j]);


            }
            
        }



        if (!Player.run && step && !Player.idle && Player.isGround && !Player.lockVar) { if (!grunt.audio.isPlaying) { idleTime = 0f; StartCoroutine(BreathSlow()); } } //walk 
        else if (Player.run && step && !Player.idle && Player.isGround && !Player.lockVar) { if (!grunt.audio.isPlaying) { StartCoroutine(Breath()); idleTime = 0f; } } //run
        else if (step && !Player.idle && Player.isGround && Player.lockVar && Player.forw != 0) { if (!grunt.audio.isPlaying) { idleTime = 0f; StartCoroutine(BreathSlow()); } } //ironsight
        else if (step && Player.idle)
        {
            if (!grunt.audio.isPlaying && idleTime > 8f) { StartCoroutine(Idle()); idleTime = 0f; }
            else idleTime += Time.deltaTime;   
        } //idle

        if (step && Player.jump) if (!grunt.audio.isPlaying) Jump(); 
	}

    void clearList()
    {
        AudiozoneTab.Clear();
    }

    void Jump()
    {
        string tmp = jumpTab[Random.Range(0, breathTab.GetLength(0))];
        step = false;
        grunt.audio.clip = SoundBank.SoundBank[tmp];
        grunt.audio.Play();
        step = true; 

    }

    IEnumerator Breath()
    {
        string tmp = breathTab[Random.Range(0, breathTab.GetLength(0))];
        step = false;
        grunt.audio.clip = SoundBank.SoundBank[tmp];
        grunt.audio.Play(); 
        yield return new WaitForSeconds(0.15f);
        step = true; 
       
    }

    IEnumerator BreathSlow()
    {
        string tmp = breathTabSlow[Random.Range(0, breathTabSlow.GetLength(0))];
        step = false;
        grunt.audio.clip = SoundBank.SoundBank[tmp];
        grunt.audio.Play();
        yield return new WaitForSeconds(0.65f);
        step = true;

    }

    IEnumerator Idle()
    {
        string tmp = idleTab[Random.Range(0, idleTab.GetLength(0))]; 
        step = false;
        grunt.audio.clip = SoundBank.SoundBank[tmp];
        grunt.audio.Play();
        yield return new WaitForSeconds(1f);
        step = true;

    }
}
