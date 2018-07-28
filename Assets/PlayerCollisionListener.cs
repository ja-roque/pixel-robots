using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionListener : MonoBehaviour {	

	public bool blocking = false;

	protected Animator anim;
	void Start () {
		anim = GetComponentInParent<Animator>();

	}

	void OnTriggerEnter (Collider col){
		Debug.Log (col);
		if (!blocking) {
			anim.Play ("HighDamage");
			if (name == "head") {
				//HITS HEAD
				PlayerDataManager.playerHealth.ApplyDamage(9.90f);
			} else {
				//HITS CHEST
				PlayerDataManager.playerHealth.ApplyDamage(7.90f);
			}

			if(PlayerDataManager.playerHealth.value <= 0){
				//LOSE
				
			}

		} else {
			anim.SetTrigger ("blockConfirm");
			// Debug.Log ("blocked!");
		}
	}
}
