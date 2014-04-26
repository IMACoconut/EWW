using UnityEngine;
using System.Collections;

public class Aiguille : MonoBehaviour {

    public float min = -80, max = 80;
    public float angle;
    private bool raise;
    private float realAngle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        realAngle = angle + Random.Range(-3,3);
        this.transform.localEulerAngles = new Vector3(0, 0, realAngle);
	}

    public void SetAngle(float a) {
        angle = a;
    }
}
