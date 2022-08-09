using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class ButtonControls : MonoBehaviour
    {
        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;

        public void BindControls(Hoop hoop)
        {
            leftButton.onClick.AddListener(hoop.JumpLeft);
            rightButton.onClick.AddListener(hoop.JumpRight);
        }
    }
}


