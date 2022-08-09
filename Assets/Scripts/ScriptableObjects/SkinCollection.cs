using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Skins/Skin Collection")]
    public class SkinCollection : ScriptableObject
    {
        public int SelectedSkinIndex = 0;

        public List<Skin> Skins;
    }
}


