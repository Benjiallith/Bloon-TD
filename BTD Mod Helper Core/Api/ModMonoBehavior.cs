﻿using MelonLoader;
using System;
using System.Linq;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace BTD_Mod_Helper.Api
{
    /// <summary>
    /// Used to make custom MonoBehaviors that will work within the game.
    /// <br/>Inherit this class to have custom Monobehaviors automatically loaded by the Mod Helper
    /// when the game starts.
    /// </summary>
    public class ModMonoBehavior : MonoBehaviour
    {
        /// <summary>
        /// Required base constructor needed to load custom MonoBehaviors.
        /// </summary>
        /// <param name="ptr"></param>
        public ModMonoBehavior(IntPtr ptr) : base(ptr) { }

        /// <summary>
        /// Called once when the game starts to check and load all custom MonoBehaviors found in each mod.
        /// </summary>
        internal static void LoadAllModMonoBehaviors()
        {
            foreach (var mod in MelonHandler.Mods)
            {
                var types = mod?.Assembly?.GetTypes()?.Where(type => type.IsSubclassOf(typeof(ModMonoBehavior)));
                if (types != null && !types.Any())
                    continue;

                foreach (var customBehavior in types)
                {
                    ClassInjector.RegisterTypeInIl2Cpp(customBehavior);
                }
            }
        }
    }
}
