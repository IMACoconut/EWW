using UnityEngine;
using System.Collections;

public class GUIMainMenu : MonoBehaviour {

    public GameScript main;

	// Use this for initialization
	void Start () {
        Hide();
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
        Hide();
        Application.LoadLevel("menu");
    }

    public void OnOptionsClicked()
    {
        Hide();
        main.ShowOptions();
    }

    public void OnResumeClicked()
    {
        Hide();
        main.Resume();
    }
}
