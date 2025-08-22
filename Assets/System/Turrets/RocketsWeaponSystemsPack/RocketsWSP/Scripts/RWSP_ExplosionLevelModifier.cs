using UnityEngine;
using System.Collections;

public class RWSP_ExplosionLevelModifier : MonoBehaviour {

	public ParticleSystem MainExploParticleSystem;
	public ParticleSystem MainSmokeParticleSystem;
	public RocketTypes RocketExplodingType = RocketTypes.RocketType1;
	private float rocketLevelNum = 0;

	// Use this for initialization
	void Start () {

	}

	public void SetRocketType(RocketTypes rocketTypeIn) {
		RocketExplodingType = rocketTypeIn;

		int rocketLevelNumInt = (int)RocketExplodingType + 1;
		rocketLevelNum = (float)rocketLevelNumInt / 2;

		// Set Particle System Variables Based On Level
		if (MainExploParticleSystem != null) {
			ParticleSystem.MinMaxCurve newExploCurve = new ParticleSystem.MinMaxCurve (Random.Range (rocketLevelNum * 5.0f, rocketLevelNum * 7.5f));
			ParticleSystem.MainModule newMainModule = MainExploParticleSystem.main;
			newMainModule.startSize = newExploCurve;
		}
		if (MainSmokeParticleSystem != null) {
			ParticleSystem.MinMaxCurve newSmokeCurve = new ParticleSystem.MinMaxCurve (Random.Range (rocketLevelNum * 5.0f, rocketLevelNum * 7.5f));
			ParticleSystem.MainModule newMainModule = MainSmokeParticleSystem.main;
			newMainModule.startSize = newSmokeCurve;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
