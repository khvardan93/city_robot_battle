using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityManager
{

	public enum QualityMode
	{
		NONE,
		LOW,
		NORMAL,
		HIGH,
		SUPER
	};

	private static QualityMode defaultMode = QualityMode.HIGH;

	private static QualityMode _currentMode = QualityMode.NONE;

	public static QualityMode currentMode {
		get {
			string currentModeString = PlayerPrefs.GetString("QualityManager_qualitymode", defaultMode.ToString());
			return (QualityMode)System.Enum.Parse(typeof(QualityMode), currentModeString);
		}
		internal set {
			_currentMode = value;
			PlayerPrefs.SetString("QualityManager_qualitymode", _currentMode.ToString());
		}
	}

	public static void setQualitySettings()
	{
		NativeController.trackEvent("quality " + currentMode.ToString());
		setQualitySettings(currentMode);
	}

	public static void setQualitySettings(QualityMode mode)
	{
		if (mode == _currentMode) {
			return;
		}

		if (mode == QualityMode.LOW) {
			QualitySettings.antiAliasing = 0;
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
			QualitySettings.shadows = ShadowQuality.Disable;
		} else if (mode == QualityMode.NORMAL) {
			QualitySettings.antiAliasing = 0;
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
			QualitySettings.shadows = ShadowQuality.Disable;
		} else if (mode == QualityMode.HIGH) {
			QualitySettings.antiAliasing = 0;
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
			QualitySettings.shadows = ShadowQuality.Disable;
		} else if (mode == QualityMode.SUPER) {
			QualitySettings.antiAliasing = 0;
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
			QualitySettings.shadows = ShadowQuality.HardOnly;
		}

		currentMode = mode;
	}

}
