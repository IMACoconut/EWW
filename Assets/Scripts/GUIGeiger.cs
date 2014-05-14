using UnityEngine;
using System.Collections;


public class GUIGeiger : MonoBehaviour {

    public bool enableShow = true;
	// Use this for initialization
	void Start () {
        Hide();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Show()
    {
        if (enableShow)
            NGUITools.SetActive(this.gameObject, true);
    }

    public void Hide()
    {
        NGUITools.SetActive(this.gameObject, false);
    }
}
