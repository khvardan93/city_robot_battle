using UnityEngine;
using UnityEngine.UI;
using RobotBattle.Game;
using UnityEngine.Serialization;

namespace RobotBattle.Robot
{
    public class RobotStatusBarScript : MonoBehaviour
    {
        [FormerlySerializedAs("shieldHealth")] [SerializeField] Text _shieldHealth;
        [FormerlySerializedAs("robotHealth")] [SerializeField] Text _robotHealth;

        private Transform _camera;
        private Transform _thisTransform;
        private PlayerController _playerController;

        void Start()
        {
            _playerController = GetComponentInParent<PlayerController>();
            _thisTransform = transform;
            //camera = CameraController.Instance.transform;
            //GetComponent<Canvas>().worldCamera = CameraController.Instance.getCamera();
        }

        void Update()
        {
            Vector3 targetVector = -(_camera.position - _thisTransform.position);

            _thisTransform.LookAt(targetVector + _thisTransform.position);
            _shieldHealth.text = (int)_playerController.GetShieldHealthInPercents() + "%";
            _robotHealth.text = (int)_playerController.GetHealthInPercents() + "%";

            if (_playerController.GetShieldHealthInPercents() <= 0) _shieldHealth.text = "";
        }
    }
}
