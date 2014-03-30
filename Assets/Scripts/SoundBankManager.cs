using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public class SoundBankManager : MonoBehaviour {
    //public AudioClip[] SoundBank;
    public List<string> AudioName = new List<string>();
    public List<AudioClip> AudioTrack = new List<AudioClip>(); 
    public Dictionary<string, AudioClip> SoundBank = new Dictionary<string, AudioClip>();  
	
    // Use this for initialization
	void Start () {

        if (AudioName.Count != AudioTrack.Count) Debug.LogError("Attention incohérence nom/piste audio");
        else
        {
            for (int i = 0; i < AudioTrack.Count; i++)
            {
                SoundBank[AudioName[i]] = AudioTrack[i]; //on remplit la banque de son
            }
        } 
        
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
