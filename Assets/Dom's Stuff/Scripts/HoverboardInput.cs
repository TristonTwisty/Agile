//Input system for the hoverboard
//seperate script just in case we want AI to use the hoverboard script too

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverboardInput : MonoBehaviour
{
	public string verticalAxisName = "Vertical";        //The name of the thruster axis
	public string horizontalAxisName = "Horizontal";    //The name of the rudder axis
	public string brakingKey = "Brake";                 //The name of the brake button

	//We hide these in the inspector because we want 
	//them public but we don't want people trying to change them
	[HideInInspector] public float thruster;            //The current thruster value
	[HideInInspector] public float rudder;              //The current rudder value
	[HideInInspector] public bool isBraking;            //The current brake value

    private void Update()
    {
		//Get the values of the thruster, rudder, and brake from the input class
		thruster = Input.GetAxis(verticalAxisName);
		rudder = Input.GetAxis(horizontalAxisName);
		isBraking = Input.GetButton(brakingKey);
	}
}
