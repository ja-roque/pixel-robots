using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorMomotaro : EnemyBehavior {
	Random rand = new Random();

	// Use this for initialization
	void Start () {
		InvokeRepeating("runBehavior", 3.0f, .50f);
		timer = 0;
		timeForNextAction = 3;
	}

	void Update(){

		if (damageWouldReceived != 0) {
			ReceiveDamage ();
			PlayDamageAnimation ();
			ClearDamageFlags ();

		} else if (timer >= timeForNextAction) {
			timer = 0;
			timeForNextAction = Random.Range (minActionTime, maxActionTime);

			runBehavior ();  //attack, taunt
		}

		if(!anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
			timer += Time.deltaTime;
	}

	void Attack(){
		// Is player blocking?
		if(true){

			// Will read blocking?
			if(false){
				// Is player blocking top or bot?
				if(true){
					AttackTop();
				} else {
					AttackBot();
				}
			}

			RandAttack();
		}
	}

	void runBehavior(){
		if(anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
			return;
		
		//CHANCE FOR TAUNT HERE?
		Attack();
	}

	void RandAttack(){
		float random = Random.Range( 0.0f, 1.0f );
		
		if 		(	    0 > random && random < 1.0f/8	 ) anim.SetTrigger("leftStraightPunch");
		else if (1.0f/8   > random && random < 1.0f/8 * 2) anim.SetTrigger("rightStraightPunch");
		else if (1.0f/8 *2> random && random < 1.0f/8 * 3) anim.SetTrigger("leftCrouchPunch");
		else if (1.0f/8 *3> random && random < 1.0f/8 * 4) anim.SetTrigger("rightCrouchPunch");
		else if (1.0f/8 *4> random && random < 1.0f/8 * 5) anim.SetTrigger("rightUpSwing");
		else if (1.0f/8 *5> random && random < 1.0f/8 * 6) anim.SetTrigger("rightDownSwing");
		else if (1.0f/8 *6> random && random < 1.0f/8 * 7) anim.SetTrigger("leftUpSwing");
		else if (1.0f/8 *7> random && random < 1.0f/8 * 8) anim.SetTrigger("leftDownSwing");
		
	}

	void AttackTop(){
		
	}

	void AttackBot(){
		
	}
}
