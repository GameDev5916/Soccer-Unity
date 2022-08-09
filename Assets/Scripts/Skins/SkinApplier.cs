using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skins
{
    public class SkinApplier : MonoBehaviour
    {
        [SerializeField] private SkinCollection collection;

        private MeshRenderer renderer;

        private void Awake()
        {
            renderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            renderer.material.color =
                collection.Skins[collection.SelectedSkinIndex].SkinColor;
        }
    }
}
