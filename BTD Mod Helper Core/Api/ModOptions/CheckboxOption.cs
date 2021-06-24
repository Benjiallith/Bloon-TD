﻿using UnityEngine;
using UnityEngine.UI;
using BTD_Mod_Helper.Extensions;
using System;

namespace BTD_Mod_Helper.Api.ModOptions
{
    internal class CheckboxOption : SharedOption
    {
        public Toggle checkbox;
        public Text checkboxText;

        private readonly ModSettingBool modSettingBool;

        public CheckboxOption(GameObject parentGO, ModSettingBool modSettingBool) : base(parentGO, modSettingBool, "CheckboxOption")
        {
            this.modSettingBool = modSettingBool;
            checkbox = instantiatedGameObject.transform.Find("Checkbox1").GetComponent<Toggle>();
            checkboxText = instantiatedGameObject.transform.Find("Checkbox1/Label").GetComponent<Text>();

            checkbox.Set(modSettingBool);
            checkbox.AddOnValueChanged(value => modSettingBool.SetValue(value));
        }

        internal override ModSetting GetModSetting()
        {
            return modSettingBool;
        }

        internal static RectTransform GetOriginalAsset(GameObject parentGO)
        {
            return GetOriginalAsset(parentGO, "CheckboxOption");
        }
    }
}
