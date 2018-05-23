using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour {

	public float straightDamage;
	public float sideDamage;
	public float uppercutDamage;

	public string attackType = "none";
	public string attackSide = "none";

	[HideInInspector] public float currentDamage;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		string clipName = anim.GetCurrentAnimatorClipInfo (0) [0].clip.name;

		if (clipName.ToLower ().Contains ("left")) {
			attackSide = "left";
		} else if (clipName.ToLower ().Contains ("right")) {
			attackSide = "right";
		} else {
			attackSide = "none";
		}


		if (clipName.ToLower ().Contains ("uppercut")) {
			attackType = "uppercut";
			currentDamage = uppercutDamage;
		} else if (clipName.ToLower ().Contains ("side")) {
			attackType = "side";
			currentDamage = sideDamage;
		} else if (clipName.ToLower ().Contains ("straight")) {
			attackType = "straight";
			currentDamage = straightDamage;
		} else {
			attackType = "none";
			currentDamage = 0;
		}
	}
}
