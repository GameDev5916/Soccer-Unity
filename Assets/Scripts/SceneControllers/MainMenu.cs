using DG.Tweening;
using SceneTransitions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneControllers
{
    public class MainMenu : SceneController
    {
        #region Fields

        [Header("Animations")]
        [SerializeField] private float animationsDuration;

        [Header("References")]
        [SerializeField] private Button playButton;
        [SerializeField] private Text gemAmountText;

        #endregion

        private static string GEM_TEMPLATE_TEXT = " gems";

        public static string PLAYER_PREFS_GEM_KEY = "gems";

        protected void Start()
        {
            name = "MainMenu";

            StartCoroutine(EnterAnimationCoroutine(animationsDuration));

            playButton.onClick.AddListener(() => SceneTransitioner.Instance.OpenScene("Game", args: new Dictionary<string, object>()
            {
                ["maxScore"] = 3
            }));

            gemAmountText.text = 
                PlayerPrefs.GetInt(PLAYER_PREFS_GEM_KEY, 0).ToString() + GEM_TEMPLATE_TEXT;
        }

        #region OnLoad/Unload

        public override IEnumerator OnSceneLoaded()
        {
            yield return EnterAnimationCoroutine(animationsDuration);
        }

        public override IEnumerator OnSceneUnloaded()
        {
            playButton.interactable = false;

            yield return ExitAnimationCoroutine(animationsDuration);
        }

        protected IEnumerator EnterAnimationCoroutine(float duration, Action callback = null)
        {
            Fade(0, 1, duration);

            yield return new WaitForSeconds(duration);

            callback?.Invoke();
        }

        protected IEnumerator ExitAnimationCoroutine(float duration, Action callback = null)
        {
            Fade(1, 0, duration);

            yield return new WaitForSeconds(duration);

            callback?.Invoke();
        }

        #endregion
    }
}

