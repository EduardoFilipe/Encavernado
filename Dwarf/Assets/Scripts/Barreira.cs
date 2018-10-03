﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barreira : MonoBehaviour {
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<PlayerBehaviour> ().arma1 != null || player.GetComponent<PlayerBehaviour> ().arma2 != null) {
			Destroy (gameObject);
		}
	}
}
