using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RobotBattle.UI.MainMenu;

namespace RobotBattle.UI.MainMenu
{
    public class LoadingScript : MainMenuPageBaseScript
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