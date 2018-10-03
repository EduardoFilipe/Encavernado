using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSackBehaviour : Enemy {

	public float timer, tmDeath;
	public float mag;
	public GameObject slime;
	bool died;
	GameObject gC;
	bool playdeathsound= true;

	//para os slimes pularem em momentos diferentes
	public float randomInitialJump;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		gC = GameObject.FindGameObjectWithTag ("GameController");
		timer = 0;
		died = false;
		normalSpeed = speed;
		enabled = false;

	}

	void FixedUpdate () {
		if (enabled == true) {
			Morte ();

			if (hp > 0) {
				Jump ();
			}
			SetOrder ();
			timer += Time.deltaTime;

			mag = transform.localScale.magnitude;
			if (hp <= 0) {
				PlayDead ();
				tmDeath += Time.deltaTime;
				GetComponent<Animator> ().SetTrigger ("Die");
				if (tmDeath > 1 && !died) {
					
					speed = 0;
					var newSlime = Instantiate (slime, transform.position + new Vector3 (Random.Range (-0.2f, 0.2f), Random.Range (-0.2f, 0.2f), 0), Quaternion.identity);
					var slimeScript = newSlime.GetComponent<SlimeBehaviour> ();
					slimeScript.sack = 1;
					var newSlime2 = Instantiate (slime, transform.position, Quaternion.identity);
					var slimeScript2 = newSlime2.GetComponent<SlimeBehaviour> ();
					slimeScript2.sack = 1;
					var newSlime3 = Instantiate (slime, transform.position, Quaternion.identity);
					var slimeScript3 = newSlime3.GetComponent<SlimeBehaviour> ();
					slimeScript3.sack = 1;
					died = true;
				} else if (tmDeath > 1.5f) {
					Destroy (gameObject);
				}
			}
			FlashEnemy ();
		}
	}




	void Jump () {
		if (timer > (3/hp) - 1) {
			MoveEnemy ();
		} 
		if (timer > (3/hp) - 1 + 0.8f) {
			transform.position = Vector2.MoveTowards (transform.position, transform.position, 0);
			timer = 0;
			gC.GetComponent<GameController>().audioManager.GetComponent<AudioManager> ().Play ("slimesackandando");
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

	void PlayDead() {
		if (playdeathsound == true) {
			gC.GetComponent<GameController> ().audioManager.GetComponent<AudioManager> ().Play ("slimesackmorrendo");
			playdeathsound = false;
		}
	}
		

}
