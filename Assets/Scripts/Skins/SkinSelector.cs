using SceneControllers;
using ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Skins
{
    public class SkinSelector : MonoBehaviour
    {
        [SerializeField] private SkinCollection collection;

        [SerializeField] private Button leftArrowButton;
        [SerializeField] private Button rightArrowButton;

        [SerializeField] private Image previewImage;
        [SerializeField] private Button buyButton;
        [SerializeField] private Text buyText;

        private int shownIndex;

        private void Awake()
        {
            shownIndex = collection.SelectedSkinIndex;

            BindButtons();

            ShowSkin();
        }

        private void BindButtons()
        {
            leftArrowButton.onClick.AddListener(SelectPreviousSkin);
            rightArrowButton.onClick.AddListener(SelectNextSkin);
            buyButton.onClick.AddListener(BuySkin);
        }

        private void SelectNextSkin()
        {
            if (shownIndex + 1 > collection.Skins.Count)
            {
                return;
            }

            shownIndex++;

            if (collection.Skins[shownIndex].IsAvailable())
            {
                collection.SelectedSkinIndex = shownIndex;
            }

            ShowSkin();
        }

        private void SelectPreviousSkin()
        {
            if (shownIndex - 1 < 0)
            {
                return;
            }

            shownIndex--;

            if (collection.Skins[shownIndex].IsAvailable())
            {
                collection.SelectedSkinIndex = shownIndex;
            }

            ShowSkin();
        }

        private void BuySkin()
        {
            int price = collection.Skins[shownIndex].Price;

            int gems = PlayerPrefs.GetInt(MainMenu.PLAYER_PREFS_GEM_KEY, 0);

            if (gems >= price)
            {
                gems -= price;
                PlayerPrefs.SetInt(MainMenu.PLAYER_PREFS_GEM_KEY, gems);

                collection.Skins[shownIndex].Buy();
                collection.SelectedSkinIndex = shownIndex;

                ShowSkin();
            }
        }

        private void ShowSkin()
        {
            rightArrowButton.interactable = !(shownIndex == collection.Skins.Count - 1);
            leftArrowButton.interactable = !(shownIndex == 0);

            ShowSkin(collection.Skins[shownIndex]);
        }

        private void ShowSkin(Skin skin)
        {
            bool isAvailable = skin.IsAvailable(); 

            buyButton.enabled = !isAvailable;
            buyButton.image.enabled = !isAvailable;
            buyText.enabled = !isAvailable;

            buyText.text = "buy for " + skin.Price.ToString() + " gems";

            previewImage.color = skin.SkinColor;
        }
    }
}
