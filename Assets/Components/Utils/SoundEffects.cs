using UnityEngine;
using System.Collections;

/// <summary>
/// Creating instance of sounds from code with no effort
/// </summary>
public class SoundEffects : MonoBehaviour
{

	public AudioClip checkPointSound;

	public AudioClip deathSound;
	public AudioClip winSound;
	public AudioClip pauseSound;

	public AudioClip buttonClick;
	public AudioClip buttonClick2;
	public AudioClip buttonClick3;

	public AudioClip buttonClickFail;

	//system properties
	public static SoundEffects instance;
	private AudioSource audioSource;

	void Awake ()
	{
		if (instance != null) {
			Debug.LogError ("Multiple instances of SoundEffects!");
		}
		instance = this;
		audioSource = GetComponent<AudioSource> ();
	}
	
	/// <summary>
	/// Play a given 2D sound
	/// </summary>
	/// <param name="originalClip"></param>
	public void playSound (AudioClip audioClip)
	{
		audioSource.PlayOneShot (audioClip);
	}

	public void playSound (AudioClip audioClip, float volumeScale)
	{
		audioSource.PlayOneShot (audioClip, volumeScale);
	}

}