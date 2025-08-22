using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace RobotBattle.Intro
{
	public class IntroLoader : MonoBehaviour
	{
		[SerializeField] private Image _companyLogo;
		[SerializeField] private Image _gameLogo;
		[Space] 
		[SerializeField] private string _sceneToLoad;
		[SerializeField] private Image _backgroundImage;
		[SerializeField] private float _slideshowSpeed;

		private void Awake()
		{
			StartCoroutine(Slideshow());
			StartCoroutine(LoadSceneAsync(_sceneToLoad));
		}

		private void OnDestroy()
		{
			StopAllCoroutines();
		}

		private IEnumerator LoadSceneAsync(string scene)
		{
			_companyLogo.color = Color.white;
			_gameLogo.color = new Color(1, 1, 1, 0);

			yield return new WaitForSeconds(2);

			_companyLogo.DOColor(new Color(1f, 1f, 1f, 0f), 0.5f);
			_gameLogo.DOColor(Color.white, 0.5f);

			yield return new WaitForSeconds(1f);
			SceneManager.LoadSceneAsync(scene);
		}

		private IEnumerator Slideshow()
		{
			var slides = Resources.LoadAll<Sprite>("IntroImages");
			int index = 0;

			while (gameObject)
			{
				_backgroundImage.sprite = slides[index];
				index++;
				if (index == slides.Length) index = 0;

				yield return new WaitForSeconds(_slideshowSpeed);
			}
		}
	}
}