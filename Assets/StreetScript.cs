using UnityEngine;
using System.Collections;

public class StreetScript : MonoBehaviour {
	
	public bool N;
	public bool E;
	public bool S;
	public bool W;
	public bool open = true;
	public int x, y;
	// Use this for initialization
	void Start () {
	
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
}
