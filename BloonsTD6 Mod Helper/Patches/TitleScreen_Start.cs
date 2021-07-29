﻿using BTD_Mod_Helper.Api;
using HarmonyLib;
using System.Linq;
using Assets.Main.Scenes;
using MelonLoader;

namespace BTD_Mod_Helper.Patches
{
    [HarmonyPatch(typeof(TitleScreen), nameof(TitleScreen.Start))]
    internal class TitleScreen_Start
    {
        [HarmonyPostfix]
        [HarmonyPriority(Priority.High)]
        internal static void Postfix()
        {
            foreach (var mod in MelonHandler.Mods.OfType<BloonsMod>())
            {
                ModContent.LoadAllModContent(mod);
            }
            
            MelonMain.DoPatchMethods(mod => mod.OnTitleScreen());
        }
    }
}