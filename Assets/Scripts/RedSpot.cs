using UnityEngine;
using System.Collections;

public class RedSpot : MonoBehaviour {

    private Light light;
    private bool alert = false;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if (alert)
        {
            transform.Rotate(Vector3.up, 180 * Time.deltaTime);
        }
	}

    public void Alert()
    {
        alert = true;
        light.type = LightType.Spot;
        light.color = new Color(255, 0, 0);
    }
}
