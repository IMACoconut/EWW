using UnityEngine;
using System.Collections;

public class GUISubtitle : MonoBehaviour {

    private UILabel text;
    private int sub = 0;
	// Use this for initialization
	void Start () {
        text = GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void displaySubtitles(string subtitles)
    {
        NGUITools.SetActive(gameObject, true);
        text.text = subtitles;
        ++sub;
    }

    public void hideSubtitles()
    {
        --sub;
        if (sub == 0)
        {
            text.text = "";
            NGUITools.SetActive(gameObject, false);
        }
    }
}
