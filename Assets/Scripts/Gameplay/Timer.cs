using SceneControllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private Text text;

        private const string defaultTime = "00:00";

        private void Start()
        {
            GameController.Instance.TimeChanged += UpdateTime;
            text.text = defaultTime;
        }

        private void UpdateTime(int time)
        {
            text.text = "0" + (time / 60).ToString() + ":" + (time % 60).ToString();
        }
    }
}
