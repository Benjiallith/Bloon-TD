﻿using Assets.Scripts.Unity.UI_New.Odyssey;
using BTD_Mod_Helper.Api;
using Harmony;

namespace BTD_Mod_Helper.Patches
{
    [HarmonyPatch(typeof(OdysseyEventScreen), nameof(OdysseyEventScreen.Update))]
    internal class OdysseyEventScreen_Update
    {
        [HarmonyPostfix]
        internal static void Postfix()
        {
            // Setting only if false because this method is called constantly when screen is open
            if (!SessionData.IsInOdyssey)
            {
                SessionData.IsInOdyssey = true;
            }
        }
    }
}
