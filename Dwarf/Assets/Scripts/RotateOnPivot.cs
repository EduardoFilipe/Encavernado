using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnPivot : MonoBehaviour {

	public int rotation;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward * -rotation, Space.Self);
	}
}
