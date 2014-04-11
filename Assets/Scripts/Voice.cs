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

      

        if (!Player.run && step && !Player.idle && Player.isGround && !Player.lockVar) { if (!grunt.audio.isPlaying) StartCoroutine(BreathSlow()); } //walk 
        else if (Player.run && step && !Player.idle && Player.isGround && !Player.lockVar) { if (!grunt.audio.isPlaying) StartCoroutine(Breath()); } //run
        else if (step && !Player.idle && Player.isGround && Player.lockVar && Player.forw != 0) { if (!grunt.audio.isPlaying) StartCoroutine(BreathSlow()); } //ironsight




	
	}

    IEnumerator Breath()
    {
        string tmp = breathTab[Random.Range(0, breathTab.GetLength(0))];
        step = false;
        Debug.Log("RESPIRE !!!!!!!");
        grunt.audio.clip = SoundBank.SoundBank[tmp];
        grunt.audio.Play(); 
        yield return new WaitForSeconds(2);
        step = true; 
       
    }

    IEnumerator BreathSlow()
    {
        string tmp = breathTab[Random.Range(0, breathTab.GetLength(0))];
        step = false;
        Debug.Log("RESPIRE SLOW !!!!!!!");
        grunt.audio.clip = SoundBank.SoundBank[tmp];
        grunt.audio.Play();
        yield return new WaitForSeconds(3);
        step = true;

    }
}
