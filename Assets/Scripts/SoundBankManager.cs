using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public class SoundBankManager : MonoBehaviour {
    public List<Sound> Sounds;
    public GUISubtitle subtitles;
	
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Sound get(string name)
    {
        foreach (Sound s in Sounds)
        {
            if (s.name == name)
                return s;
        }

        return null;
    }

    public void PlaySound(string name, GameObject source)
    {
        Sound s = get(name);
        if (s == null)
        {
            Debug.Log("Unable to find " + name);
            return;
        }
        StartSound(s, source);
    }

    public void StartSound(Sound s, GameObject source)
    {
        if(s.enableSubtitles)
            displaySub(s.subtitle);
        source.audio.priority = 0;
        source.audio.PlayOneShot(s.sound);
        StartCoroutine(DelayedCallback(s.sound.length, s.enableSubtitles));
    }

    void displaySub(string sub)
    {
        subtitles.displaySubtitles(sub);
    }

    void hideSub(bool hide)
    {
        if(hide)
            subtitles.hideSubtitles();
    }


    private IEnumerator DelayedCallback(float time, bool hide)
    {
        yield return new WaitForSeconds(time);
        hideSub(hide);
    }
}
