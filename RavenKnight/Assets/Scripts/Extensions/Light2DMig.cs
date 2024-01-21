using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class Light2DMig : MonoBehaviour
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
	private float minFalloffStrength = 0.8f; // Минимальное значение falloffStrength

	[SerializeField]
	private float maxFalloffStrength = 1.2f; // Максимальное значение falloffStrength

	[SerializeField]
	private float flickerSpeed = 0.5f;

	public Light2D Light2DComponent
	{
		get { return light2DComponent; }
		set { light2DComponent = value; }
	}

	// Остальные геттеры и сеттеры остаются без изменений

	public float MinFalloffStrength
	{
		get { return minFalloffStrength; }
		set { minFalloffStrength = value; }
	}

	public float MaxFalloffStrength
	{
		get { return maxFalloffStrength; }
		set { maxFalloffStrength = value; }
	}

	void Start()
	{
		Light2DComponent = GetComponent<Light2D>();
		InvokeRepeating("Flicker", 0f, flickerSpeed);
	}

	void Flicker()
	{
		float newIntensity = Random.Range(minIntensity, maxIntensity);
		float newOuterRadius = Random.Range(minRadius, maxOuterRadius);
		float newFalloffStrength = Random.Range(minFalloffStrength, maxFalloffStrength);

		Light2DComponent.intensity = newIntensity;
		Light2DComponent.pointLightOuterRadius = newOuterRadius;
		Light2DComponent.falloffIntensity = newFalloffStrength;
	}

}
