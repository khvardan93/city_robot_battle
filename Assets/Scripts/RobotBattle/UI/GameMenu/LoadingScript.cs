using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RobotBattle.UI.GameMenu
{
    public class LoadingScript : GameMenuPageBaseScript
    {
        [SerializeField] Image backgroundImage;
        [SerializeField] float slideshowSpeed;

        private void OnEnable()
        {
            //StartCoroutine(slideshow());
        }

        private IEnumerator slideshow()
        {
            var slides = Resources.LoadAll<Sprite>("LoadingImages");
            int index = 0;

            while (gameObject)
            {
                backgroundImage.sprite = slides[index];
                index++;
                if (index == slides.Length) index = 0;

                yield return new WaitForSeconds(slideshowSpeed);
            }
        }
    }
}