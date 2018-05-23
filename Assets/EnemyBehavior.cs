using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	[SerializeField] protected string attackedSide = "none";
	[SerializeField] protected string attackedType = "none";
	[SerializeField] protected float damageWouldReceived = 0;

	public void SetDamagedFlags(string side, string type, float damage){
		attackedSide = side;
		attackedType = type;
		damageWouldReceived = damage;

		Debug.Log ("damage flags set!");
	}

	protected void ClearDamageFlags(){
		attackedSide = "none";
		attackedType = "none";
		damageWouldReceived = 0;
	}
}
