using UnityEngine;
using System.Collections;

public class Shaker : MonoBehaviour
{
	[SerializeField] private float shakeDuration = 0.5f;

	[SerializeField] private float shakeAmount = 0.7f;
	[SerializeField] private float decreaseFactor = 1.0f;

	private Vector3 initialPos;
	private float currentShakeDuration = 0;

	public void Shake()
    {
		initialPos = transform.position;
		currentShakeDuration = shakeDuration;
    }

	void Update()
	{
		if (currentShakeDuration > 0)
		{
			transform.localPosition = initialPos + Random.insideUnitSphere * shakeAmount;

			currentShakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			currentShakeDuration = 0f;
			transform.localPosition = initialPos;
		}
	}
}