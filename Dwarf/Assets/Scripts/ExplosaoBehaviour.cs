using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosaoBehaviour : MonoBehaviour {
	public float damage, timer, lifeTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		//transform.localScale = new Vector2 (transform.localScale.x + 10, transform.localScale.y + 10);
		Destroy (gameObject, lifeTime);
	}

	void OnTriggerStay2D (Collider2D coll) {
		if (coll.gameObject.tag.Equals ("Enemy") || coll.gameObject.tag.Equals ("Minhocão")) {
			if (timer > 0.1f) {
				coll.gameObject.SendMessage ("Damage", damage, SendMessageOptions.DontRequireReceiver);
				timer = 0;
			}
		}
	}
}
