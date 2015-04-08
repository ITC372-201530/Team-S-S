using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public int maxFallDistance;
	private Vector3 moveDirection = Vector3.zero;
	private bool isRestarting = false;



	void Update() {
		CharacterController controller = GetComponent<CharacterController>();


		if (controller.isGrounded) {
			moveDirection = new Vector3(0, 0, Input.GetAxis("Horizontal"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if (Input.GetKey(KeyCode.W))
			{				
				moveDirection.y = jumpSpeed;
			}	
		}	
		else {
			moveDirection = new Vector3(0, moveDirection.y, Input.GetAxis("Horizontal"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection.x *= speed;
			moveDirection.z *= speed;
		}



		if (transform.position.y < -10) 
		{
			Debug.Log("Error");
			if(isRestarting == false)
			{
				RestartLevel();
			}
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Collectable") 
		{
			other.gameObject.SetActive(false);
			//do something
		}

	}

	void RestartLevel ()
	{
		isRestarting = true;
		isRestarting = false;
		Application.LoadLevel ("Level01");
	}
}