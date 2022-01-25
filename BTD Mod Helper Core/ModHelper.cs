﻿using System.IO;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Unity;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Extensions;
using MelonLoader;
using Exception = System.Exception;

namespace BTD_Mod_Helper
{
    /// <summary>
    /// Catch-all class for non-extension static methods
    /// </summary>
    public static class ModHelper
    {
        #region ModHelperData for the Mod Helper

        internal const string Version = "3.0.0";
        internal const string RepoOwner = "gurrenm3";
        internal const string RepoName = "BTD-Mod-Helper";
        internal const bool ManualDownload = true;
        internal const string Description = "The mod that is allowing you to see this screen right now :P";

        #endregion

        /// <summary>
        /// Directory for where disabled mods are stored
        /// </summary>
        public static string DisabledModsDirectory => Path.Combine(MelonHandler.ModsDirectory, "Disabled");

        /// <summary>
        /// Directory where the Mod Helper stores most of its extra info
        /// </summary>
        public static string ModHelperDirectory =>
            Path.Combine(MelonHandler.ModsDirectory, Assembly.GetExecutingAssembly().GetName().Name);

        internal static string ZipTempDirectory => Path.Combine(ModHelperDirectory, "Zip Temp");
        internal static string OldModsDirectory => Path.Combine(ModHelperDirectory, "Old Mods");

        internal static void LoadAllMods()
        {
            foreach (var mod in MelonHandler.Mods.OfType<BloonsMod>().OrderByDescending(mod => mod.Priority))
            {
                try
                {
                    ModContentInstances.SetInstance(mod.GetType(), mod);
                    ResourceHandler.LoadEmbeddedTextures(mod);
                    ResourceHandler.LoadEmbeddedBundles(mod);
                    ModContent.LoadModContent(mod);
                }
                catch (Exception e)
                {
                    Error("Critical failure when loading resources for mod " + mod.Info.Name);
                    Error(e);
                }
            }

            foreach (var melonMod in MelonHandler.Mods)
            {
                try
                {
                    ModHelperData.Load(melonMod);
                }
                catch (Exception e)
                {
                    Warning(e);
                }
            }
        }

        #region Console Messages

        /// <summary>
        /// Logs a message from the specified Mod's LoggerInstance
        /// </summary>
        public static void Log<T>(object obj) where T : BloonsMod
        {
            ModContent.GetInstance<T>().LoggerInstance.Msg(obj);
        }

        /// <summary>
        /// Logs a message from the specified Mod's LoggerInstance
        /// </summary>
        public static void Msg<T>(object obj) where T : BloonsMod
        {
            ModContent.GetInstance<T>().LoggerInstance.Msg(obj);
        }


        /// <summary>
        /// Logs an error from the specified Mod's LoggerInstance
        /// </summary>
        public static void Error<T>(object obj) where T : BloonsMod
        {
            ModContent.GetInstance<T>().LoggerInstance.Error(obj);
        }

        /// <summary>
        /// Logs a warning from the specified Mod's LoggerInstance
        /// </summary>
        public static void Warning<T>(object obj) where T : BloonsMod
        {
            ModContent.GetInstance<T>().LoggerInstance.Warning(obj);
        }

        /// <summary>
        /// Logs a message from the Mod Helper's LoggerInstance
        /// </summary>
        internal static void Log(object obj)
        {
            Main.LoggerInstance.Msg(obj);
        }

        /// <summary>
        /// Logs a message from the Mod Helper's LoggerInstance
        /// </summary>
        internal static void Msg(object obj)
        {
            Main.LoggerInstance.Msg(obj);
        }

        /// <summary>
        /// Logs an error from the Mod Helper's LoggerInstance
        /// </summary>
        internal static void Error(object obj)
        {
            Main.LoggerInstance.Error(obj);
        }

        /// <summary>
        /// Logs a warning from the Mod Helper's LoggerInstance
        /// </summary>
        internal static void Warning(object obj)
        {
            Main.LoggerInstance.Warning(obj);
        }

        #endregion

        internal static BloonsMod Main => ModContent.GetInstance<MelonMain>();

        private static void PerformHook<T>(System.Action<T> action) where T : BloonsMod
        {
            foreach (var mod in MelonHandler.Mods.OfType<T>().OrderByDescending(mod => mod.Priority))
            {
                if (!mod.CheatMod || !Game.instance.CanGetFlagged())
                {
                    try
                    {
                        action.Invoke(mod);
                    }
                    catch (Exception e)
                    {
                        mod.LoggerInstance.Error(e);
                    }
                }
            }
        }

#if BloonsTD6
        internal static void PerformHook(System.Action<BloonsTD6Mod> action)
        {
            PerformHook<BloonsTD6Mod>(action);
        }
#elif BloonsAT
        internal static void PerformHook(System.Action<BloonsATMod> action)
        {
            PerformHook<BloonsATMod>(action);
        }
#endif
    }
}