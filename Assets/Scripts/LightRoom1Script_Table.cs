using UnityEngine;
using System.Collections;

public class LightRoom1Script_Table : MonoBehaviour
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
       

    }

    void OnGUI()
    {
        
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
