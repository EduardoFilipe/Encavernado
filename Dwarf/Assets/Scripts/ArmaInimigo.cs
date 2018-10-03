using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaInimigo : MonoBehaviour {
	public float speed, damage, lifeTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MoveArma () {
		transform.Translate (Vector2.up * speed * Time.deltaTime);
		Destroy (gameObject, lifeTime);
	}
}
