using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour {
	public GameObject[] enemys;
	public GameObject[] portas;
	public int enemysCont;
	public float timerAbrir, timerFechar;
	bool aberto, fechado;
	GameObject audioManager;

	// Use this for initialization
	void Start () {
		audioManager = GameObject.FindGameObjectWithTag ("AudioManager");
		enemysCont = 0;
		timerAbrir = 3;
		timerFechar = 3;
		fechado = false;
		portas = GameObject.FindGameObjectsWithTag ("Ground");
		for (int x = 0; x < enemys.Length; x++) {
			enemysCont++;
		}

	}
	
	// Update is called once per frame
	void Update () {
		timerFechar += Time.deltaTime;
		if (enemysCont <= 0 && !aberto) {
			timerAbrir = 0;
		}
		AbrePortas ();
		FechaPortas ();
	}

	public void AbrePortas () {
		if (timerAbrir < 2) {
			timerAbrir += Time.deltaTime;
			aberto = true;
			for (int i = 0; i < portas.Length; i++) {
				portas [i].transform.Translate (Vector2.right * 7f * Time.deltaTime);
			}

			for (int j = 0; j < portas.Length; j++) {
				portas [j].GetComponentInChildren<Porta> ().aberta = true;
			}
		} 
	}

	public void FechaPortas () {
		
		if (timerFechar < 2) {
			fechado = true;
			for (int i = 0; i < portas.Length; i++) {
				portas [i].transform.Translate (Vector2.left * 7f * Time.deltaTime);
			}
		} else {
			if (fechado) {
				for (int j = 0; j < portas.Length; j++) {
					portas [j].GetComponentInChildren<Porta> ().aberta = false;
				}
				fechado = false;
			}
		}
	}

	public void AtivarInimigos () {
		for (int x = 0; x < enemys.Length; x++) {
			enemys [x].SendMessage ("Ativar");
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag.Equals ("Player")) {
			audioManager.GetComponent<AudioManager> ().Play ("pontemovendo");
			timerFechar = 0;
			FechaPortas ();
			AtivarInimigos ();
		}
	}
}
