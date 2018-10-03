using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endgameManager : MonoBehaviour {

	public GameObject plain;
	public GameObject vignette;
	public GameObject canvas;
	public GameObject vignette2;
	GameObject audioManager;
	public Text texto1, texto2;
	public Image titulo;
	int state = 1;

	void Update () {
		if (state == 1) {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			//Destroy (player.GetComponent<PlayerBehaviour> ());
			state = 2;
		} else if (state == 2) {
			if (vignette.GetComponent<SpriteRenderer> ().color.a > 0) {
				vignette.GetComponent<SpriteRenderer> ().color -= new Color (0, 0, 0, 0.01f);
			} else {
				if (texto1.color.a < 1) {
					texto1.color += new Color (0, 0, 0, 0.008f);
				} else {
					state = 3;
				}
			}
		}else if (state == 3) {
			Destroy (vignette2);
			if (texto2.color.a < 1) {
				texto2.color += new Color (0, 0, 0, 0.008f);
			} else {
				state = 4;
			}
			
		} else if (state == 4) {
			if (titulo.color.a < 1) {
					titulo.color += new Color (0, 0, 0, 0.005f);
			}
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.tag == "Player") {
			state = 1;
		}
	}
}
