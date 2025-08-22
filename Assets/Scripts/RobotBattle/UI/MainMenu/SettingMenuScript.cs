using UnityEngine;
using UnityEngine.UI;

namespace RobotBattle.UI.MainMenu
{
    public class SettingMenuScript : MainMenuPageBaseScript
    {
        [SerializeField] Image muteImage;
        [SerializeField] Sprite soundSprite;
        [SerializeField] Sprite noSoundSprite;
        [Space] [SerializeField] Image musicImage;
        [SerializeField] Sprite musicSprite;
        [SerializeField] Sprite noMusicSprite;
        [Space] [SerializeField] Slider sensitivitySlider;
        [SerializeField] InputField nameInput;

        public override void Show()
        {
            base.Show();

            muteImage.sprite = GameManagerScript.Instance.isMuted ? noSoundSprite : soundSprite;
            musicImage.sprite = GameManagerScript.Instance.isNoMusic ? noMusicSprite : musicSprite;

            sensitivitySlider.value = GameManagerScript.Instance.sensitivity;
            nameInput.text = GameManagerScript.Instance.playerName;
        }

        public void onMute()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            GameManagerScript.Instance.isMuted = !GameManagerScript.Instance.isMuted;
            muteImage.sprite = GameManagerScript.Instance.isMuted ? noSoundSprite : soundSprite;
            AudioListener.volume = GameManagerScript.Instance.isMuted ? 0 : 1;
        }

        public void onMusic()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            GameManagerScript.Instance.isNoMusic = !GameManagerScript.Instance.isNoMusic;

            if (!GameManagerScript.Instance.isNoMusic)
            {
                MainMenuScript.Instance.SetMusic(true);
                musicImage.sprite = musicSprite;
            }
            else
            {
                MainMenuScript.Instance.SetMusic(false);
                musicImage.sprite = noMusicSprite;
            }
        }

        public void onSensitivity()
        {
            GameManagerScript.Instance.sensitivity = sensitivitySlider.value;
        }

        public void onChangeName()
        {
            if (nameInput.text.Replace(" ", "") != "")
            {
                GameManagerScript.Instance.playerName = nameInput.text;
            }
            else
            {
                nameInput.text = GameManagerScript.Instance.playerName;
            }
        }

        public void onBack()
        {
            RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
            ChangePage<HomeMenuScript>();
        }
    }
}