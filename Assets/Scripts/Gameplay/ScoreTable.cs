using DG.Tweening;
using SceneControllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class ScoreTable : MonoBehaviour
    {
        [SerializeField] private int hoopIndex;

        [SerializeField] private Text text;

        private GameController gameController;

        private void Start()
        {
            gameController = GameController.Instance;

            gameController.ScoreChanged += IterateScore;

            UpdateScore(0);
        }

        private void IterateScore(int index, int score)
        {
            if (index == hoopIndex)
            {
                StartCoroutine(PlayAnimationCoroutine(0.2f));

                UpdateScore(score);
            }
        }

        private IEnumerator PlayAnimationCoroutine(float duration)
        {
            Color initialColor = text.color;

            Color endColor = initialColor;
            endColor.a = 1;

            text.DOColor(endColor, duration / 2);

            yield return new WaitForSeconds(duration / 2);

            text.DOColor(initialColor, duration / 2);
        }

        private void UpdateScore(int score)
        {
            text.text = score.ToString();
        }
    }
}

