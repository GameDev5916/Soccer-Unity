using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PlayerKeyboardControls : MonoBehaviour
    {
        private Hoop donut;

        private void Awake()
        {
            donut = GetComponent<Hoop>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                donut.JumpRight();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                donut.JumpLeft();
            }
        }
    }
}
