using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBehaviour : Enemy {
	public GameObject waypointinstance, waypoint, web, aim;
	float timerShot, tmDeath;
	bool tiro;
	bool playDeathSound = true;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		gC = GameObject.FindGameObjectWithTag ("GameController");
		normalSpeed = speed;
		waypoint = Instantiate (waypointinstance, transform.position, Quaternion.identity);
		InvokeRepeating("ChangeWP",2f,2f);
		tiro = false;
		enabled = false;
	
	}

	void AI() {
		if (hp > 0) {
			if (disttoplayer > 10 && disttoplayer < 20) {
				Shot ();
			} else {
				MoveToWp ();
			}
		}
	}

	void MoveToWp (){
		AnimateSpider ();
		if (PlayerBehaviour.dead == false) {
			transform.position = Vector2.MoveTowards (transform.position, waypoint.transform.position, speed * Time.deltaTime);
			//gC.GetComponent<GameController> ().audioManager.GetComponent<AudioManager> ().Play ("aranhaatacando");
		}
		if (transform.position.x > waypoint.transform.position.x) {
			transform.eulerAngles = new Vector3 (0, 0, 0);
		} else {
			transform.eulerAngles = new Vector3 (0, 180, 0);
		}
	}

	void OnCollisionStay2D (Collision2D coll) {
		waypoint.transform.position = transform.position + (new Vector3(Random.Range(-3,3),Random.Range(-3,3),0));
	}

	void ChangeWP (){
		waypoint.transform.position =  player.transform.position;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (enabled == true) {
			Morte ();
			AI ();
			SetOrder ();
			timerShot += Time.deltaTime;
			FlashEnemy ();
			if (playDamageSound == true) {
				if (hp > 0) {
					gC.GetComponent<GameController> ().audioManager.GetComponent<AudioManager> ().Play ("aranhasofrendo");
				}
				playDamageSound = false;
			}

			if (hp <= 0) {
				PlayDead ();
				tmDeath += Time.deltaTime;
				if (tmDeath < 1) {
					GetComponent<Animator> ().SetTrigger ("Die");
					speed = 0;
				} else {
					Destroy (gameObject);
				}
			}
		}
	}

	void PlayDead() {
		if (playDeathSound == true) {
			playDeathSound = false;
			gC.GetComponent<GameController> ().audioManager.GetComponent<AudioManager> ().Play ("aranhamorrendo");
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {
		gC.GetComponent<GameController> ().audioManager.GetComponent<AudioManager> ().Play ("aranhaatacando");
	}
	void Shot () {
		if (timerShot > 3) {
			Vector2 direction = new Vector2 (player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
			aim.transform.up = Vector3.Lerp(transform.up, direction, 1);
			if (!tiro) {
				GetComponent<Animator> ().SetTrigger ("Shot");
				tiro = true;
				if (GetComponent<Animator> ().GetBool ("Walking") == false) {
					if (player.transform.position.x < transform.position.x) {
						GetComponent<SpriteRenderer> ().flipX = true;
					} else {
						GetComponent<SpriteRenderer> ().flipX = false;
					}
				} else {
					GetComponent<SpriteRenderer> ().flipX = false;
				}
			}
		}
		if (timerShot > 3.3f) {
			gC.GetComponent<GameController> ().audioManager.GetComponent<AudioManager> ().Play ("aranhaatirando");
			Instantiate (web, aim.transform.position, aim.transform.rotation);
			timerShot = 0;
			tiro = false;
		}

	}

	void AnimateSpider () {
		if (transform.position.x <= waypoint.transform.position.x + 1f && transform.position.x >= waypoint.transform.position.x - 1f
			&& transform.position.y <= waypoint.transform.position.y + 1f && transform.position.y >= waypoint.transform.position.y - 1f) {
			GetComponent<Animator> ().SetBool ("Walking", false);
		} else {
			GetComponent<Animator> ().SetBool ("Walking", true);
		}
	}
}
