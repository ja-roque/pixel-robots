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
			if (name == "head") {
				//HITS HEAD
				anim.Play ("HighDamage");
				PlayerDataManager.playerHealth.ApplyDamage(9.90f);
			} else {
				//HITS CHEST
				anim.Play ("HighDamage");
				PlayerDataManager.playerHealth.ApplyDamage(7.90f);
			}
		} else {
			anim.SetTrigger ("blockConfirm");
			Debug.Log ("blocked!");
		}
	}
}
