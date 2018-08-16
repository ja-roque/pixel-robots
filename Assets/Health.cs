using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health {

	// Use this for initialization
	public float value;

	public Health(float value = 100f)
    {
        this.value = value;
    }

	public float ApplyDamage(float damage){
		this.value -= damage;
		return this.value;
	}

	public void ResetHealth(float value = 100f){
		this.value = value;
	}

}
