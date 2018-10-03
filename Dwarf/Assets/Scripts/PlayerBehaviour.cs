using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour {
	Rigidbody2D rb;
	static public bool dead;
	float timerDeath;
	public GameObject fade;

	//movement
	public float speed, timerSpeed;
	public float normalspeed;
	Animator playerAnim;
	int flash, id;
	public float flashTimer;

	//aim
	public string nomeArma1, nomeArma2;
	public GameObject aim, arma1, arma2, gC, dropArma1, dropArma2;
	public float timerAtk, timerShot1, timerShot2, fireRateP, fire1, fire2;
	public Camera currentCamera;

	public GameObject grave;
	void Start () {
		fireRateP = 1;
		gC = GameObject.FindGameObjectWithTag ("GameController");
		rb = gameObject.transform.GetComponent<Rigidbody2D> ();
		playerAnim = GetComponent <Animator> ();
		dead = false;
		normalspeed = speed;
		timerSpeed = 2;
	}

	void FixedUpdate () {
		if (!dead) {
			if (arma1 != null)
				fire1 = arma1.GetComponent<Arma> ().fireRate / fireRateP;
			if (arma2 != null)
				fire2 = arma2.GetComponent<Arma> ().fireRate / fireRateP;
			RotatePlayer ();
			Movement ();
			Shot ();
			ReduceSpeed ();
			SetOrder ();
			Death ();
		} else {
			timerDeath += Time.deltaTime;
			if (timerDeath > 2f) {
				SceneManager.LoadScene ("salas");
			}
		}
		timerAtk += Time.deltaTime;
		timerShot1 += Time.deltaTime;
		timerShot2 += Time.deltaTime;
		FlashPlayer ();

		if (nomeArma1 != "") {
			arma1 = gC.GetComponent<GameController> ().armas [int.Parse (nomeArma1)];
			gC.GetComponent<GameController> ().arma1.sprite = arma1.GetComponentInChildren<SpriteRenderer> ().sprite;
		}
		if (nomeArma2 != "") {
			arma2 = gC.GetComponent<GameController> ().armas [int.Parse (nomeArma2)];
			gC.GetComponent<GameController> ().arma2.sprite = arma2.GetComponentInChildren<SpriteRenderer> ().sprite;
		}
	}

	void Death () {
		if (gC.GetComponent<GameController> ().playerHP <= 0) {
			dead = true;
			playerAnim.SetBool ("Die", true);
			playerAnim.Play ("Die");
		}
	}
	void OnDestroy () {
		Instantiate (grave, transform.position, Quaternion.identity);
	}
	void RotatePlayer () {
		if (aim.transform.eulerAngles.z >= 0 && aim.transform.eulerAngles.z < 45 || aim.transform.eulerAngles.z < 360 && aim.transform.eulerAngles.z >= 315) {
			playerAnim.SetBool ("Horizontal", false);
			playerAnim.SetBool ("Vertical", false);
			playerAnim.SetBool ("Up", true);
			transform.eulerAngles = new Vector2 (0, 0);
		} else if (aim.transform.eulerAngles.z >= 45 && aim.transform.eulerAngles.z < 135) {
			playerAnim.SetBool ("Horizontal", true);
			playerAnim.SetBool ("Vertical", false);
			playerAnim.SetBool ("Up", false);
			transform.eulerAngles = new Vector2 (0, 180);
		} else if (aim.transform.eulerAngles.z >= 135 && aim.transform.eulerAngles.z < 225) {
			playerAnim.SetBool ("Horizontal", false);
			playerAnim.SetBool ("Vertical", true);
			playerAnim.SetBool ("Up", false);
			transform.eulerAngles = new Vector2 (0, 0);
		} else if (aim.transform.eulerAngles.z >= 225 && aim.transform.eulerAngles.z < 315) {
			playerAnim.SetBool ("Horizontal", true);
			playerAnim.SetBool ("Vertical", false);
			playerAnim.SetBool ("Up", false);
			transform.eulerAngles = new Vector2 (0, 0);
		}
	}
		
	 void Movement () {
		float directionX, directionY;
		directionX = Input.GetAxisRaw("Horizontal");
		directionY = Input.GetAxisRaw ("Vertical");
		transform.Translate (new Vector2 (directionX, directionY) * speed * Time.deltaTime,Space.World);
		if (directionX != 0 || directionY != 0) {
			playerAnim.SetBool ("Walking", true);
		} else {
			playerAnim.SetBool ("Walking", false);
		}
	}

	void DropArma1 () {
		if (arma1 != null) {
			dropArma1.SetActive (true);
			dropArma1.transform.position = transform.position;
			dropArma1.transform.rotation = transform.rotation;
		}
	}

	void DropArma2 () {
		if (arma2 != null) {
			dropArma2.SetActive (true);
			dropArma2.transform.position = transform.position;
			dropArma2.transform.rotation = transform.rotation;
		}
	}

	void Shot(){
		if (Input.GetKey(KeyCode.Mouse0) && arma1 != null) {
			if (timerShot1 > fire1) {
				Instantiate (arma1, aim.transform.position, aim.transform.rotation);
				timerShot1 = 0;

				Vector2 dir;
				dir = (Vector2)aim.transform.position - (Vector2)transform.position;
				dir.Normalize ();
				gameObject.GetComponent<Rigidbody2D> ().AddForce (-aim.transform.up * arma1.GetComponent<Arma>().force * 10);
			}
		}
		if (Input.GetKey(KeyCode.Mouse1) && arma2 != null) {
			if (timerShot2 > fire2) {
				Instantiate (arma2, aim.transform.position, aim.transform.rotation);
				timerShot2 = 0;

				Vector2 dir;
				dir = (Vector2)aim.transform.position - (Vector2)transform.position;
				dir.Normalize ();
				gameObject.GetComponent<Rigidbody2D> ().AddForce (-aim.transform.up * arma2.GetComponent<Arma>().force * 10);
			}
		}
	}
		
	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag.Equals ("Rastro")) {
			timerSpeed = 0;
		}

		if (coll.gameObject.tag.Equals ("Minhocão")) {
			gC.GetComponent<GameController> ().playerHP = 0;
		}

		if (coll.gameObject.tag.Equals ("KillZone")) {
			for (int i = 0; i < 32; i++) {
				SetFallingOrder (coll);
				Caindo ();
			}
		}

		if (coll.tag.Equals ("Item")) {
			gC.GetComponent<GameController> ().hpMax += coll.GetComponent<Item> ().hp;
			speed += coll.GetComponent<Item> ().speed;
			fireRateP += coll.GetComponent<Item> ().fireRate;
			Destroy (coll.gameObject);
		}

		if (coll.tag.Equals ("Room")) {
			Destroy (coll);
		}

		if (coll.tag.Equals ("Final")) {
			speed = 0;
			fade.GetComponent<Fade> ().onFade = true;
			fade.GetComponent<Fade> ().speed = 0.1f;
			if (fade.GetComponent<SpriteRenderer> ().color.a == 1) {
				SceneManager.LoadScene ("end");
			}
		}
	}

	void OnTriggerStay2D (Collider2D coll) {
		if (coll.gameObject.tag.Equals ("KillZone")) {
			SetFallingOrder (coll);
			Caindo ();
		}

		if (coll.tag.Equals ("Arma"))  {
			//timerShot1 = 0;
			//timerShot2 = 0;
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				DropArma1 ();
				nomeArma1 = coll.name;
				dropArma1 = coll.gameObject;
				coll.gameObject.SetActive (false);
			}
			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				DropArma2 ();
				nomeArma2 = coll.name;
				dropArma2 = coll.gameObject;
				coll.gameObject.SetActive (false);
			}
		}
	}

	void OnCollisionStay2D (Collision2D coll) {
		if (coll.gameObject.tag.Equals ("Enemy")) {
			if (timerAtk >= 1) {
				gC.SendMessage ("DamagePlayer");
				flash = 10;
				timerAtk = 0;
			}
		}
	}

	void ReduceSpeed () {
		if (timerSpeed < 2f) {
			timerSpeed += Time.deltaTime;
			speed = normalspeed / 2;
			GetComponent<SpriteRenderer> ().color = new Color (0.56f, 1, 1);
		} else {
			speed = normalspeed;
			GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
		}
	}

	void FlashPlayer () {
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
			if (timerSpeed >= 2) {
				GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
				speed = normalspeed;
			}
		}
	}

	void SetOrder () {
		if (!dead) 
		gameObject.GetComponent<SpriteRenderer> ().sortingOrder = (int)(-transform.position.y)+100;
	}

	void SetFallingOrder (Collider2D coll) {
		dead = true;
		if (transform.position.y > coll.transform.position.y) {
			transform.position = transform.position + new Vector3 (0, -0.01f,0);
			gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 9999;
		} if (transform.position.y <= coll.transform.position.y)  {
			transform.position = transform.position + new Vector3 (0, +0.01f,0);
			gameObject.GetComponent<SpriteRenderer> ().sortingOrder = -	9999;
		}
		if (transform.position.x <= coll.transform.position.x)  {
			transform.position = transform.position + new Vector3 (0.01f,0,0);
	
		}
		if (transform.position.x > coll.transform.position.x)  {
			transform.position = transform.position + new Vector3 (-0.01f,0,0);
		
		}

	}
	public void Caindo () {
			dead = true;
			speed = 0;
			gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			playerAnim.SetBool ("Falling", true);
			playerAnim.Play ("Falling");
	}
}
