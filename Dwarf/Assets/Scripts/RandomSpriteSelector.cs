using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteSelector : MonoBehaviour {

	public GameObject[] possibleSprites;
	public bool enemy;

	void Awake () {
		int rand = Random.Range (0, possibleSprites.Length);
		int mirrored = Random.Range (-1, 1);
		GameObject instantiated;
		instantiated = Instantiate (possibleSprites [rand], transform.position, Quaternion.identity);
		if (mirrored == 1) {
			instantiated.GetComponent<SpriteRenderer> ().flipX = true;
		} else {
			instantiated.GetComponent<SpriteRenderer> ().flipX = false;
		}
		//instantiated.transform.parent = gameObject.transform;
		instantiated.GetComponent<SpriteRenderer>().sortingOrder = (int)(-transform.position.y)+100;
		Destroy (gameObject);


	
	}
	

}
