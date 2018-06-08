using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeter : MonoBehaviour {

	[SerializeField] private GameObject target;

	RectTransform 	healthMask;
	float 		healthScaleSizeX;
	float 		healthScaleSizeY;
	Health 		healthData;

	// Use this for initialization
	void Start () {
		healthMask = this.gameObject.transform.GetChild(0).GetComponent<RectTransform>();
		healthScaleSizeX = healthMask.sizeDelta.x;
		healthScaleSizeY = healthMask.sizeDelta.y;

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
		healthMask.sizeDelta = new Vector2(healthScaleSizeX * (healthData.value/100f), healthScaleSizeY);
	}
}
