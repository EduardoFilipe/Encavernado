using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

	public bool onFade;
	public float speed;

	void Awake () {
		speed = speed / 100;
		onFade = false;
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
	}

	void Update () {
		if (onFade == false)
			fadeOut ();
		else
			fadeIn ();
	}

	public void fadeOut() {
		if (gameObject.GetComponent<SpriteRenderer> ().color.a > 0) {
			gameObject.GetComponent<SpriteRenderer> ().color -= new Color(0,0,0,speed);
		}
	}
	public void fadeIn() {
		if (gameObject.GetComponent<SpriteRenderer> ().color.a <1) {
			gameObject.GetComponent<SpriteRenderer> ().color += new Color(0,0,0,speed);
		}
	}

}
