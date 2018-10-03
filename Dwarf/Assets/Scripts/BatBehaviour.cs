using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehaviour : Enemy {
	float timer, x, timerDeath;
	public GameObject waypointinstance, waypoint;

	float morcegoGrita = 0;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		gC = GameObject.FindGameObjectWithTag ("GameController");
		normalSpeed = speed;
		waypoint = Instantiate (waypointinstance, transform.position, Quaternion.identity);
		x = 0;
		enabled = false;
	
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (enabled == true) {
			Morte ();
			SetOrder ();
			timer += Time.deltaTime;
			if (disttoplayer > 7) {
				x = 0;
				speed = normalSpeed;
				morcegoGrita = 0;
			} else {
				MorcegoGrita ();

				x = 1.5f;
				speed = 2 *normalSpeed;
			}
			Move ();
			FlashEnemy ();
			if (hp <= 0) {
				timerDeath += Time.deltaTime;
				GetComponent<Animator> ().SetTrigger ("Die");
				if (timerDeath > 1) {
					Destroy (gameObject);
				}
			}
		}
	}

	void Move () {
		if (timer > x) {
			waypoint.transform.position = player.transform.position;
			timer = 0;
		}
		if (transform.position.x > waypoint.transform.position.x) {
			transform.eulerAngles = new Vector3 (0, 180, 0);
		} else {
			transform.eulerAngles = new Vector3 (0, 0, 0);
		}
		if (hp > 0) {
			transform.position = Vector2.MoveTowards (transform.position, waypoint.transform.position, speed * Time.deltaTime);
		}
	}

	void MorcegoGrita () {
		morcegoGrita -= Time.deltaTime;
		if (morcegoGrita <= 0) {
			morcegoGrita = 1.5f;
			gC.GetComponent<GameController> ().audioManager.GetComponent<AudioManager> ().Play ("morcegogritando");
		}
	}
}
