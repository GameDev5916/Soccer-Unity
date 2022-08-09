using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        [SerializeField] private AudioSource audioSource;

        #region AudioClips

        [SerializeField] private AudioClip jumpSound;

        #endregion

        private void Awake()
        {
            SetupSingleton();
        }

        private void SetupSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayJumpSound()
        {
            Play(jumpSound);
        }

        private void Play(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}

