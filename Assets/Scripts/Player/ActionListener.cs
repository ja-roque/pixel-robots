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
		
		if (touch.Swipe().Type == "tap" && touch.Swipe().Side == "right"){
			anim.SetTrigger("rightStraightPunch");
        }

        if (touch.Swipe().Type == "tap" && touch.Swipe().Side == "left"){
			anim.SetTrigger("leftStraightPunch");
        }

        if (touch.Swipe().Type == "rightSwipe"){
        	anim.SetTrigger("rightSideHook");
        }

        if (touch.Swipe().Type == "leftSwipe"){
        	anim.SetTrigger("leftSideHook");
        }

        if (touch.Swipe().Type == "upSwipe" 
        	&& touch.Swipe().Side == "right"){
        	anim.SetTrigger("rightUppercut");
        } else if(mouse.Swipe().Type == "upSwipe" 
    		&& touch.Swipe().Side == "left") {
        	anim.SetTrigger("leftUppercut");
        }

		if (touch.Swipe().Type == "multiHoldTop"){
					anim.SetBool("highBlock", true);
		        } else{
		        	anim.SetBool("highBlock", false);
        }

        if (touch.Swipe().Type == "multiHoldBot"){
					anim.SetBool("lowBlock", true);
		        } else{
		        	anim.SetBool("lowBlock", false);
        }
	}

}
