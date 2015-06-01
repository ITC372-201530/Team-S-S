using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialDisappear : MonoBehaviour {
	float duration = 2;	
	public Text text;
	Color myColor;
	float ratio;
	float Timer;
	public Controller1 player;
	private bool moved = false;
	// Use this for initialization
	void Start () {
		myColor = text.color;
		myColor.a = 1;
		text.enabled = true;
		Timer = 0;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.movement.z != 0 || player.movement.y != 0) 
		{
			moved = true;

		}

		if (moved)
		{
			Timer += Time.deltaTime;
			if (Timer > duration) {
				text.enabled = false;
			}
			
			ratio = Timer / duration;
			myColor.a = Mathf.Lerp (1, 0, ratio);
			text.color = myColor;
		}
	}
}
