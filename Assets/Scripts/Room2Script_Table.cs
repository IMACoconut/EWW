using UnityEngine;
using System.Collections;

public class Room2Script_Table : MonoBehaviour
{

    public bool off = true;
    public bool solution = false;
    public bool collided = false;
    public GameScript mainScript;
    // Use this for initialization
    void Start()
    {
        mainScript = GameObject.Find("GameGeneralScript").GetComponent<GameScript>();
        audio.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (collided)
        {
            if (Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.E))
            {
                off = !off;
                Debug.Log("switch");

            }
        }

    }

    void OnGUI()
    {
        if (!collided)
            return;

        if (!off)
            GUI.Box(new Rect(0, Screen.height - 50, Screen.width, 50), "Press 'A' to switch OFF the light");
        else
            GUI.Box(new Rect(0, Screen.height - 50, Screen.width, 50), "Press 'A' to switch ON the light");
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.tag.Equals("Player"))
        {
            collided = true;
            if(solution) audio.Play(0);
        }
    }

    void OnTriggerExit(Collider player)
    {
        collided = false;
        audio.Stop();
    }

    
}
