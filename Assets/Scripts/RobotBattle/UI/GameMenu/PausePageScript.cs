using UnityEngine;
using UnityEngine.UI;

namespace RobotBattle.UI.GameMenu
{
    public class PausePageScript : GameMenuPageBaseScript
    {
        [SerializeField] Image muteImage;
        [SerializeField] Sprite soundSprite;
        [SerializeField] Sprite noSoundSprite;
        [Space] [SerializeField] Slider sensitivitySlider;

        public void open()
        {
            TimerUtil.pause();
            gameObject.SetActive(true);
            muteImage.sprite = GameManagerScript.Instance.isMuted ? noSoundSprite : soundSprite;
            sensitivitySlider.value = GameManagerScript.Instance.sensitivity;
        }

        public void close()
        {
            TimerUtil.release();
            gameObject.SetActive(false);
        }

        public void onMute()
        {
            GameManagerScript.Instance.isMuted = !GameManagerScript.Instance.isMuted;
            muteImage.sprite = GameManagerScript.Instance.isMuted ? noSoundSprite : soundSprite;
            AudioListener.volume = GameManagerScript.Instance.isMuted ? 0 : 1;
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
        }

        public void onResume()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            close();
        }

        public void onBackToGarage()
        {
            close();
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            GameMenuScript.Instance.goToGarage();
        }

        public void onSensitivity()
        {
            GameManagerScript.Instance.sensitivity = sensitivitySlider.value;
        }
    }
}