﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BTD_Mod_Helper.Extensions;
using BTD_Mod_Helper.Patches;
using MelonLoader;

namespace BTD_Mod_Helper.Api.ModOptions
{
    internal class ShowModOptions_Button
    {
        public ModOptionsMenu modOptionsMenu;
        public static GameObject settingsUI_Canvas;
        public Button instantiatedButton;
        public Button optionsButton;

        public ShowModOptions_Button()
        {
            
        }

        public void Init()
        {
            var scene = SceneManager.GetSceneByName("SettingsUI");
            var rootGameObjects = scene.GetRootGameObjects();
            settingsUI_Canvas = rootGameObjects[0];

            optionsButton = ModOptionsMenu.CanvasGO.transform.Find("ModOptionsButton/Button").GetComponent<Button>();

            instantiatedButton = GameObject.Instantiate(optionsButton, settingsUI_Canvas.transform);
            instantiatedButton.onClick.AddListener(OptionButtonClicked);

            var screenSizePanel = settingsUI_Canvas.GetComponentInChildrenByName<RectTransform>("ScreenSizePanel");
            var updateButton = settingsUI_Canvas.GetComponentInChildrenByName<RectTransform>("UpdateButton");

            instantiatedButton.transform.position = new Vector3(screenSizePanel.position.x + 25, updateButton.position.y + 25);
            instantiatedButton.transform.localScale = new Vector3(3, 3);
        }

        public void OptionButtonClicked()
        {
            modOptionsMenu = new ModOptionsMenu();
            instantiatedButton.gameObject.SetActive(false);
            GameObject.Destroy(instantiatedButton.gameObject);
        }
    }
}
