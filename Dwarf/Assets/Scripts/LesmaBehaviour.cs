using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesmaBehaviour : Enemy {
	public GameObject waypointinstance, waypoint, rastro;
	float timer, timerDth;
	float rastroTimer;
	bool playdeathsound = true;
	float andandotimer =0;
	// Use this for initialization
	void Start () {
		normalSpeed = speed;
		waypoint = Instantiate (waypointinstance, transform.position, Quaternion.identity);
		player = GameObject.FindGameObjectWithTag ("Player");
		gC = GameObject.FindGameObjectWithTag ("GameController");
		timer = 3;
		enabled = false;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer += Time.deltaTime;
		if (enabled == true) {
			Morte ();
			Move ();
			SetOrder ();
			FlashEnemy ();
			Death ();
		}
	}

	void Move () {
		if (timer > hp / 2) {
			waypoint.transform.position = transform.position + (new Vector3 (Random.Range (-5, 5), Random.Range (-5, 5), 0));
			timer = 0;
		}
		if (transform.position.x > waypoint.transform.position.x) {
			transform.eulerAngles = new Vector3 (0, 0, 0);
		} else {
			transform.eulerAngles = new Vector3 (0, 180, 0);
		}
		if (hp > 0) {
			andandotimer += Time.deltaTime;
			PlayAndando ();
	
			transform.position = Vector2.MoveTowards (transform.position, waypoint.transform.position, 5 / hp * speed * Time.deltaTime);
			DeixarRastro ();
		}
	}

	void Death () {
		if (hp <= 0) {
			PlayDead ();
			timerDth += Time.deltaTime;
			GetComponent<Animator> ().SetTrigger ("Die");
			if (timerDth > 0.7f) {
				Destroy (gameObject);
			}
		}
	}

	void DeixarRastro () {
		rastroTimer += Time.deltaTime;
		if (timer > 0.15f) {
			GameObject tempRastro;
			tempRastro = Instantiate (rastro, transform.position + new Vector3 (0, 0.5f, 0), transform.rotation);
			tempRastro.GetComponent<SpriteRenderer> ().sortingOrder = 1;
			rastroTimer = 0;
		}
	}

	void OnCollisionStay2D (Collision2D coll) {
		waypoint.transform.position = transform.position + (new Vector3(Random.Range(-3,3),Random.Range(-3,3),0));
	}

	void PlayDead() {
		if (playdeathsound == true) {
			playdeathsound = false;	
			gC.GetComponent<GameController> ().audioManager.GetComponent<AudioManager> ().Play ("lesmamorrendo");
		}
	}

	void PlayAndando () {
		if (andandotimer > 0.5f) {
			gC.GetComponent<GameController> ().audioManager.GetComponent<AudioManager> ().Play ("lesmaandando");
			andandotimer = 0;
		}
	}
}
