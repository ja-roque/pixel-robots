using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListener : MonoBehaviour {
	EnemyBehavior myBehavior;

	void Start () {
		myBehavior = this.GetComponentInParent<EnemyBehavior> ();
	}
		

	void OnTriggerEnter (Collider col){

		PlayerDataManager playerData = col.transform.root.GetComponent<PlayerDataManager> ();
		myBehavior.SetDamagedFlags (playerData.attackSide, playerData.attackType, playerData.currentDamage);
	}
}
