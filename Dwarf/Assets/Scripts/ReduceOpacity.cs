using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceOpacity : MonoBehaviour {



	void OnTriggerEnter2D (Collider2D coll) {
		Color tmp;
		if (coll.tag != "Ground" && coll.tag != "Enemy") {
			if (coll.GetComponent<SpriteRenderer>() != null && coll.GetComponent<SpriteRenderer>().color.a > 0.5f) {
				coll.GetComponent<SpriteRenderer> ().color = coll.GetComponent<SpriteRenderer> ().color + new Color (0, 0, 0, -0.5f);


			}
		}

	}

	void OnTriggerExit2D (Collider2D coll) {
		if (coll.tag == "Rock") {
			if (coll.GetComponent<SpriteRenderer>() != null && coll.GetComponent<SpriteRenderer>().color.a < 1f) {
				coll.GetComponent<SpriteRenderer> ().color = coll.GetComponent<SpriteRenderer> ().color + new Color (0, 0, 0, 0.5f);


			}
		}

	}
}
