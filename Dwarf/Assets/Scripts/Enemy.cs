using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


	public float speed, normalSpeed, force, hp;
	[HideInInspector]
	public float disttoplayer;
	[HideInInspector]
	public GameObject player;
	public GameObject hpRecover;
	//[HideInInspector]
	public bool enabled;

	[HideInInspector]
	public GameObject  gC;
	[HideInInspector]
	public int  id, flash;
	[HideInInspector]
	public float flashTimer;

	[HideInInspector]
	public bool playDamageSound;


	void Start () {
		playDamageSound = false;
		normalSpeed = speed;
		enabled = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		SetOrder ();
	

	}

	public void SetOrder () {
		gameObject.GetComponent<SpriteRenderer> ().sortingOrder = (int)(-transform.position.y)+100;

	}

	void Update () {
		PlayerDistance ();
	}

	public void Damage (float damage) {
		if (enabled) {
			hp -= damage;
			flash = 10;
			playDamageSound = true;

		}
	}

	public void Morte () {
		if (hp <= 0) {
			tag = "Untagged";
		}
	}

	void OnCollisionEnter2D (Collision2D coll){
		if (coll.gameObject.tag.Equals ("Player")) {
			Vector2 dir;
			dir = (Vector2)coll.transform.position - (Vector2)transform.position;
			dir.Normalize ();
			coll.gameObject.GetComponent<Rigidbody2D> ().AddForce (dir * force*10);
		}
	}

	public void FlashEnemy () {
		if (flash > 0) {
			speed = 0;
			flashTimer += Time.deltaTime;
			if (flashTimer > 0.05f) {
				if (flash % 2 == 0) {
					GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
				} else {
					GetComponent<SpriteRenderer> ().color = new Color (1, 0.56f, 0.56f);
				}
				flashTimer = 0;
				flash--;
			}
		} else {
			GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
			speed = normalSpeed;
		}
	}

	void Ativar () {
		enabled = true;
	}


	void OnDestroy () {
		GetComponentInParent<RoomBehaviour> ().enemysCont--;
		int rand = Random.Range (0, 6);
		if (rand == 0) {
			Instantiate (hpRecover, transform.position, transform.rotation);
		}
	}

	public void PlayerDistance () {
		disttoplayer = Vector2.Distance (transform.position, player.transform.position);
	}



}