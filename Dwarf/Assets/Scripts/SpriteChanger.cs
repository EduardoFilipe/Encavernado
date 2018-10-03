using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour {
	public Sprite newSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MudarSprite () {
		gameObject.GetComponent<SpriteRenderer> ().sprite = newSprite;
	}
}
