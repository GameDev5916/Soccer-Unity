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
    public class GameResultsMenu : SceneController
    {
        #region Fields

        [Header("UI Elements References")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Text message;

        [Header("Messages")]
        [SerializeField] private string winMessage;
        [SerializeField] private string loseMessage;

        private bool won = false;

        #endregion

        protected void Start()
        {
            name = "GameResultsMenu";

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));

            message.text = won ? winMessage : loseMessage;

            if (won)
            {
                GivePlayerGem();
            }

            playButton.onClick.AddListener(() => SceneTransitioner.Instance.OpenScene("Game"));
            mainMenuButton.onClick.AddListener(() => SceneTransitioner.Instance.OpenScene("MainMenu"));
        }

        private void GivePlayerGem()
        {
            int gems = PlayerPrefs.GetInt(MainMenu.PLAYER_PREFS_GEM_KEY, 0);
            gems++;
            PlayerPrefs.SetInt(MainMenu.PLAYER_PREFS_GEM_KEY, gems);
        }

        #region OnLoad/Unload

        public override IEnumerator OnSceneLoaded()
        {
            yield return EnterAnimationCoroutine(1);
        }

        public override IEnumerator OnSceneUnloaded()
        {
            playButton.interactable = false;
            mainMenuButton.interactable = false;

            yield return ExitAnimationCoroutine(1);
        }

        protected IEnumerator EnterAnimationCoroutine(float duration, Action callback = null)
        {
            Fade(0, 1, duration);

            yield return new WaitForSecondsRealtime(duration);

            callback?.Invoke();
        }

        protected IEnumerator ExitAnimationCoroutine(float duration, Action callback = null)
        {
            Fade(1, 0, duration);

            yield return new WaitForSecondsRealtime(duration);

            callback?.Invoke();
        }

        public override void ReceiveArgs(Dictionary<string, object> args)
        {
            string winKey = "won";

            if (args != null && args.ContainsKey(winKey))
            {
                won = (bool)args[winKey];
            }
        }

        #endregion
    }
}

