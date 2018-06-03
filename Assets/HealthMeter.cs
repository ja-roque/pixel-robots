using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeter : MonoBehaviour {

	Transform 	healthMask;
	float 		healthScaleSize = 2f;

	// Use this for initialization
	void Start () {
		healthMask = this.gameObject.transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		updateHealthBar();
	}

	void updateHealthBar(){
		healthMask.localScale = new Vector3(healthScaleSize * (PlayerDataManager.playerHealth.value/100f), 1, 1);
	}
}
