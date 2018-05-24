using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	protected string attackedSide = "none";
	protected string attackedType = "none";
	protected float damageWouldReceived = 0;

	protected float timer = 0;
	[SerializeField] protected float timeForNextAction;
	[SerializeField] protected float minActionTime = 1;
	[SerializeField] protected float maxActionTime = 1;

	protected Animator anim;

	void Awake (){
		anim = GetComponent<Animator> ();
	}

	public void SetDamagedFlags(string side, string type, float damage){
		attackedSide = side;
		attackedType = type;
		damageWouldReceived = damage;
	}

	protected void ClearDamageFlags(){
		attackedSide = "none";
		attackedType = "none";
		damageWouldReceived = 0;
	}

	protected void PlayDamageAnimation () {

		Debug.Log (attackedSide + " " + attackedType + " " + damageWouldReceived);
		
		if (attackedSide == "left")
			anim.SetTrigger ("damagedFromLeft");
		else if (attackedSide == "right")
			anim.SetTrigger ("damagedFromRight");


		if (attackedType == "uppercut")
			anim.SetTrigger ("damagedByUppercut");
		else if (attackedType == "straight")
			anim.SetTrigger ("damagedByStraight");
		else if (attackedType == "side")
			anim.SetTrigger ("damagedBySide");
	}

	protected void ReceiveDamage(){
		Debug.Log ("lose health here");
	}
}
