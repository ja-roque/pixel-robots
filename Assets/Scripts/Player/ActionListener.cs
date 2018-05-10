using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionListener : MonoBehaviour {

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            
            //Send the message to the Animator to activate the trigger parameter named "Jump"
//            anim.SetTrigger("leftStraightPunch");
			anim.SetTrigger("rightStraightPunch");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            
            //Send the message to the Animator to activate the trigger parameter named "Jump"
//            anim.SetTrigger("leftStraightPunch");
			anim.SetTrigger("leftStraightPunch");
        }
	}

	// void lfist_straight_punch(){
	// 	 anim.SetTrigger("lfist_straight_punch");
	// }
}
