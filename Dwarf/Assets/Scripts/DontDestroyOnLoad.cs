using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

	Transform initialpos;
	void Awake () {

		gameObject.GetComponent<SpriteRenderer> ().color += new Color (0, 0, 0, -1);
		DontDestroyOnLoad(transform.gameObject);
		gameObject.GetComponent<SpriteRenderer> ().sortingOrder = (int)(-transform.position.y)+100;
	}

	public void Enable() {
		gameObject.GetComponent<SpriteRenderer> ().color += new Color (0, 0, 0, 1);
		gameObject.GetComponent<Rigidbody2D> ().simulated = false;
	}

	void OnTriggerStay2D (Collider2D coll) {
		if (coll.gameObject.tag == "KillZone") {
			Destroy (gameObject);
		}
		else if (coll.gameObject.tag == "destroygrave") {
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag == "KillZone") {
			Destroy (gameObject);
		}
		else if (coll.gameObject.tag == "destroygrave") {
			Destroy (gameObject);
		}
	}
	void OnTriggerExit2D (Collider2D coll) {
		if (coll.gameObject.tag == "KillZone") {
			Destroy (gameObject);
		}
		else if (coll.gameObject.tag == "destroygrave") {
			Destroy (gameObject);
		}
	}


}
