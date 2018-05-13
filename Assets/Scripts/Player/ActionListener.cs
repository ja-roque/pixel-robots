using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionListener : MonoBehaviour {

	Animator anim;
	MouseSwipes mouse;
	TouchSwipes touch;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		mouse = new MouseSwipes();
		touch = new TouchSwipes();
		
	}
	
	// Update is called once per frame
	void Update () {
		touch.Swipe();
		mouse.Swipe();
		
		if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("attacking"))
		{
				if (Input.GetKeyDown(KeyCode.UpArrow)){
					anim.SetTrigger("rightStraightPunch");
		        }
		
		        if (Input.GetKeyDown(KeyCode.DownArrow)){
					anim.SetTrigger("leftStraightPunch");
		        }
		
		        if (mouse.Swipe().Type == "rightSwipe"){
		        	anim.SetTrigger("rightSideHook");
		        }
		
		        if (mouse.Swipe().Type == "leftSwipe"){
		        	anim.SetTrigger("leftSideHook");
		        }
		
		        if (mouse.Swipe().Type == "upSwipe" && mouse.Swipe().Side == "right"){
		        	anim.SetTrigger("rightHook");
		        } else if(mouse.Swipe().Type == "upSwipe" && mouse.Swipe().Side == "left") {
		        	anim.SetTrigger("leftHook");
		        }
		}
	}

}
