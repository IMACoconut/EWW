using UnityEngine;
using System.Collections;

public class GUIMainMenu : MonoBehaviour {

    public GameScript main;

	// Use this for initialization
	void Start () {
        NGUITools.SetActive(this.gameObject, false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Show()
    {
        NGUITools.SetActive(this.gameObject, true);
    }

    public void Hide()
    {
        NGUITools.SetActive(this.gameObject, false);
    }

    public void OnBackClicked()
    {
        Application.LoadLevel("menu");
    }

    public void OnOptionsClicked()
    {
        Debug.Log(2);
    }

    public void OnResumeClicked()
    {
        main.Resume();
        Hide();
    }
}
