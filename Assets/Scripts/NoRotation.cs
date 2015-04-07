using UnityEngine;
using System.Collections;

public class NoRotation : MonoBehaviour {

	public Vector3 Rotation;
	public Vector3 Position;

	// Use this for initialization
	void Start () {
		Rotation = new Vector3 (0.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		Position = new Vector3 (0, Position.y, Position.z);
		transform.rotation = Quaternion.Euler(Rotation);

		//this.transform.position = Position;
	}
}
