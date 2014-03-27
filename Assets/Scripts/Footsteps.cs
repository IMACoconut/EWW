using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {
    public AudioClip[] metal ;
    public AudioClip[] concrete ;
    private bool step; 
    private float audioStepLengthWalk = 0.7f;
    private float audioStepLengthRun = 0.2f;
    BallPlayer Player;

    
	// Use this for initialization
	void Start () {
        step = true;
        Player = GameObject.Find("Player").GetComponent<BallPlayer>();
        
         
        
	}

    void OnControllerColliderHit (ControllerColliderHit hit) { //permet de gerer les differents types de sol

        CharacterController controller = Player.GetComponent<CharacterController>();

        if (!Player.run && step && !Player.idle && Player.isGround && hit.gameObject.tag == "metal" ) { if (!Player.audio.isPlaying) StartCoroutine(WalkOnMetal()); }
        else if (Player.run && step && !Player.idle && Player.isGround && hit.gameObject.tag == "metal" ) { if (!Player.audio.isPlaying) StartCoroutine(RunOnMetal()); }
        else if (!Player.run && step && !Player.idle && Player.isGround && (hit.gameObject.tag == "concrete" || hit.gameObject.tag == "Untagged") ) { if (!Player.audio.isPlaying) StartCoroutine(WalkOnConcrete()); }
        else if (Player.run && step && !Player.idle && Player.isGround && (hit.gameObject.tag == "concrete" || hit.gameObject.tag == "Untagged")) { if (!Player.audio.isPlaying) StartCoroutine(RunOnConcrete()); }
        
    }
	

  
    IEnumerator WalkOnMetal()
    {	
	step = false;
    Player.audio.clip = metal[Random.Range(0, metal.GetLength(0))];
    Player.audio.panLevel = 0.86f; 
    Player.audio.volume = .8f;
    Player.audio.Play();
    yield return new WaitForSeconds(audioStepLengthWalk);
	step = true;
}

    IEnumerator RunOnMetal()
    {
	step = false;
    Player.audio.clip = metal[Random.Range(0, metal.GetLength(0))];
    Player.audio.panLevel = 0.86f; 
    Player.audio.volume = .9f;
    Player.audio.Play();
    yield return new WaitForSeconds(audioStepLengthRun);
	step = true;
}


    IEnumerator WalkOnConcrete()
    {
        step = false;
        Player.audio.clip = concrete[Random.Range(0, concrete.GetLength(0))];
        Player.audio.panLevel = 0.86f;
        Player.audio.volume = .8f;
        Player.audio.Play();
        yield return new WaitForSeconds(audioStepLengthWalk);
        step = true;
    }

    IEnumerator RunOnConcrete()
    {
        step = false;
        Player.audio.clip = concrete[Random.Range(0, concrete.GetLength(0))];
        Player.audio.panLevel = 0.86f;
        Player.audio.volume = .9f;
        Player.audio.Play();
        yield return new WaitForSeconds(audioStepLengthRun);
        step = true;
    }

  
}
