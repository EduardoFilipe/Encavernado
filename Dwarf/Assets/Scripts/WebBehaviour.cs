using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBehaviour : ArmaInimigo {

	// Use this for initialization
	void Start () {
		damage = 0;
	}
	
	// Update is called once per frame
	void Update () {
		MoveArma ();
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag.Equals ("Player")) {
			coll.gameObject.GetComponent<PlayerBehaviour> ().timerSpeed = 0;
			Destroy (gameObject);
		}
	}
}
