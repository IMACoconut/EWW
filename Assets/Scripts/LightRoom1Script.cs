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
    private int nbalert = 0;
    GameObject grunt;
    private bool angry = false; 

    // Use this for initialization
    void Start()
    {
        doorEnd.collider.enabled = false;
        grunt = GameObject.Find("grunt");
        solved = false;
        SoundBank = game.GetComponent<SoundBankManager>();
        bulb = GameObject.Find("bulb").GetComponent<Light>();
        bulb.light.color = Color.red; 
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
            return;

        if (solved) 
        {
            doorEnd.collider.enabled = true;
            bulb.light.color = Color.green; 
            
        }
        else
        {
            solved = isSolved();
        }

        alertTime += Time.deltaTime;
       
        if (alertTime >= 10 && !played) 
        {
            //Debug.Log("Boo");
            Alert();
            nbalert++;
            alertTime = 0f; 
            if(nbalert == 3) played = true;
        }
        else if (alertTime >= 5 && played && !angry)
        {
            //Debug.Log("she's bothering me !");
            grunt.audio.Stop(); 
            if (!grunt.audio.isPlaying)
            {
                step = false;
                SoundBank.PlaySound("she is bothering me", grunt);
                step = true;
                angry = true; 
            }
           
        } 
    }

    void Alert()
    {
        string tmp = alertTab[Random.Range(0, alertTab.GetLength(0))];
        step = false;
        SoundBank.PlaySound(tmp, alert);
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
