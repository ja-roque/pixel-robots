using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockListener : MonoBehaviour {

	protected Animator anim;
	void Start () {
		anim = GetComponentInParent<Animator>();
	}

	void OnTriggerEnter (Collider col){
		string clipName = anim.GetCurrentAnimatorClipInfo (0) [0].clip.name;
		if(clipName.ToLower ().Contains ("block")){
			//play block anim			
		}
	}
}
