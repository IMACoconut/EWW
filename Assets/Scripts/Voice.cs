using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Voice : MonoBehaviour {
   
    BallPlayer Player;
    SoundBankManager SoundBank; 
    private List <GameObject> AudiozoneTab;
    AudioClip ToPlay; 
    
    
    
	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player").GetComponent<BallPlayer>();
        SoundBank = GameObject.Find("GameGeneralScript").GetComponent<SoundBankManager>(); 
        AudiozoneTab = new List<GameObject>();
        	
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
                Player.audio.clip = SoundBank.SoundBank[AudiozoneTab[j].GetComponent<Audiozone>().audiofile];
                Player.audio.Play(); 
                

                AudiozoneTab.Remove(AudiozoneTab[j]);


            }
            
        }
	
	}
}
