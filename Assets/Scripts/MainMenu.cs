using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public void OnPlayClicked()
    {
        Application.LoadLevel("greyBox2");
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
	
	// Update is called once per frame
	void Update () {

	}
}
