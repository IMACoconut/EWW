using UnityEngine;
using System.Collections;

public class RoomTest2_switch : MonoBehaviour {

    private GameObject player;
    private bool collided;
    public bool on ;
    private GUISubtitle instructions;

    // Use this for initialization
    void Awake()
    {
        instructions = GameObject.Find("Instructions").GetComponent<GUISubtitle>();
    }

    void Start()
    {
        
        player = GameObject.Find("Player");
        on = false;
        audio.Stop();
       
    }

    // Update is called once per frame
    void Update()
    {

        if (collided)
        {
            if (Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.E))
            {
                on = !on;
                audio.Play(0);
            }
        }
       
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.tag.Equals("Player"))
        {
            collided = true;
            if(Constants.useController)
                instructions.displaySubtitles("Press 'A' to activate");
            else
                instructions.displaySubtitles("Press 'E' to activate");
        }
    }

    void OnTriggerExit(Collider player)
    {
        collided = false;
        instructions.hideSubtitles();
    }
}
