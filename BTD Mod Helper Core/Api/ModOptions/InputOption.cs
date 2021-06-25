﻿using BTD_Mod_Helper.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace BTD_Mod_Helper.Api.ModOptions
{
    internal class InputOption : SharedOption
    {
        public Button button;
        public Text buttonText;
        public InputField inputField;

        private InputOption(GameObject parentGO, ModSetting modSetting) : base(parentGO, modSetting, "TextInputOption")
        {
            button = instantiatedGameObject.transform.Find("Button").GetComponent<Button>();
            buttonText = instantiatedGameObject.transform.Find("Button/Text").GetComponent<Text>();
            inputField = instantiatedGameObject.transform.Find("InputField").GetComponent<InputField>();

            inputField.SetText(modSetting.GetValue().ToString());

            buttonText.text = "Reset";
            button.AddOnClick(() =>
            {
                inputField.SetText(modSetting.GetDefaultValue().ToString());
            });
        }
        
        public InputOption(GameObject parentGO, ModSettingString modSettingString) : this(parentGO, (ModSetting)modSettingString)
        {
            inputField.characterValidation = modSettingString.GetValidation();
            inputField.AddSubmitEvent(modSettingString.SetValue);
        }
        
        public InputOption(GameObject parentGO, ModSettingInt modSettingInt) : this(parentGO, (ModSetting)modSettingInt)
        {
            inputField.characterValidation = InputField.CharacterValidation.Integer;
            inputField.AddOnValueChangedEvent(value =>
            {
                var i = long.Parse(value);
                if (modSettingInt.maxValue.HasValue && i > modSettingInt.maxValue.Value)
                {
                    i = modSettingInt.maxValue.Value;
                } else if (modSettingInt.minValue.HasValue && i < modSettingInt.minValue.Value)
                {
                    i = modSettingInt.minValue.Value;
                }
                inputField.SetText(i.ToString());
                modSettingInt.SetValue(i);
            });
        }
        
        public InputOption(GameObject parentGO, ModSettingDouble modSettingDouble) : this(parentGO, (ModSetting)modSettingDouble)
        {
            inputField.characterValidation = InputField.CharacterValidation.Decimal;
            inputField.AddOnValueChangedEvent(value =>
            {
                var d = double.Parse(value);
                if (modSettingDouble.maxValue.HasValue && d > modSettingDouble.maxValue.Value)
                {
                    d = (int) modSettingDouble.maxValue.Value;
                } else if (modSettingDouble.minValue.HasValue && d < modSettingDouble.minValue.Value)
                {
                    d = (int) modSettingDouble.minValue.Value;
                }
                inputField.SetText(d.ToString());
                modSettingDouble.SetValue(d);
            });
        }

        internal static RectTransform GetOriginalAsset(GameObject parentGO)
        {
            return GetOriginalAsset(parentGO, "TextInputOption");
        }
    }
}
