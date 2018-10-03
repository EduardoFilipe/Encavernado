using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour {
	public GameObject explosao, gC;
	public float speed, damage, lifeTime, fireRate;
	public float counter, force;
	public float timer, timerArea;
	public bool area, bomba, trap, bombaRoll, tocou;
	public string som;

	// Use this for initialization
	void Start () {
		tocou = false;
		gC = GameObject.FindGameObjectWithTag ("GameController");
		counter = 0;
		if (trap) {
			gameObject.GetComponent<CircleCollider2D> ().enabled = false;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!area) {
			gameObject.GetComponentInChildren<SpriteRenderer> ().sortingOrder = (int)(-transform.position.y) + 100;
			if (speed - counter <= 0) {
				if (!tocou) {
					gC.GetComponent<GameController> ().audioManager.GetComponent<AudioManager> ().Play (som);
					tocou = true;
				}
				Destroy (gameObject.GetComponentInChildren<RotateOnPivot> ());
				timer += Time.deltaTime;
				if (bomba) {
					explosao.GetComponent<ExplosaoBehaviour> ().damage = damage;
					Instantiate (explosao, transform.position, explosao.transform.rotation);
					Destroy (gameObject);
				}
				if (!trap) {
					Destroy (gameObject.GetComponent<CircleCollider2D> ());
					if (timer > 2) {
						Destroy (gameObject);
					}
				} else {
					GetComponentInChildren<SpriteChanger> ().MudarSprite ();
					gameObject.GetComponent<CircleCollider2D> ().enabled = true;
				}
			} else {
				counter += Time.deltaTime * lifeTime;
				transform.Translate (Vector2.up * (speed - counter) * Time.deltaTime);
			}
		} else {
			timerArea += Time.deltaTime;
			transform.localScale = new Vector2 (transform.localScale.x + speed, transform.localScale.y + speed);
			Destroy (gameObject, lifeTime);
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag.Equals ("Enemy") || coll.gameObject.tag.Equals ("Minhocão")) {
			if (!area && !bombaRoll) {
				if (bomba) {
					explosao.GetComponent<ExplosaoBehaviour> ().damage = damage;
					Instantiate (explosao, transform.position, transform.rotation);
					Destroy (gameObject);
				}
				coll.gameObject.SendMessage ("Damage", damage, SendMessageOptions.DontRequireReceiver);
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerStay2D (Collider2D coll) {
		if (coll.gameObject.tag.Equals ("Enemy") || coll.gameObject.tag.Equals ("Minhocão")) {
			if (area && timerArea > 0.1f) {
				coll.gameObject.SendMessage ("Damage", damage, SendMessageOptions.DontRequireReceiver);
				timerArea = 0;
			}
		}
	}
}

