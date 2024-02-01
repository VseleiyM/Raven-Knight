using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light2DFlicker : MonoBehaviour
{
	[SerializeField]
	private Light2D light2DComponent;

	[SerializeField]
	private float minIntensity = 0.8f;

	[SerializeField]
	private float maxIntensity = 1.2f;

	[SerializeField]
	private float minRadius = 3.0f;

	[SerializeField]
	private float maxOuterRadius = 5.0f;

	[SerializeField]
	private float minFalloffStrength = 0.8f;

	[SerializeField]
	private float maxFalloffStrength = 1.2f;

	[SerializeField]
	private float flickerSpeed = 0.5f;

	[SerializeField]
	private AnimationCurve flickerCurve;

	void Start()
	{
		if (light2DComponent == null)
		{
			light2DComponent = GetComponent<Light2D>();
		}
		StartCoroutine(FlickerRoutine());
	}

	IEnumerator FlickerRoutine()
	{
		while (true)
		{

			float time = Time.time;
			float curveTime = time % flickerSpeed;
			float curveValue = flickerCurve.Evaluate(curveTime / flickerSpeed);

			light2DComponent.intensity = Mathf.Lerp(minIntensity, maxIntensity, curveValue);
			light2DComponent.pointLightOuterRadius = Mathf.Lerp(minRadius, maxOuterRadius, curveValue);
			light2DComponent.falloffIntensity = Mathf.Lerp(minFalloffStrength, maxFalloffStrength, curveValue);

			yield return new WaitForSeconds(flickerSpeed);
		}
	}

}