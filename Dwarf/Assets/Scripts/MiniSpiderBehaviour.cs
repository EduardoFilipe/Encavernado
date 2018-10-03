using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSpiderBehaviour : Enemy {
	public GameObject waypointinstance, waypoint;
	float timer, tmDeath;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		normalSpeed = speed;
		waypoint = Instantiate (waypointinstance, transform.position, Quaternion.identity);
		enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (enabled == true) {
			Morte ();
			AnimateSpider ();
			Move ();
			Death ();
			FlashEnemy ();
			SetOrder ();
		}
	}

	void Death () {
		if (hp <= 0) {
			tmDeath += Time.deltaTime;
			if (tmDeath < 1) {
				GetComponent<Animator> ().SetTrigger ("Die");
				speed = 0;
			} else {
				Destroy (gameObject);
			}
		}
	}

	void Move () {
		timer += Time.deltaTime;
		if (timer > 1) {
			waypoint.transform.position = transform.position + (new Vector3 (Random.Range (-5, 5), Random.Range (-5, 5), 0));
			timer = 0;
		}
		if (transform.position.x > waypoint.transform.position.x) {
			transform.eulerAngles = new Vector3 (0, 0, 0);
		} else {
			transform.eulerAngles = new Vector3 (0, 180, 0);
		}
		if (hp > 0) {
			transform.position = Vector2.MoveTowards (transform.position, waypoint.transform.position, speed * Time.deltaTime);
		}
	}

	void OnCollisionStay2D (Collision2D coll) {
		waypoint.transform.position = transform.position + (new Vector3(Random.Range(-3,3),Random.Range(-3,3),0));
	}

	void AnimateSpider () {
		if (transform.position.x == waypoint.transform.position.x) {
			GetComponent<Animator> ().SetBool ("Walking", false);
		} else {
			GetComponent<Animator> ().SetBool ("Walking", true);
		}
	}
}
