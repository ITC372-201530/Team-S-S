using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour {
	public float speed = 6.0F;
	public float jumpSpeed = 10.0F;
	public float gravity = 20.0F;
	public int maxFallDistance;
	private Vector3 moveDirection = Vector3.zero;
	private bool isRestarting = false;
	public Slider shadowBar;
	public float shadowEnergy;
	GameObject [] shadows;
	public float pushPower = 10.0F;
	public Canvas wintext;
	public GameObject Sun;
	private float playerPositionZ;
	

	void Update() {
		shadowBar.value = shadowEnergy;

		shadows = GameObject.FindGameObjectsWithTag("Shadow");
		CharacterController controller = GetComponent<CharacterController>();

		if (controller.isGrounded) {
			moveDirection = new Vector3(0, 0, Input.GetAxis("Horizontal"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if (Input.GetKey(KeyCode.W))
			{				
				moveDirection.y = jumpSpeed*2;
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

			if(isRestarting == false)
			{
				RestartLevel();
			}
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

		if(Input.GetKey(KeyCode.UpArrow))
		{
			if (shadowEnergy > 0f)
			{

				foreach (GameObject sdw in shadows)
				{
					sdw.collider.enabled = true;
				}
				shadowEnergy -= 0.1f;
			}
		}

		if(Input.GetKeyUp(KeyCode.UpArrow))
		{
			foreach (GameObject sdw in shadows)
			{
				sdw.collider.enabled = false;
			}
		}

//		if(Input.GetKeyDown(KeyCode.R))
//		{
//			playerPositionZ = transform.position.z;
//			Sun.transform.position = new Vector3(Sun.transform.position.x, Sun.transform.position.y, playerPositionZ);
//		}
		
		if(shadowEnergy <= 0f)
		{
			foreach (GameObject sdw in shadows)
			{
				sdw.collider.enabled = false;
			}
		}


	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Collectable") 
		{
			shadowEnergy += 15f;
			other.gameObject.SetActive(false);
		}

		if (other.gameObject.tag == "Win") 
		{
			wintext.enabled = true;
		}
		
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic)
			return;
		
		if (hit.moveDirection.y < -0F)
			return;
		
		Vector3 pushDir = new Vector3(0, 0, hit.moveDirection.z);
		body.velocity = pushDir * pushPower * 2;
	}

	void RestartLevel ()
	{
		isRestarting = true;
		isRestarting = false;
		Application.LoadLevel ("Level01");
	}
}