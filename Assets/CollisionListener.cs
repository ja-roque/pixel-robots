using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListener : MonoBehaviour {
	EnemyBehavior myBehavior;
	SpriteEffector spriteEffector;

	void Start () {
		myBehavior = this.GetComponentInParent<EnemyBehavior> ();
		spriteEffector = GetComponent<SpriteEffector> ();
	}
		

	void OnTriggerEnter (Collider col){

		PlayerDataManager playerData = col.transform.root.GetComponent<PlayerDataManager> ();
		myBehavior.SetDamagedFlags (playerData.attackSide, playerData.attackType, playerData.currentDamage);
		spriteEffector.FlashRedOnce ();
	}
}
