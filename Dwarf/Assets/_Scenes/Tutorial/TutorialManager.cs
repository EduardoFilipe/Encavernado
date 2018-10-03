using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	public GameObject faderin,faderout, player, text;
	public int state;
	public bool onplayer =false;
	GameObject audioManager;
	float timerz;


	void Start () {
		state = -1;
		audioManager = GameObject.FindGameObjectWithTag ("AudioManager");
		faderin.GetComponent<Fade> ().onFade = false;
		text.GetComponent<Text>().color += new Color (0, 0, 0, -1); 


	}


	void Update () {
		if (state == -1) {
			player.GetComponent<PlayerBehaviour> ().enabled = false;
			if (faderin.GetComponent<SpriteRenderer> ().color.a < 0.1f) {
				state = 0;
			}
		} else if (state == 0) {
			TextFadeIn ();
			if (text.GetComponent<Text> ().color.a > 0.95f) {
				state = 1;
			}
		} else if (state == 1) {
			player.GetComponent<PlayerBehaviour> ().enabled = true;
			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D)) {
				audioManager.GetComponent<AudioManager> ().Play ("tutorialparabens");
				state = 2;
			}
		} else if (state == 2) {
			TextFadeOut ();
			if (text.GetComponent<Text> ().color.a < 0.05f) {
				state = 3;
			}
		} else if (state == 3) {
			text.GetComponent<Text> ().text = "Ande até o mapa e use os botoes do mouse para pega-lo";
			state = 4;
		} else if (state == 4) {
			TextFadeIn ();
			if (text.GetComponent<Text> ().color.a > 0.95f) {
				state = 6;
			}
		} else if (state == 6) {
			if (onplayer == true) {
				if (Input.GetMouseButtonDown (0) || Input.GetMouseButtonDown (1)) {
					audioManager.GetComponent<AudioManager> ().Play ("pegouitem");
					state = 7;
					gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				}
			}
		} else if (state == 7) {
			TextFadeOut ();
			if (text.GetComponent<Text> ().color.a < 0.01f) {
				state = 8;
			}
		} else if (state == 8) {
			TextFadeOut ();
			if (text.GetComponent<Text> ().color.a < 0.05f) {
				state = 9;
			} 
		} else if (state == 9) {
			text.GetComponent<Text> ().text = "Use os botoes do mouse para usar o mapa";
			TextFadeIn ();
			if (text.GetComponent<Text> ().color.a > 0.95f) {
				state = 10;
			}
		} else if (state == 10) {
			if (Input.GetMouseButtonDown (0) || Input.GetMouseButtonDown (1)) {
				audioManager.GetComponent<AudioManager> ().Play ("tutorialparabens");
				state = 11;
				player.GetComponent<PlayerBehaviour> ().enabled = false;
			}
		} else if (state == 11) {
			TextFadeOut ();
			if (text.GetComponent<Text> ().color.a < 0.05f) {
				state = 12;
			} 
		} else if (state == 12) {
			faderout.GetComponent<Fade> ().onFade = true;
			if (faderout.GetComponent<SpriteRenderer> ().color.a > 0.95) {
				state = 13;

			}
		} else if (state == 13) {
			
			timerz += Time.deltaTime;
			if (timerz >1) 
			Application.LoadLevel ("intro");
		}



	}

	void OnTriggerStay2D (Collider2D coll) {
		onplayer = true;


	}

	void OnTriggerExit2D(Collider2D coll) {
		onplayer = false;
	}


	void TextFadeOut () {
		if (text.GetComponent<Text>().color.a > 0) {
			text.GetComponent<Text>().color -= new Color(0,0,0,0.01f);
		}
	}

	void TextFadeIn () {
		if (text.GetComponent<Text>().color.a < 1) {
			text.GetComponent<Text>().color -= new Color(0,0,0,-0.01f);
		}
	}
}
