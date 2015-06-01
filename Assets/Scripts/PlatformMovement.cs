﻿using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour {

	public Transform movingPlatform;
	public Transform position1;
	public Transform position2;
	public Vector3 newPosition;
	public string currentState;
	public float smooth;
	public float resetTime;
	public Controller1 player;

	// Use this for initialization
	void Start () {
		changeTarget ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (!player.paused)
		{
			movingPlatform.position = Vector3.Lerp (movingPlatform.position, newPosition, smooth * Time.deltaTime);
		}
	}

	void changeTarget ()
	{
		if (currentState == "Moving to Position 1")
		{
			currentState = "Moving to Position 2";
			newPosition = position2.position;
		}
		else if (currentState == "Moving to Position 2")
		{
			currentState = "Moving to Position 1";
			newPosition = position1.position;
		}
		else if (currentState == "")
		{
			currentState = "Moving to Position 2";
			newPosition = position2.position;
		}
		Invoke ("changeTarget", resetTime);
	}
}
