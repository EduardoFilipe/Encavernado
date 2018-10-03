using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingSceneManager : MonoBehaviour {

	int state;
	public GameObject dwarf,overlay;
	float timer;
	GameObject audioManager;
	void Start () {
		audioManager = GameObject.FindGameObjectWithTag ("AudioManager");
		state = 0;
		timer = 0;
	}
	

	void Update () {
		if (state == 0) {
			if (dwarf.transform.position.y > 0) {
				dwarf.transform.position += new Vector3 (0, -0.15f, 0);
			} else {

				state = 1;
			}
		} else if (state == 1) {
			transform.position = dwarf.transform.position + new Vector3 (0, 0, -10);
			if (dwarf.transform.position.y > -20) {
				dwarf.transform.position += new Vector3 (0, -0.15f, 0);
			} else {
				audioManager.GetComponent<AudioManager> ().Play ("introdrama2");
				state = 11;
			}
		} else if (state == 11) {
			transform.position = dwarf.transform.position + new Vector3 (0, 0, -10);
			if (dwarf.transform.position.y > -42) {
				dwarf.transform.position += new Vector3 (0, -0.15f, 0);
			} else {

				state = 10;
			}
		}
		 else if (state == 10) {
			transform.position = dwarf.transform.position + new Vector3 (0, 0, -10);
			if (dwarf.transform.position.y > -58) {
				dwarf.transform.position += new Vector3 (0, -0.2f, 0);
			} else {
				audioManager.GetComponent<AudioManager> ().Play ("introdrama2");
				state = 2;
			}
		}else if (state == 2) {
			if (dwarf.transform.position.y > -90) {

				dwarf.transform.position += new Vector3 (0, -0.2f, 0);
			} else {
				audioManager.GetComponent<AudioManager> ().Play ("introcorpoimpacto");
				audioManager.GetComponent<AudioManager> ().Play ("intropedraimpacto");
				audioManager.GetComponent<AudioManager> ().Play ("intropedraimpacto2");
				state = 3;
			}
		}
			else if (state == 3) {
				overlay.GetComponent<SpriteRenderer> ().color += new Color (0, 0,0, 1);
				timer += Time.deltaTime;
				if (timer > 1.5f) {
					state = 4;
				}
			}
			else if (state == 4) {
			Application.LoadLevel ("salas");

			}
		}
	}

