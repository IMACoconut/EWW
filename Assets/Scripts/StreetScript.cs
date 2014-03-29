using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StreetScript : MonoBehaviour {
	
	public bool N;
	public bool E;
	public bool S;
	public bool W;
	public bool open = true;
    public Vector3 position;
    public List<GameObject> affiches;

	// Use this for initialization
	void Start () {
        position = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public int getPathsCount() {
		int cnt = 0;
		cnt += (N ? 1 : 0);
		cnt += (E ? 1 : 0);
		cnt += (S ? 1 : 0);
		cnt += (W ? 1 : 0);
		return cnt;
	}
	
	public void rotateLeft() {
		transform.Rotate(new Vector3(0,1,0), 90);
		bool tmp = N;
		N = W;
		W = S;
		S = E;
		E = tmp;
	}
	
	public void rotateRight() {
		transform.Rotate(new Vector3(0,1,0), -90);	
		bool tmp = N;
		N = E;
		E = S;
		S = W;
		W = tmp;
	}

    public void OnDestroy()
    {
        foreach(GameObject a in affiches)
            GameObject.Destroy(a);
    }
}
