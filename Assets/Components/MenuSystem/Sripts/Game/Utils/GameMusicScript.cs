using UnityEngine;
using System.Collections;

public class GameMusicScript : MonoBehaviour {

	private AudioSource audioSource;
	private int counter = 1;

	void Update(){
		counter++;

		if (counter % 30 == 0) {
			audioSwitcher ();
			counter = 1;
		}

	}

	private void audioSwitcher(){
		if (audioSource == null)
			audioSource = GetComponent<AudioSource> ();

		if (TimerUtil.paused) {
			audioSource.enabled = true;
		} else {
			audioSource.enabled = false;
		}

	}

}
