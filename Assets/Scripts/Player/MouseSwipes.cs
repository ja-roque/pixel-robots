using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To-Do: Separate screen areas to detect which fist to trigger on swipes.

public class MouseSwipes {

	//inside class
	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	 
	public string Swipe()
	{
	     if(Input.GetMouseButtonDown(0))
	     {
	         //save began touch 2d point
	        firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
	     }
	     if(Input.GetMouseButtonUp(0))
	     {
	            //save ended touch 2d point
	        secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
	       
	            //create vector from the two points
	        currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
	           
	        //normalize the 2d vector
	        currentSwipe.Normalize();
	 
	        //swipe upwards
	        if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
	        {
	            return "upSwipe";
	        }
	        //swipe down
	        if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
	        {
	            return "downSwipe";
	        }
	        //swipe left
	        if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
	        {
	            return "leftSwipe";
	        }
	        //swipe right
	        if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
	        {
	            return "rightSwipe";
	        }
	    }

	    return "none";
	}
}
