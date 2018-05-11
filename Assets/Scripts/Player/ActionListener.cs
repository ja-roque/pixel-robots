using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionListener : MonoBehaviour {

	Animator anim;
	MouseSwipes swipe;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		swipe = new MouseSwipes();
		
	}
	
	// Update is called once per frame
	void Update () {

		swipe.Swipe();
		if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("attacking"))
		{
				if (Input.GetKeyDown(KeyCode.UpArrow)){
					anim.SetTrigger("rightStraightPunch");
		        }
		
		        if (Input.GetKeyDown(KeyCode.DownArrow)){
					anim.SetTrigger("leftStraightPunch");
		        }
		
		        if (swipe.Swipe() == "rightSwipe"){
		        	anim.SetTrigger("rightSideHook");
		        }
		
		        if (swipe.Swipe() == "leftSwipe"){
		        	anim.SetTrigger("leftSideHook");
		        }
		
		        if (swipe.Swipe() == "upSwipe"){
		        	anim.SetTrigger("rightHook");
		        }
		}
	}

}
