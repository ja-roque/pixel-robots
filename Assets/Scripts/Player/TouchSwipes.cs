using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSwipes{

	//inside class
	Vector2 firstPressPos;
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
	             }
	             //swipe down
	             if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
	             {
	                inputResponse.Type = "downSwipe";
	            	return inputResponse;
	             }
	             //swipe left
	             if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
	             {
	                inputResponse.Type = "leftSwipe";
	            	return inputResponse;
	             }
	             //swipe right
	             if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
	             {	                
	                inputResponse.Type = "rightSwipe";
	            	return inputResponse;
	             }
	         }
	     }

	     return new InputResponse("none", "none");
	}
}
