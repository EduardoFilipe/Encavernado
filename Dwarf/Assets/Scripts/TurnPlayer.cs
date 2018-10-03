using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlayer : MonoBehaviour {
	Vector2 direction;
	Vector3 mousePos;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		FaceMouse ();	
	}

	void FaceMouse () {
		mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		direction = new Vector2 (mousePos.x - transform.position.x, mousePos.y - transform.position.y);
		//transform.up = direction;
		transform.up = Vector3.Lerp(transform.up, direction, 1);
	}
}
