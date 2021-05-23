﻿using System;
using System.Collections.Generic;
using Harmony;
using Assets.Scripts.Unity.UI;

namespace BTD_Mod_Helper.Patches
{
    [HarmonyPatch(typeof(FrontendWorld), nameof(FrontendWorld.Start))]
    internal class FrontendWorld_Start
    {
        [HarmonyPostfix]
        internal static void Postfix()
        {
            ResetSessionData();

            MelonMain.DoPatchMethods(mod => mod.OnFrontEndWorld());
        }

        private static void ResetSessionData()
        {
            SessionData.PoppedBloons = new Dictionary<string, int>();
            SessionData.RoundSet = null;
            SessionData.bloonTracker = new Api.BloonTracker();

            Api.SessionData.instance = new Api.SessionData();
        }
    }
}
