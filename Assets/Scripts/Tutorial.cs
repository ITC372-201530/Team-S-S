using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	public Canvas Tut1;

	void OnTriggerEnter (Collider col)
	{
		if (col.transform.tag.Equals ("Player"))
		{
			Tut1.enabled = true;
		}

	}
	
	void OnTriggerExit (Collider col)
	{
		Tut1.enabled = false;
	}
}

