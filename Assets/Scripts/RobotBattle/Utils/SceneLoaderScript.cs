using UnityEngine.SceneManagement;

namespace RobotBattle.Utils
{
	public enum Scenes
	{
		Island,
		Garage
	}
	
	public class SceneLoader
	{
		private static void LoadScene(string name)
		{
			NativeController.trackEvent("page_" + name);
			SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
		}
		
		public static void LoadScene(Scenes scene)
		{
			LoadScene(scene.ToString());
		}

		public static void ReloadScene()
		{
			NativeController.trackEvent("page_" + SceneManager.GetActiveScene().name);
			SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
		}
	}
}