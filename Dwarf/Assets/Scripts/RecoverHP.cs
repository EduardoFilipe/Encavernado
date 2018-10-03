using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverHP : MonoBehaviour {
	GameObject player, gC;
	float disttoplayer;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		gC = GameObject.FindGameObjectWithTag ("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		disttoplayer = Vector2.Distance (transform.position, player.transform.position);

		if (disttoplayer < 5) {
			if (gC.GetComponent<GameController> ().playerHP < gC.GetComponent<GameController> ().hpMax) {
				transform.position = Vector2.MoveTowards (transform.position, player.transform.position, 20 * Time.deltaTime);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.tag.Equals ("Player")) {
			if (gC.GetComponent<GameController> ().playerHP < gC.GetComponent<GameController> ().hpMax) {
				gC.GetComponent<GameController> ().playerHP += 1;
				gC.GetComponent<GameController> ().audioManager.GetComponent<AudioManager> ().Play ("pegouitem");
			}
			Destroy (gameObject);
		}
	}
}
