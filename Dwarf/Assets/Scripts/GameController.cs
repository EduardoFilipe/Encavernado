using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public int playerHP, hpMax;
	GameObject player;

	GameObject[] lights;
	GameObject[] rocks;
	GameObject[] graves;
	public GameObject[] armas;
	public float timer = 0;
	[HideInInspector]
	public GameObject audioManager;

	public Image hpHUD, cool_1, cool_2, arma1, arma2;

	// Use this for initialization
	void Start () {
		audioManager = GameObject.FindGameObjectWithTag ("AudioManager");
		audioManager.GetComponent<AudioManager> ().StopAll();
		hpMax = 5;
		playerHP = hpMax;
		player = GameObject.FindGameObjectWithTag ("Player");
		lights = GameObject.FindGameObjectsWithTag ("Lights");
		graves = GameObject.FindGameObjectsWithTag ("Grave");
		rocks = GameObject.FindGameObjectsWithTag ("Rock");
		LightOrder ();
		RockOrder ();
		audioManager.GetComponent<AudioManager> ().Play ("trilhaSonora");
		EnableGraves ();

	}

	void EnableGraves () {
		for (int i = 0; i <graves.Length; i++ )
			graves[i].GetComponent<DontDestroyOnLoad>().Enable();
	}

	void LightOrder() {
		foreach (GameObject light in lights) {
			light.GetComponent<SpriteRenderer> ().sortingOrder = (int)(-light.transform.position.y)+100;
		}
	}
	void RockOrder() {
		foreach (GameObject rock in rocks) {
			rock.GetComponent<SpriteRenderer> ().sortingOrder = (int)(-rock.transform.position.y)+100;
		}
	}

	void Update () {
		hpHUD.rectTransform.sizeDelta = new Vector2 (41, (float)playerHP / (float)hpMax * 44);
		Cooldown ();
	}

	void DamagePlayer () {
		playerHP -= 1;
	}

	void Cooldown () {
		if (player.GetComponent<PlayerBehaviour> ().arma1 != null) {
			if (cool_1.rectTransform.sizeDelta.y < 0) {
				cool_1.rectTransform.sizeDelta = new Vector2 (41, 0);
			} else {
				cool_1.rectTransform.sizeDelta = new Vector2 (41, 41 - player.GetComponent<PlayerBehaviour> ().timerShot1 / player.GetComponent<PlayerBehaviour> ().fire1 * 41);
			}
		}
		if (player.GetComponent<PlayerBehaviour> ().arma2 != null) {
			if (cool_2.rectTransform.sizeDelta.y < 0) {
				cool_2.rectTransform.sizeDelta = new Vector2 (41, 0);
			} else {
				cool_2.rectTransform.sizeDelta = new Vector2 (41, 41 - player.GetComponent<PlayerBehaviour> ().timerShot2 / player.GetComponent<PlayerBehaviour> ().fire2 * 41);
			}
		}
	}


}
