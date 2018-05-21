using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListener : MonoBehaviour {
	Animator anim;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter (Collision col){
		Debug.Log ("pega porque es gorda");
		anim = col.gameObject.GetComponentInParent(typeof(Animator)) as Animator;
		string clipInfo = anim.GetCurrentAnimatorClipInfo(0)[0].clip + "";
//		if(clipInfo.Contains(left))
	}
}
