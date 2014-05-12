using UnityEngine;
using System.Collections;


public class GUIGeiger : MonoBehaviour {

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
}
