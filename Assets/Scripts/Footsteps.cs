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

        if (!Player.run && step && !Player.idle && Player.isGround && hit.gameObject.tag == "metal" && !Player.lockVar) { if (!Player.audio.isPlaying) StartCoroutine(WalkOnMetal()); } //walk 
        else if (Player.run && step && !Player.idle && Player.isGround && hit.gameObject.tag == "metal" && !Player.lockVar) { if (!Player.audio.isPlaying) StartCoroutine(RunOnMetal()); } //run
        else if (step && !Player.idle && Player.isGround && hit.gameObject.tag == "metal" && Player.lockVar && Player.forw != 0) { if (!Player.audio.isPlaying) StartCoroutine(WalkOnMetal()); } //ironsight

        else if (!Player.run && step && !Player.idle && Player.isGround && (hit.gameObject.tag == "concrete" || hit.gameObject.tag == "Untagged") && !Player.lockVar) { if (!Player.audio.isPlaying) StartCoroutine(WalkOnConcrete()); }
        else if (Player.run && step && !Player.idle && Player.isGround && (hit.gameObject.tag == "concrete" || hit.gameObject.tag == "Untagged") && !Player.lockVar) { if (!Player.audio.isPlaying) StartCoroutine(RunOnConcrete()); }
        else if (step && !Player.idle && Player.isGround && (hit.gameObject.tag == "concrete" || hit.gameObject.tag == "Untagged") && Player.lockVar && Player.forw != 0) { if (!Player.audio.isPlaying) StartCoroutine(WalkOnConcrete()); }
       
    }
	

  
    IEnumerator WalkOnMetal()
    {	
	step = false;
    Player.audio.clip = metal[Random.Range(0, metal.GetLength(0))];
    Player.audio.panLevel = 0.86f; 
    Player.audio.volume = .5f;
    Player.audio.Play();
    yield return new WaitForSeconds(audioStepLengthWalk);
	step = true;
}

    IEnumerator RunOnMetal()
    {
	step = false;
    Player.audio.clip = metal[Random.Range(0, metal.GetLength(0))];
    Player.audio.panLevel = 0.86f; 
    Player.audio.volume = .7f;
    Player.audio.Play();
    yield return new WaitForSeconds(audioStepLengthRun);
	step = true;
}


    IEnumerator WalkOnConcrete()
    {
        step = false;
        Player.audio.clip = concrete[Random.Range(0, concrete.GetLength(0))];
        Player.audio.panLevel = 0.86f;
        Player.audio.volume = .7f;
        Player.audio.Play();
        yield return new WaitForSeconds(audioStepLengthWalk);
        step = true;
    }

    IEnumerator RunOnConcrete()
    {
        step = false;
        Player.audio.clip = concrete[Random.Range(0, concrete.GetLength(0))];
        Player.audio.panLevel = 0.86f;
        Player.audio.volume = .8f;
        Player.audio.Play();
        yield return new WaitForSeconds(audioStepLengthRun);
        step = true;
    }

  
}
