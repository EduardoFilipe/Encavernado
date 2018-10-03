using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minhocao : Enemy {
	public GameObject waypointinstance, waypoint, buraco;
	float timer, timerAtk, timerDeath, ataque;
	int buracoCont;
	float attacksound,movendosound;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		gC = GameObject.FindGameObjectWithTag ("GameController");
		waypoint = Instantiate (waypointinstance, transform.position, Quaternion.identity);
		speed = player.GetComponent<PlayerBehaviour> ().speed;
		ataque = Random.Range (1, 6);
		GetComponent<CapsuleCollider2D> ().enabled = false;
		enabled = false;
	
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (enabled) {
			Morte ();

			timerAtk += Time.deltaTime;
			gameObject.GetComponent<SpriteRenderer> ().sortingOrder = (int)(-transform.position.y)+105;
			FlashEnemy ();
			Death ();
			if (timerAtk > 2) {
				Atacking ();
			} else {
				ataque = Random.Range (1, 7);
			}
		}
	}

	void Death () {
		if (hp <= 0) {
			timerDeath += Time.deltaTime;
			if (timerDeath > 1) {
				Destroy (gameObject);
			}
		}
	}

	void Atacking () {
		GameObject tempburaco;
		timer += Time.deltaTime;
		if (timer < ataque) {
			movendosound -= Time.deltaTime;
			PlayMovendo ();
			GetComponent<Animator> ().SetBool ("Atacando", false);
			GetComponent<Animator> ().SetBool ("Retraindo", false);
			waypoint.transform.position = player.transform.position;
			transform.position = waypoint.transform.position;
		} else {
			attacksound -= Time.deltaTime;
			PlayAtaque ();
			GetComponent<Animator> ().SetBool ("Atacando", true);
			GetComponent<Animator> ().SetBool ("Retraindo", false);
			if (buracoCont < 1) {
				tempburaco = Instantiate (buraco, transform.position, transform.rotation);
				buracoCont++;
				tempburaco.GetComponent<SpriteRenderer> ().sortingOrder = 1;
			}
			if (timer > ataque + 0.5f) {
				
				PlayAtaque ();
				GetComponent<CapsuleCollider2D> ().enabled = true;
				if (timer > ataque + 1.5f) {
					buracoCont = 0;
					timer = 0;
					timerAtk = 0;
					GetComponent<Animator> ().SetBool ("Atacando", false);
					GetComponent<Animator> ().SetBool ("Retraindo", true);
					GetComponent<CapsuleCollider2D> ().enabled = false;
				}
			}
		}
	}

	void PlayAtaque() {
		if (attacksound <= 0) {
			gC.GetComponent<GameController> ().audioManager.GetComponent<AudioManager> ().Play ("minhocaatacando");
			attacksound = 1;
		}
	}
	void PlayMovendo() {
		if (movendosound <= 0) {
			gC.GetComponent<GameController> ().audioManager.GetComponent<AudioManager> ().Play ("minhocamovendo");
			movendosound = 1;
		}
	}
}