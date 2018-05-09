using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actions : MonoBehaviour {

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow))
        {
            
            //Send the message to the Animator to activate the trigger parameter named "Jump"
            anim.SetTrigger("lfist_straight_punch");
        }
	}

	// void lfist_straight_punch(){
	// 	 anim.SetTrigger("lfist_straight_punch");
	// }
}
