using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creditosManager : MonoBehaviour {


		public float letterPause = 0.2f;
	public GameObject fader;

		string message;
		public Text textComp;

		// Use this for initialization
		void Start () {
			message = textComp.text;
			textComp.text = "";
			StartCoroutine(TypeText ());
		}

		IEnumerator TypeText () {
			foreach (char letter in message.ToCharArray()) {
				textComp.text += letter;
				yield return 0;
				yield return new WaitForSeconds (letterPause);
			}
		}

	void Update() {
		if (textComp.text == message) {
			if (textComp.color.a > 0) {
				textComp.color += new Color (0, 0, 0, -0.02f);
			} else {
				if (fader.GetComponent<SpriteRenderer> ().color.a < 1) {
					fader.GetComponent<SpriteRenderer> ().color += new Color (0, 0, 0, 0.02f);
				} else {
					Application.LoadLevel ("tutorial");
				}
			}
		}
	}
		


//	public Text text;
//	string[] temptext;
//	int counter;
//	void Start () {
//		counter = 0;
//		temptext = new string[text.text.Length];
//		for (int i = 0; i < text.text.Length; i++) {
//			temptext[i] = text.text.;
//		}
//
//		text = " ";
//		InvokeRepeating ("WriteText", 0.5f, 0.2f);
//	}
//	
//	// Update is called once per frame
//	void WriteText () {
//		text.text += temptext [counter];
//		counter++;
//	}
}
