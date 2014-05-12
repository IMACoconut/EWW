using UnityEngine;
using System.Collections;

public class RoomLightFolded : Room {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = player.transform.position - transform.position;
        float angle = Constants.RealAngle(pos, -transform.forward, -transform.right);
        Debug.Log(angle);
	}
}
