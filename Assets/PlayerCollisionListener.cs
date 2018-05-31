using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionListener : MonoBehaviour {	

	protected Animator anim;
	void Start () {
		anim = GetComponentInParent<Animator>();

	}
		

	void OnTriggerEnter (Collider col){		

		Debug.Log(name);
		if(name == "head"){
			PlayerDataManager.playerHealth.ApplyDamage(0.90f);
			anim.Play("HighDamage");
			Debug.Log("Top");
		} else {
			anim.Play("HighDamage");
			Debug.Log("Low");
		}
	
	}
}
