using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeter : MonoBehaviour {

	Transform 	healthMask;
	float 		healthScaleSize = 2f;
	Health 		healthData;

	// Use this for initialization
	void Start () {
		healthMask = this.gameObject.transform.GetChild(0);
		
		if (this.gameObject.tag == "player"){
			healthData = PlayerDataManager.playerHealth;
		} else if(this.gameObject.tag == "enemy"){
			Debug.Log("samsung");
			healthData = EnemyBehavior.enemyHealth;
		}
	}
	
	// Update is called once per frame
	void Update () {
		updateHealthBar();
	}

	void updateHealthBar(){
		healthMask.localScale = new Vector3(healthScaleSize * (healthData.value/100f), 1, 1);
	}
}
