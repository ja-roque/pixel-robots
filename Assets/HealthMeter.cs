using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeter : MonoBehaviour {

	[SerializeField] private GameObject target;

	Transform 	healthMask;
	float 		healthScaleSize = 2f;
	Health 		healthData;

	// Use this for initialization
	void Start () {
		healthMask = this.gameObject.transform.GetChild(0);

		if (target.tag == "Player"){
			healthData = PlayerDataManager.playerHealth;
		} else if(target.tag == "Enemy"){
			Debug.Log("samsung");
			healthData = EnemyBehavior.enemyHealth;
		}
	}
	
	// Update is called once per frame
	void Update () {
		updateHealthBar();
	}

	void updateHealthBar(){
		healthMask.localScale = new Vector3(1 * (healthData.value/100f), 1, 1);
	}
}
