using UnityEngine;
using System.Collections;

public class LightRoom1Script_Light : MonoBehaviour
{

    public bool off = true;
    public bool solution = false;
    public bool collided = false;
    public GameScript mainScript;
    private Light[] lightswitch;

    // Use this for initialization
    void Start()
    {
        mainScript = GameObject.Find("GameGeneralScript").GetComponent<GameScript>();
        lightswitch = gameObject.GetComponentsInChildren<Light>();
        for (int i = 0; i < lightswitch.Length; i++) { lightswitch[i].enabled = false; }
    


    }

    // Update is called once per frame
    void Update()
    {
        if (Constants.pause)
            return;

        if (collided)
        {
            if (Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.E))
            {
                off = !off;
                if (off) { for (int i = 0; i < lightswitch.Length; i++) { lightswitch[i].enabled = false; } }
                else { for (int i = 0; i < lightswitch.Length; i++) { lightswitch[i].enabled = true; } }
                mainScript.instructions.hideSubtitles();
                RefreshInstruction();
            }
        }

    }

    void RefreshInstruction()
    {
        if (collided)
        {
            if (!off)
            {
                if (Constants.useController)
                    mainScript.instructions.displaySubtitles("Press 'A' to switch OFF the light");
                else
                    mainScript.instructions.displaySubtitles("Press 'E' to switch OFF the light");
            }
            else
            {
                if (Constants.useController)
                    mainScript.instructions.displaySubtitles("Press 'A' to switch ON the light");
                else
                    mainScript.instructions.displaySubtitles("Press 'E' to switch ON the light");
            }
        }
        else
        {
            mainScript.instructions.hideSubtitles();
        }
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.tag.Equals("Player"))
        {
            collided = true;
            RefreshInstruction();
        }
    }

    void OnTriggerExit(Collider player)
    {
        if (player.tag.Equals("Player"))
        {
            collided = false;
            RefreshInstruction();
        }
    }


}
