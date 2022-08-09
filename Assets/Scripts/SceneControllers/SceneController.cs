using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SceneControllers
{
    public abstract class SceneController : MonoBehaviour
    {
        [HideInInspector] public string sceneName;

        #region OnLoad/Unload

        public virtual IEnumerator OnSceneLoaded()
        {
            yield return null;
        }

        public virtual IEnumerator OnSceneUnloaded()
        {
            yield return null;
        }

        public virtual void ReceiveArgs(Dictionary<string, object> args)
        { }

        #endregion

        #region Animations

        protected void Fade(float startOpacity, float endOpacity, float duration)
        {
            var graphics = gameObject.GetComponentsInChildren<Graphic>();

            foreach (var graphic in graphics)
            {
                var startColor = graphic.color;
                startColor.a = startOpacity;
                graphic.color = startColor;

                var endColor = startColor;
                endColor.a = endOpacity;

                graphic.DOColor(endColor, duration).SetUpdate(UpdateType.Normal, true);
            }
        }

        #endregion
    }
}

