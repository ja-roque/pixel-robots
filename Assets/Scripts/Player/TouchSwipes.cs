using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSwipes{

	//inside class
	Vector2 firstPressPos;
	Vector2 firstPressPos2;

	Vector2 secondPressPos;
	Vector2 currentSwipe;

	InputResponse inputResponse = new InputResponse();

	static Vector2 screenSize = new Vector2(Screen.width, Screen.height);
	Vector2 screenCenterPoint = new Vector2(screenSize.x / 2, screenSize.y / 2);
 
	public InputResponse Swipe()
	{
	     if(Input.touches.Length > 0)
	     {
	         Touch t = Input.GetTouch(0);

	         if(t.phase == TouchPhase.Began)
	         {

	         	// Determine if it is a multitouch action
	         	if (Input.touches.Length > 1){
	         		Touch t2 = Input.GetTouch(1);
	         		firstPressPos2 = new Vector2(t2.position.x,t2.position.y);	

	         		Debug.Log(firstPressPos2);
	        		Debug.Log(firstPressPos);

	         		// Get positions of touch to block
	         		if( ((screenCenterPoint.x / 2) < firstPressPos.x  && firstPressPos.x  > (screenCenterPoint.x / 2)*3) &&
	         			((screenCenterPoint.x / 2) < firstPressPos2.x && firstPressPos2.x > (screenCenterPoint.x / 2)*3) &&
	         			((screenCenterPoint.y / 2) < firstPressPos.y) &&
	         			((screenCenterPoint.y / 2) < firstPressPos2.y)
         				)
	         		inputResponse.Type = "multiHoldTop";
	         		return inputResponse;
	         	}

              	//save began touch 2d point
	            firstPressPos = new Vector2(t.position.x,t.position.y);

				if(firstPressPos.x < screenCenterPoint.x){
					inputResponse.Side = "left";
				} else{
					inputResponse.Side = "right";
				}

	         }
	         if(t.phase == TouchPhase.Ended)
	         {
	              //save ended touch 2d point
	             secondPressPos = new Vector2(t.position.x,t.position.y);
	                           
	              //create vector from the two points
	             currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
	               
	             //normalize the 2d vector
	             currentSwipe.Normalize();
	 
	             //swipe upwards
	             if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
	             {
	                inputResponse.Type = "upSwipe";
	            	return inputResponse;
	             } else if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
	             {
	                inputResponse.Type = "downSwipe";
	            	return inputResponse;
	             } else if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
	             {
	                inputResponse.Type = "leftSwipe";
	            	return inputResponse;
	             } else if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
	             {	                
	                inputResponse.Type = "rightSwipe";
	            	return inputResponse;
	             } else {
	             	inputResponse.Type = "tap";
	             	return inputResponse;
	             }


	         }
	     }

	     return new InputResponse("none", "none");
	}
}
