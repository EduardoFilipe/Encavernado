using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	

	void Start () {
		
	}

	public void Inicio () {
		SceneManager.LoadScene ("salas");
	}

	public void Sair () {
		Application.Quit ();
	}
}
