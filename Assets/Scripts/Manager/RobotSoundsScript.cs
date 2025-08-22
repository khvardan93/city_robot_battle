using UnityEngine;

public class RobotSoundsScript : MonoBehaviour
{
    [SerializeField] AudioClip[] stepSounds;
    [SerializeField] AudioClip[] turnSounds;
    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip miniGunSound;
    [SerializeField] AudioClip machineGunSound;
    [SerializeField] AudioClip rocketSound;
    [SerializeField] AudioClip bigRocketSound;
    [SerializeField] AudioClip laserSound;
    [Space]
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip failSound;

    private AudioSource audioSource;

    private int stepSoundIndex = 0;
    private int turnSoundIndex = 0;

    public static RobotSoundsScript Instance
    {
        get;
        internal set;
    }

    private void Awake()
    {
        AudioListener.volume = GameManagerScript.Instance.isMuted ? 0 : 1;

        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public AudioClip getStepSound()
    {
        stepSoundIndex++;
        if (stepSoundIndex == stepSounds.Length) stepSoundIndex = 0;
        return stepSounds[stepSoundIndex];
    }

    public AudioClip getTurnSound()
    {
        turnSoundIndex++;
        if (turnSoundIndex == turnSounds.Length) turnSoundIndex = 0;
        return turnSounds[turnSoundIndex];
    }

    public AudioClip getFireSound()
    {
        return fireSound;
    }

    public AudioClip getMiniGunSound()
    {
        return miniGunSound;
    }

    public AudioClip getMachineGunSound()
    {
        return machineGunSound;
    }

    public AudioClip getRocketSound()
    {
        return rocketSound;
    }

    public AudioClip getBigRocketSound()
    {
        return bigRocketSound;
    }

    public AudioClip getLaserSound()
    {
        return laserSound;
    }

    public AudioClip GetClickSound()
    {
        return clickSound;
    }

    public AudioClip getFailSound()
    {
        return failSound;
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}