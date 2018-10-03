using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEM : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag.Equals ("Rastro") || coll.gameObject.tag.Equals ("Web")) {
			Destroy (coll.gameObject);
		}
	}
}
