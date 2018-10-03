using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : Enemy {
	public float timer, tmDeath;
	Animator slimeAnim;
	public float mag;
	public GameObject marca;
	public int sack;
	bool playdead = true;

	//para os slimes pularem em momentos diferentes
	public float randomInitialJump;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

		gC = GameObject.FindGameObjectWithTag ("GameController");
		randomInitialJump = Random.Range (0, 1f);

		timer = 0;
		slimeAnim = GetComponent<Animator> ();
		normalSpeed = speed;

		if (sack == 1) {
			enabled = true;
		} else {
			enabled = false;
		}

	}

	void FixedUpdate () {
		if (enabled == true) {
			Morte ();

			if (randomInitialJump > 0) {
				randomInitialJump -= Time.deltaTime / 2;
			} else {
				if (hp > 0) {
					Jump ();
					SlimeScale ();
				}
			}
			SetOrder ();
			timer += Time.deltaTime;
			mag = transform.localScale.magnitude;
			if (hp <= 0) {
				PlayDead ();
				tmDeath += Time.deltaTime;
				if (tmDeath < 1) {
					slimeAnim.SetBool ("Morrendo", true);
					speed = 0;
				} else {
					Destroy (gameObject);
				}
			}
			FlashEnemy ();
			if (hp <= 0) {
				//GetComponent<CircleCollider2D> ().enabled = false;
			}

			if (playDamageSound == true) {
				playDamageSound = false;
				if (hp > 0) {
					gC.GetComponent<GameController> ().audioManager.GetComponent<AudioManager> ().Play ("slimesofrendo");
				}
			}
		}
	}




	void Jump () {
		if (timer > hp - 1) {
			slimeAnim.SetBool ("Pulando", true);
			MoveEnemy ();

			//gC.GetComponent<GameController> ().DeixarMarcas (transform.position, marca);
		} 
		if (timer > hp - 1 + 0.8f) {
			slimeAnim.SetBool ("Pulando", false);
			transform.position = Vector2.MoveTowards (transform.position, transform.position, 0);
			gC.GetComponent<GameController>().audioManager.GetComponent<AudioManager> ().Play ("slimemovendo");
			timer = 0;
		}

	}

	void SlimeScale () {
		if (transform.localScale.x > ((float)hp*10 / 3) + 3.5f) {
			if (transform.localScale.x > 7f) {
				transform.localScale = new Vector2 (transform.localScale.x - 0.2f, transform.localScale.y - 0.2f);
			}
		}
	}
		
	void MoveEnemy () {
		if (PlayerBehaviour.dead == false) {
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, speed * Time.deltaTime);
		}
		if (transform.position.x > player.transform.position.x) {
			transform.eulerAngles = new Vector3 (0, 180, 0);
		} else {
			transform.eulerAngles = new Vector3 (0, 0, 0);
		}
	}

	//SOUNDS
	void PlayDead() {
		if (playdead == true) {
			playdead = false;
			gC.GetComponent<GameController>().audioManager.GetComponent<AudioManager> ().Play ("slimemorrendo");
		}
	}
}
