using UnityEngine;
using System.Collections;

public enum TextureColorSchemes {
	Normal,
	Hyper,
	LevelColors
}

[RequireComponent (typeof (RWSP_RocketTurret))]
public class RWSP_RocketTurretAssetMng : MonoBehaviour {

	// Sets Materials Based on Current RocketType in RocketLauncher
	private RWSP_RocketTurret rocketTurret;
	private RocketTypes CurrentRocketLevel = RocketTypes.RocketType1;
	public TextureColorSchemes CurrentColorScheme = TextureColorSchemes.LevelColors;
	public Material[] RocketLevelMats;
	public Material RocketHyperMaterial;
	public Material RocketNormalMaterial;

	public GameObject[] RocketPrefabsLevelTypes;
	public GameObject[] RocketPrefabsNormalTypes;
	public GameObject[] RocketPrefabsHyperTypes;

	private Renderer RocketLauncherRenderer;
	private Renderer[] RocketLauncherSubRenderers;
	
	public Material[] ExcludeMaterials;
	
	// Use this for initialization
	void Start () {
		rocketTurret = gameObject.GetComponent<RWSP_RocketTurret>();
		
		RocketLauncherRenderer = gameObject.GetComponent<Renderer>();
		RocketLauncherSubRenderers = gameObject.GetComponentsInChildren<Renderer>();
		
		// Set Initial Rocket Launcher Material
		UpdateRocketLauncherMaterial();
	}
	
	private void UpdateRocketLauncherMaterial() {
		if (CurrentRocketLevel != rocketTurret.CurrentRocketType) {
			CurrentRocketLevel = rocketTurret.CurrentRocketType;	
		}
		
		// Set Level Material Color
		if (CurrentColorScheme != rocketTurret.CurrentColorScheme) {
			CurrentColorScheme = rocketTurret.CurrentColorScheme;
		}
		
		if (CurrentColorScheme == TextureColorSchemes.LevelColors) {
			// Assign Rocket Color Scheme Rockets to Rocket Launcher
			rocketTurret.RocketTypesPrefabs = RocketPrefabsLevelTypes;
			rocketTurret.ForceReloadRockets();

			if (RocketLauncherRenderer != null) {
				RocketLauncherRenderer.material = RocketLevelMats[(int)CurrentRocketLevel];
			}
			if (RocketLauncherSubRenderers.Length > 0) {
				for (int i = 0; i < RocketLauncherSubRenderers.Length; i++) {
					bool needsChange = true;
					if (ExcludeMaterials.Length > 0) {
						for (int m = 0; m < ExcludeMaterials.Length; m++) {
							if (RocketLauncherSubRenderers[i].sharedMaterial == ExcludeMaterials[m])
								needsChange = false;
						}
					}
					// Needs Change
					if (needsChange)
						RocketLauncherSubRenderers[i].material = RocketLevelMats[(int)CurrentRocketLevel];
				}
			}
		}
		else if (CurrentColorScheme == TextureColorSchemes.Normal) {
			// Assign Rocket Color Scheme Rockets to Rocket Launcher
			rocketTurret.RocketTypesPrefabs = RocketPrefabsNormalTypes;
			rocketTurret.ForceReloadRockets();

			if (RocketLauncherRenderer != null) {
				RocketLauncherRenderer.material = RocketNormalMaterial;
			}
			if (RocketLauncherSubRenderers.Length > 0) {
				for (int i = 0; i < RocketLauncherSubRenderers.Length; i++) {
					bool needsChange = true;
					if (ExcludeMaterials.Length > 0) {
						for (int m = 0; m < ExcludeMaterials.Length; m++) {
							if (RocketLauncherSubRenderers[i].sharedMaterial == ExcludeMaterials[m])
								needsChange = false;
						}
					}
					// Needs Change
					if (needsChange)
						RocketLauncherSubRenderers[i].material = RocketNormalMaterial;
				}
			}
		}
		else if (CurrentColorScheme == TextureColorSchemes.Hyper) {
			// Assign Rocket Color Scheme Rockets to Rocket Launcher
			rocketTurret.RocketTypesPrefabs = RocketPrefabsHyperTypes;
			rocketTurret.ForceReloadRockets();

			if (RocketLauncherRenderer != null) {
				RocketLauncherRenderer.material = RocketHyperMaterial;
			}
			if (RocketLauncherSubRenderers.Length > 0) {
				for (int i = 0; i < RocketLauncherSubRenderers.Length; i++) {
					bool needsChange = true;
					if (ExcludeMaterials.Length > 0) {
						for (int m = 0; m < ExcludeMaterials.Length; m++) {
							if (RocketLauncherSubRenderers[i].sharedMaterial == ExcludeMaterials[m])
								needsChange = false;
						}
					}
					// Needs Change
					if (needsChange)
						RocketLauncherSubRenderers[i].material = RocketHyperMaterial;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (rocketTurret.CurrentRocketType != CurrentRocketLevel) {
			UpdateRocketLauncherMaterial();
		}
		if (rocketTurret.CurrentColorScheme != CurrentColorScheme) {
			UpdateRocketLauncherMaterial();
		}
	}
}
