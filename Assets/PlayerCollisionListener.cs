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
		Debug.Log (this.gameObject.name);
		if (!blocking) {
			if (name == "head") {
				anim.Play ("HighDamage");
//				Debug.Log ("Top");
			} else {
				anim.Play ("HighDamage");
//				Debug.Log ("Low");
			}
		} else {
			anim.SetTrigger ("blockConfirm");
			Debug.Log ("blocked!");
		}
	}
}
