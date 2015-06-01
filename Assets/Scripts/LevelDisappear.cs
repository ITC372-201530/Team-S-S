using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelDisappear : MonoBehaviour {
	float duration = 5;	
	public Text text;
	Color myColor;
	float ratio;
	float Timer;

	// Use this for initialization
	void Start () {
		myColor = text.color;
		myColor.a = 1;
		text.enabled = true;
		Timer = 0;


	}
	
	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime;
		if(Timer >duration)
		{
			text.enabled = false;
		}

		ratio = Timer / duration;
		myColor.a = Mathf.Lerp (1, 0, ratio);
		text.color = myColor;
	}
}
