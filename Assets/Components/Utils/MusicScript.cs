using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour {

	private static AudioSource audioSource;

	void Start () {
		if (audioSource == null) {
			DontDestroyOnLoad(gameObject);
			audioSource = GetComponent<AudioSource>();
		} else {
			Destroy(gameObject);
		}
	}

	public static void pause(){
		if (audioSource) {
			audioSource.Pause();
		}
	}

	public static void play(){
		if (audioSource) {
			audioSource.Play();
		}
	}
}
