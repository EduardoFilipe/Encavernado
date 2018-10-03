using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour {

	public GameObject player, pitfall, fader; 
	public int state = -1;
	float timer;
	GameObject audioManager;

	void Start () {
		audioManager = GameObject.FindGameObjectWithTag ("AudioManager");
		player.GetComponent<PlayerBehaviour> ().enabled = false;
	}
	


	void Update () {
		timer += Time.deltaTime;
		if (state == -1) {
			if (timer > 2f) {
				audioManager.GetComponent<AudioManager> ().Play ("introdrama");
				state = 0;
			}
		}
		else if (state == 0) {
			if (player.transform.position.y > pitfall.transform.position.y) {
				player.transform.position += new Vector3 (0, -0.1f, 0);
			} else {
				state = 1;
			}
		} else if (state == 1) {
			player.GetComponent<PlayerBehaviour> ().enabled = true;
			player.GetComponent<PlayerBehaviour> ().Caindo ();
			state = 2;
		} else if (state == 2) {
			fader.GetComponent<SpriteRenderer> ().color = new Color (-1, -1, -1, 0);
			state = 3;
		}
		if (timer >4.5f) {
			Application.LoadLevel ("caindo");

		}
	}
}
