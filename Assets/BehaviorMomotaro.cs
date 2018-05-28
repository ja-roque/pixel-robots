using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorMomotaro : EnemyBehavior {
	Random rand = new Random();
	public float health;
	// Use this for initialization
	void Start () {
		InvokeRepeating("runBehavior", 3.0f, .50f);
		
	}

	void Update(){
		health = enemyHealth.value;

		if (damageWouldReceived != 0) {

			string clipName = anim.GetCurrentAnimatorClipInfo (0) [0].clip.name;

			if (clipName.ToLower ().Contains ("straight")) {
				ReceiveDamage (0.2f);
				ClearDamageFlags ();
				Debug.Log(enemyHealth.value);
			} else {
				ReceiveDamage (0.8f);
				PlayDamageAnimation ();
				ClearDamageFlags ();
			}
		}
	}

	void Attack(){
		// Is player blocking?
		if(PlayerIsBlocking()["status"]){

			// Will read blocking?
			if(Random.Range( 0.0f, 1.0f ) > 0.4f ){
				// Is player blocking top or bot?
				if(PlayerIsBlocking()["high"]){
					AttackBot();
					return;
				} else {
					AttackTop();
					return;
				}
			}			
		}

		RandAttack();
	}

	void runBehavior(){
		if(anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
			return;
	 
	    if(Random.Range( 0.0f, 1.0f ) > 0.2f ){ 
	      Attack(); 
	    } else{ 
	      StayIdle(); 
	    } 
	}

	void RandAttack(){
		float random = Random.Range( 0.0f, 1.0f );
		
		if 		(	    0 < random && random < 1.0f/8	 ) anim.SetTrigger("leftStraightPunch");
		else if (1.0f/8   < random && random < 1.0f/8 * 2) anim.SetTrigger("rightStraightPunch");
		else if (1.0f/8 *2< random && random < 1.0f/8 * 3) anim.SetTrigger("leftCrouchPunch");
		else if (1.0f/8 *3< random && random < 1.0f/8 * 4) anim.SetTrigger("rightCrouchPunch");
		else if (1.0f/8 *4< random && random < 1.0f/8 * 5) anim.SetTrigger("rightUpSwing");
		else if (1.0f/8 *5< random && random < 1.0f/8 * 6) anim.SetTrigger("rightDownSwing");
		else if (1.0f/8 *6< random && random < 1.0f/8 * 7) anim.SetTrigger("leftUpSwing");
		else if (1.0f/8 *7< random && random < 1.0f/8 * 8) anim.SetTrigger("leftDownSwing");
		
	}

 	void StayIdle(){ 
    	anim.Play("Idle");
	}

	void AttackTop(){
		anim.SetTrigger("rightUpSwing");
	}

	void AttackBot(){
		anim.SetTrigger("leftCrouchPunch");
	}

	Dictionary<string, bool> PlayerIsBlocking(){
		// Might be cleaner to separte into different functions.
		var blockingStatus 			= new Dictionary<string, bool>();
		blockingStatus["status"]	= false;
		blockingStatus["high"] 		= false;

		if 	(playerAnim.GetBool("highBlock") ){
			blockingStatus["status"] 	= true;
			blockingStatus["high"] 		= true;
			return blockingStatus;
		}
		else if (playerAnim.GetBool("lowBlock")) {
			blockingStatus["status"] 	= true;
			blockingStatus["high"] 		= false;
			return blockingStatus;
		}
		
		return blockingStatus;
	}
}
