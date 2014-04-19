using UnityEngine;
using System.Collections;

public class LightRoom1Script : Room
{
    SoundBankManager SoundBank; 
    public GameObject alert;
    public string[] alertTab; 
    public GameObject doorEnd;
    public GameObject[] Lights;
    public GameObject bulblight;
    private Light bulb; 
    bool solved;
    bool step = true ;
    bool played = false; 
    private float alertTime = 5f; 

    // Use this for initialization
    void Start()
    {

        bulb = bulblight.GetComponentInChildren<Light>();
        bulb.color = Color.red;
        doorEnd.collider.enabled = false;

        solved = false;
        SoundBank = game.GetComponent<SoundBankManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
            return;

        if (solved) 
        {
            doorEnd.collider.enabled = true;
            bulb.color = Color.green;
        }
        else
        {
            solved = isSolved();
        }

        alertTime += Time.deltaTime;
        Debug.Log(alertTime % 20);
        if (alertTime%20 <= 0.1) 
        {
            Debug.Log("Boo");
            Alert();
            played = true;
        }        
    }

    void Alert()
    {
        string tmp = alertTab[Random.Range(0, alertTab.GetLength(0))];
        step = false;
        alert.audio.clip = SoundBank.SoundBank[tmp];
        alert.audio.Play();
        step = true;

    }

    bool isSolved()
    {
        if (Lights[0].GetComponent<LightRoom1Script_Light>().off == false &&
            Lights[1].GetComponent<LightRoom1Script_Light>().off == true &&
            Lights[2].GetComponent<LightRoom1Script_Light>().off == false &&
            Lights[3].GetComponent<LightRoom1Script_Light>().off == true &&
            Lights[4].GetComponent<LightRoom1Script_Light>().off == true &&
            Lights[5].GetComponent<LightRoom1Script_Light>().off == false &&
            Lights[6].GetComponent<LightRoom1Script_Light>().off == false)
        {
            //Debug.Log("Problem solved"); //table 1, table 3, table 6, table 7
            return true;
        }
        else {// Debug.Log("Problem not solved, noob");  
            return false; }
             

    }
}
