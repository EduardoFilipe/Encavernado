using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour : MonoBehaviour {

	GameObject player;
	float dist;
	SpriteRenderer sprt;
	void Start () {
	player = GameObject.FindGameObjectWithTag ("Player");
		sprt = gameObject.GetComponent<SpriteRenderer> ();
	}

	void Update() {
		dist = Vector2.Distance (transform.position, player.transform.position);
		if (dist > 2f) {
			if (sprt.color.a > 0.1f)	
				sprt.color += new Color (0, 0, 0, -0.005f);
		} else {
			if (sprt.color.a <= 0.2f)
				sprt.color += new Color (0, 0, 0, 0.005f);
		}

	}
	
	// Update is called once per frame
//	void Update () {
//		dist = Vector2.Distance (transform.position, player.transform.position);
//		if (dist > 6) {
//			if (sprt.color != new Color (1, 1, 1, 0.5f))
//				sprt.color = new Color (1, 1, 1, 0.5f);
//		} else if (dist < 2) {
//
//		}
//	}

//	void OnTriggerExit2D (Collider2D coll) {
//		if (coll.tag == "player")
//			onplayer = 0;
//		print ("saiu");
//	}
//
//	void OnTriggerEnter2D (Collider2D coll) {
//		if (coll.tag == "player")
//			onplayer = 1;
//		print ("entrou");
//	}
//
//	void Update() {
//		if (onplayer == 0) {
//			if (sprt.color.a < 0.8f) {
//				sprt.color += new Color (0, 0, 0, 0.05f);
//			}
//		} else {
//			if (sprt.color.a > 0.2f) {
//				sprt.color += new Color (0, 0, 0, -0.05f);
//			}
//		}
//	}
}
