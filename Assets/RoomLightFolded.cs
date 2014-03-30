using UnityEngine;
using System.Collections;

public class RoomLightFolded : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = player.transform.position - transform.position;
        float angle = Constants.RealAngle(pos, -transform.forward, -transform.right);
        Debug.Log(angle);
	}
}
