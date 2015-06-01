using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller1 : MonoBehaviour {
	public float speed = 6.0F;
	public float jumpSpeed = 10.0F;
	public float gravity = 20.0F;
	public int maxFallDistance;
	private Vector3 moveDirection = Vector3.zero;
	private bool isRestarting = false;
	public Slider shadowBar;
	public Slider sunLocation;
	public float shadowEnergy;
	private float difference = 0;
	GameObject [] shadows;
	public float pushPower = 10.0F;
	public Canvas wintext;
	public Canvas pauseMenu;

	public bool paused = false;
	private bool levelCompleted = false;

	public GameObject Sun;
	public GameObject DrownSound;
	public GameObject SolidifySound;
	public GameObject PushSound;
	public GameObject ShadowGone;
	public GameObject ShadowShard;
	public GameObject LevelComplete;

	private float playerPositionZ;
	private Vector3 targetAngles;
	float move;
	float jump;
	private float myTimer = 0;
	private int time;
//	static bool restarted = false;
	private Vector3 sunPosition;
	private Vector3 playerPosition;
	private bool mainMenu;
	public Vector3 movement;


	void start ()
	{

	}
	
	void Update() {


		Animator animator = GetComponent<Animator> ();
		CharacterController controller = GetComponent<CharacterController> ();
		shadowBar.value = shadowEnergy;
		sunLocation.value = 200 + difference;
		shadows = GameObject.FindGameObjectsWithTag ("Shadow");
		move = Input.GetAxis ("Horizontal");
		jump = Input.GetAxis ("Jump");
		movement = controller.velocity;

		if (Application.loadedLevel != 0)
		{
			mainMenu = false;
		}
		else
		{
			mainMenu = true;
		}


//		if (restarted == false)
//		{
//			RestartLevel ();
//			restarted = true;
//		}
		if (!paused) {




			sunPosition = GameObject.FindGameObjectWithTag ("Sun").transform.position;
			playerPosition = transform.position;

			difference = 2 * (sunPosition.z - playerPosition.z);




			if (controller.velocity.z == 0) {
				PushSound.SetActive (false);
				myTimer += Time.deltaTime;
				time = (int)Mathf.Round (myTimer);

				if (time >= 2) {
					animator.SetFloat ("Key", 0.2f);
				}
			}

			if (controller.velocity.z != 0 && controller.isGrounded) {
				PushSound.SetActive (true);
			}

			if (controller.velocity.z != 0) {

				myTimer = 0;
				animator.SetFloat ("Key", 0.0f);
			}
			if (!controller.isGrounded) {
				PushSound.SetActive (false);
			}





			if (Input.GetKeyUp (KeyCode.UpArrow) || Input.GetKeyUp (KeyCode.JoystickButton4)) {
				ShadowGone.SetActive (false);
				SolidifySound.SetActive (false);
				foreach (GameObject sdw in shadows) {
					sdw.collider.enabled = false;
				}
			}



			if (controller.isGrounded) {


				moveDirection = new Vector3 (0, 0, move);
				moveDirection = transform.TransformDirection (moveDirection);
				moveDirection *= speed;
				animator.SetFloat ("Walk", move);


				if (jump == 1f || Input.GetKey (KeyCode.W)) {				
					moveDirection.y = jumpSpeed * 2;
				}

			} else {

				animator.SetFloat ("Walk", move);
				moveDirection = new Vector3 (0, moveDirection.y, move);
				moveDirection = transform.TransformDirection (moveDirection);
				moveDirection.x *= speed;
				moveDirection.z *= speed;
			}





			if (transform.position.y < 0) {
				DrownSound.SetActive (true);

				if (transform.position.y < -70) {
					if (isRestarting == false) {

						RestartLevel ();
					}
				}
			}
			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move (moveDirection * Time.deltaTime);




//		if(Input.GetKeyDown(KeyCode.R))
//		{
//			playerPositionZ = transform.position.z;
//			Sun.transform.position = new Vector3(Sun.transform.position.x, Sun.transform.position.y, playerPositionZ);
//		}
		
			if (shadowEnergy <= 0f) {
				SolidifySound.SetActive (false);
				foreach (GameObject sdw in shadows) {
					sdw.collider.enabled = false;
				}
			}
		}

		if (!mainMenu) 
		{
			if (!levelCompleted) 
			{
				if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.JoystickButton7)) 
				{
					paused = !paused;
					if (paused) 
					{
						pauseMenu.enabled = true;

					} 
					else 
					{
						pauseMenu.enabled = false;

					}
				}
				if (paused)
				{
					if (Input.GetKeyDown (KeyCode.R) || Input.GetKeyDown (KeyCode.JoystickButton1)) 
					{
						RestartLevel();
					}
				}
			}
		}

		if (paused)
		{
			animator.SetFloat ("Walk", 0);
			PushSound.SetActive (false);
			SolidifySound.SetActive (false);
			ShadowGone.SetActive (false);
			if(Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown (KeyCode.JoystickButton0))
			{
				Application.LoadLevel ("Main Menu");
			}
		}

		if (levelCompleted) 
		{
			if (Input.GetKeyDown (KeyCode.T) || Input.GetKeyDown (KeyCode.JoystickButton2)) 
			{
				NextLevel ();
			}
			
		}


	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject. tag == "Collectable") 
		{
			ShadowShard.GetComponent<AudioSource>().Play();
			shadowEnergy += 15f;
			other.gameObject.SetActive(false);
		}

		if (other.gameObject.tag == "Win") 
		{
			wintext.enabled = true;
			LevelComplete.SetActive (true);
			paused = true;
			levelCompleted = true;

		}

		if (other.gameObject.tag == "Tutorial") 
		{
			Application.LoadLevel ("Tutorial");
			
		}

		if (other.gameObject.tag == "Level01") 
		{
			Application.LoadLevel ("Level01");
			
		}
		if (other.gameObject.tag == "Level02") 
		{
			Application.LoadLevel ("Level02");
			
		}
		if (other.gameObject.tag == "Level03") 
		{
			Application.LoadLevel ("Level03");
			
		}
		if (other.gameObject.tag == "Bonus") 
		{
			Application.LoadLevel ("Bonus");
			
		}
		
	}
	
	void FixedUpdate()
	{
		if (!paused) 
		{
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.JoystickButton4)) {
				if (shadowEnergy <= 0f) {
					ShadowGone.SetActive (true);
				}

				if (shadowEnergy > 0f) {
					SolidifySound.SetActive (true);
					foreach (GameObject sdw in shadows) {
						sdw.collider.enabled = true;
					}
					shadowEnergy -= 0.1f;
				}
			}
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

	void NextLevel ()
	{
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	void RestartLevel ()
	{
		isRestarting = true;
		isRestarting = false;
		Application.LoadLevel (Application.loadedLevel);
	}
}