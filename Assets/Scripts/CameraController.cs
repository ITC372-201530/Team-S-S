using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public GameObject player;
	private Vector3 offset;
	private int cameraDistance = 1;
	
	void Start () 
	{
		offset = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if (player.transform.position.y >= 1.0) {
			transform.position = player.transform.position + offset;
		}


		if(Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.JoystickButton5))
		{
			if (cameraDistance == 1)
			{
				camera.fieldOfView = 100;
				cameraDistance = 2;
				return;
			}
			else if (cameraDistance == 2)
			{
				camera.fieldOfView = 55;
				cameraDistance = 1;
				return;
			}
		}

	}
	
}
