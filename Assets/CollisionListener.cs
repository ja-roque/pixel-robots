using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListener : MonoBehaviour {
	Animator playerAnim;
	Animator myAnim;

	void Start () {
		myAnim = this.GetComponent<Animator> ();
	}
		

	void OnTriggerEnter (Collider col){

		playerAnim = col.gameObject.GetComponentInParent(typeof(Animator)) as Animator;
		string clipInfo = playerAnim.GetCurrentAnimatorClipInfo(0)[0].clip + "";

		//check each name and set triggers
	}
}
