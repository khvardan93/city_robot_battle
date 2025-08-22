using UnityEngine;

namespace RobotBattle.Robot
{
    public class RobotSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [Space]
        [SerializeField] private AudioClip _rightLegClip;
        [SerializeField] private AudioClip _leftLegClip;
        [Space]
        [SerializeField] private AudioClip _turnRightClip;
        [SerializeField] private AudioClip _turnLeftClip;
        
        //Animation event
        public void OnRightStep()
        {
            _audioSource.PlayOneShot(_rightLegClip);
        }
        
        //Animation event
        public void OnLeftStep()
        {
            _audioSource.PlayOneShot(_leftLegClip);
        }

        //Animation event
        public void OnTurnRight()
        {
            _audioSource.PlayOneShot(_turnRightClip);
        }
        
        //Animation event
        public void OnTurnLeft()
        {
            _audioSource.PlayOneShot(_turnLeftClip);
        }
    }
}
