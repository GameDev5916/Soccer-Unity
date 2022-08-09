using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Skins/Skin")]
    public class Skin : ScriptableObject
    {
        public string Name;

        public bool IsFree = false;

        public bool IsAvailable()
        {
            return IsFree || IsBought();
        }

        public bool IsBought()
        {
            int result = PlayerPrefs.GetInt(Name, 0);

            return result > 0;
        }

        public void Buy()
        {
            PlayerPrefs.SetInt(Name, 1);
        }

        public int Price = 5;

        public Color SkinColor;

        //Texture, etc.
    }
}
