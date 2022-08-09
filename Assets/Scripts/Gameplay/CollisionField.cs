using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    /// <summary>
    ///    Tracks certain collider and invokes callback on it leaving this collision field
    /// </summary>
    public class CollisionField : MonoBehaviour
    {
        private Collider collider;

        private Action onFieldExit;

        public void SetUpCallback(Collider collider, Action action)
        {
            this.collider = collider;
            onFieldExit = action;
        }

        private void OnTriggerExit(Collider other)
        {
            if (onFieldExit != null)
            {
                if (other == collider)
                {
                    onFieldExit();
                    onFieldExit = null;
                    collider = null;
                }

            }
        }
    }
}

