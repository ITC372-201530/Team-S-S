using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

	private Vector3 sunPosition;
	private Vector3 cubePosition;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		sunPosition = GameObject.FindGameObjectWithTag ("Sun").transform.position;
		cubePosition = GameObject.FindGameObjectWithTag ("Cube").transform.position;
		transform.position = new Vector3(0.0f, cubePosition.y -(sunPosition.y - cubePosition.y), cubePosition.z -(sunPosition.z - cubePosition.z));
	}
}
