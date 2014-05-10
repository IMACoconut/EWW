using UnityEngine;
using System.Collections;

public class flicker : MonoBehaviour {

	private Light lightclaveau;
	private float intensity ;
	private int up;
	private float timer; 
	// Use this for initialization
	void Start () {
		intensity = 0.7f ;
		lightclaveau = this.light; 
		up = 1 ;
		timer = 0f; 
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0.15f)
		{
			intensity += up * 0.1f;
			lightclaveau.intensity = intensity;
			if (intensity > 0.4) up = -1;
			else if (intensity < 1.3) up = 1;
		}
		
		
		timer += Time.deltaTime;
		
		
		
	}
}
