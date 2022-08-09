using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class MainSphere : MonoBehaviour
    {
        public event Action<Hoop> PlayerScored;

        [SerializeField] private CollisionField collisionField;

        [SerializeField] private Collider triggerCollider;

        private List<GameObject> ignoredColliders;

        public void StopColliding()
        {
            triggerCollider.enabled = false;
        }

        private void Awake()
        {
            ignoredColliders = new List<GameObject>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Hoop>() != null)
            {
                if (!ignoredColliders.Contains(other.gameObject) && other.isTrigger)
                {
#if UNITY_EDITOR
                    Debug.Log("Someone scored!");
#endif

                    PlayerScored?.Invoke(other.GetComponent<Hoop>());
                    DisablePlayerCollider(other);
                }
            }
        }

        private void DisablePlayerCollider(Collider other)
        {
            ignoredColliders.Add(other.gameObject);
            collisionField.SetUpCallback(other, () => ignoredColliders.Remove(other.gameObject));
        }
    }

}
