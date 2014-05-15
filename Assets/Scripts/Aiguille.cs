using UnityEngine;
using System.Collections;

public class Aiguille : MonoBehaviour {

    public float min = -80, max = 80;
    public float angle = 0;
    private bool raise;
    private float realAngle;

	// Use this for initialization
	void Start () {
        SetAngle(min);
	}
	
	// Update is called once per frame
	void Update () {
        if (Constants.pause)
            return;

        realAngle = angle + Random.Range(-3,3);
        this.transform.localEulerAngles = new Vector3(0, 0, realAngle);
	}

    public void SetAngle(float a) {
        angle = -a;
    }
}
