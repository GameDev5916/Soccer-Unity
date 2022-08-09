using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Gameplay
{
    public class CinemaMachineShake : MonoBehaviour
    {
        private CinemachineVirtualCamera cinemaCamera;
        private float shakeTime;
        private float startingIntensity;
        private float shakeTimeTotal;

        private void Awake()
        {
            cinemaCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void ShakeCamera(float intensity, float time)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                cinemaCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

            startingIntensity = intensity;
            shakeTime = time;
            shakeTimeTotal = time;
        }

        private void Update()
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                cinemaCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                    Mathf.Lerp(startingIntensity, 0f, 1 - shakeTime / shakeTimeTotal);
            }
        }
    }
}


