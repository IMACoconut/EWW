using UnityEngine;
using System.Collections;

public class RoomTest2_switch : MonoBehaviour {

    private GameObject player;
    private bool collided;
    public bool on ;

    // Use this for initialization
    void Start()
    {

        player = GameObject.Find("Player");
        on = false;
       
    }

    // Update is called once per frame
    void Update()
    {

        if (collided)
        {
            if (Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("switch");
                on = !on; 

            }
        }
       
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.tag.Equals("Player")) collided = true; 
    }

    void OnTriggerExit(Collider player)
    {
        collided = false;
    }

    void OnGUI()
    {
        if (!collided)
            return;

            GUI.Box(new Rect(0, Screen.height - 50, Screen.width, 50), "Press 'A' ");
    }
}
