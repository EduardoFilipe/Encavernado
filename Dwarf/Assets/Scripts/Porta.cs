using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour {
	public GameObject collider;
	public bool aberta;

	void Start () {
		aberta = true;
	}

	void Update () {
		if (aberta) {
			collider.GetComponent<BoxCollider2D> ().enabled = false;
		} else {
			collider.GetComponent<BoxCollider2D> ().enabled = true;
		}
	}
}
