using UnityEngine;
using System.Collections;

public class VisualTrick1 : MonoBehaviour {

    public bool detect;
    private GameObject player;
	// Use this for initialization
	void Start () {
        detect = false;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if(detect)
        transform.LookAt(player.transform);
//        transform.localScale = new Vector3()
	}
}
