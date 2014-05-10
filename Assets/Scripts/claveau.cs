using UnityEngine;
using System.Collections;

public class claveau : MonoBehaviour {
    private Light lightclaveau;
    private float intensity ;
    private int up;
    private float timer; 
	// Use this for initialization
	void Start () {
        intensity = 0.7f *0.2f ;
        lightclaveau = this.light; 
        up = 1 ;
        timer = 0f; 
	
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0.3f)
        {
            intensity += up * 0.10f *0.1f;
            lightclaveau.intensity = intensity;
            if (intensity > 4 * 0.2f) up = -1;
            else if (intensity < 0.7 * 0.2f) up = 1;
        }
        
        
        timer += Time.deltaTime;

        
	
	}
}
