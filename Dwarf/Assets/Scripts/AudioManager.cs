using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	[System.Serializable]
	public class Sound {

		public string name;

		public AudioClip clip;

		[Range(0f, 1f)]
		public float volume;
		[Range(.1f, 3f)] 
		public float pitch;
		public bool loop;

		[HideInInspector]
		public AudioSource source;
	}


	public Sound[] sounds;

	public static AudioManager instance;


	void Awake () {

		if (instance == null)
			instance = this;
		else {
			Destroy (gameObject);
			return;

		}

		DontDestroyOnLoad (gameObject);

		foreach (Sound s in sounds) {

			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}
		

	public void Play (string name) {
		Sound s = Array.Find (sounds, sound => sound.name == name);
		s.source.PlayOneShot(s.clip);
		//s.source.Play();
	}

	public void StopAll () {
		AudioSource[] audiosources;

		audiosources = gameObject.GetComponents<AudioSource>();
		foreach (AudioSource a in audiosources) {
			a.Stop();
		}
	}




		
}
