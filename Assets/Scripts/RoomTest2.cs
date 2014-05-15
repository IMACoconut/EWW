using UnityEngine;
using System.Collections;

public class RoomTest2 : Room
{
    SoundBankManager SoundBank;
    public GameObject alert;
    public string[] alertTab;
    bool step = true;
    bool played = false;
    private float alertTime = 5f;
    private int nbalert = 0;
    GameObject grunt;
    private bool angry = false;


    void Start()
    {
        grunt = GameObject.Find("grunt");
        SoundBank = game.GetComponent<SoundBankManager>();  
    }

    void Update() {

        alertTime += Time.deltaTime;
        /*
        if (alertTime >= 10 && !played)
        {
            //Debug.Log("Boo");
            Alert();
            nbalert++;
            alertTime = 0f;
            if (nbalert == 3) played = true;
        } */
    
    }

    void Alert()
    {
        string tmp = alertTab[Random.Range(0, alertTab.GetLength(0))];
        step = false;
        SoundBank.PlaySound(tmp, alert);
        step = true;

    }



}
