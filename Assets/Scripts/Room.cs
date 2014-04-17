using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

    public GameObject start;
    public EndPointScript end;
    protected GameScript game;
    public GameObject player;

    public void setGameScript(GameScript g)
    {
        game = g;
        end.mainScript = g;
    }
}
