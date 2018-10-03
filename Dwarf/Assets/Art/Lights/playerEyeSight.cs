using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerEyeSight : MonoBehaviour {

	public GameObject vignette;
	public bool light;
	void Start () {
		light = false;
	}

	void Update() {
		if (light == false)
		if (vignette.GetComponent<SpriteRenderer> ().color.a > 0.8f)
			vignette.GetComponent<SpriteRenderer> ().color += new Color (0, 0, 0, -0.02f);
	}
	
	// Update is called once per frame
	void OnTriggerStay2D (Collider2D coll) {
		if (coll.tag == "Lights") {
			light = true;
			if (vignette.GetComponent<SpriteRenderer> ().color.a < 0.95f)
				vignette.GetComponent<SpriteRenderer> ().color += new Color (0, 0, 0, 0.02f);
		}
		
	}

	void OnTriggerExit2D (Collider2D coll) {
		if (coll.tag == "Lights") {
			light = false;
		}
	}
}
