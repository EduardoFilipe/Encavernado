using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormigaBehaviour : Enemy {
	public GameObject waypointinstance, waypoint;
	float ataque, random;

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
			SetOrder ();
			FlashEnemy ();
			Move ();
			ataque += Time.deltaTime;
			random += Time.deltaTime;
			if (hp <= 0) {
				Destroy (gameObject);
			}
		}
	}

	void OnCollisionStay2D (Collision2D coll) {
		waypoint.transform.position = transform.position + (new Vector3(Random.Range(-3,3),Random.Range(-3,3),0));
	}

	void Move () {
		if ((player.transform.position.x >= transform.position.x - 2 && player.transform.position.x <= transform.position.x + 2) ||
			(player.transform.position.y >= transform.position.y - 2 && player.transform.position.x <= transform.position.y + 2)) {

			if (ataque > 1f) {
				waypoint.transform.position = player.transform.position;
				ataque = 0;
			}
			if (hp > 0) {
				GetComponent<SpriteRenderer> ().color = new Color (1, 0, 0);
				transform.position = Vector2.MoveTowards (transform.position, waypoint.transform.position, speed * 3 * Time.deltaTime);
			}
		} else {
			if (random > 2) {
				waypoint.transform.position = transform.position + (new Vector3 (Random.Range (-5, 5), Random.Range (-5, 5), 0));
				random = 0;
			}
			if (hp > 0) {
				GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
				transform.position = Vector2.MoveTowards (transform.position, waypoint.transform.position, speed * Time.deltaTime);
			}
		}
	}
}
