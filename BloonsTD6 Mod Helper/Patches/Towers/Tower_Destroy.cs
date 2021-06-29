﻿using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts.Simulation.Towers;
using Harmony;

namespace BTD_Mod_Helper.Patches.Towers
{

    [HarmonyPatch(typeof(Tower), nameof(Tower.OnDestroy))]
    //[HarmonyPatch]
    internal class Tower_Destroy
    {
        /*static IEnumerable<MethodInfo> TargetMethods()
        {
            yield return typeof(Tower).GetMethod(nameof(Tower.Destroy));
        }*/
        
        [HarmonyPostfix]
        internal static void Postfix(Tower __instance)
        {
            MelonMain.DoPatchMethods(mod => mod.OnTowerDestroyed(__instance));
        }
    }




}