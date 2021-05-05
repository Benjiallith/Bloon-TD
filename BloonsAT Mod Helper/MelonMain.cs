﻿using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.InGame;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.InGame_Mod_Options;
using MelonLoader;
using System;
using UnityEngine;
using BTD_Mod_Helper.Api.Updater;
using BTD_Mod_Helper.Extensions;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace BTD_Mod_Helper
{
    internal class MelonMain : BloonsATMod
    {
        public override string GithubReleaseURL => "https://api.github.com/repos/gurrenm3/BTD-Mod-Helper/releases";
        public override string LatestURL => "https://github.com/gurrenm3/BTD-Mod-Helper/releases/latest";
        internal readonly List<UpdateInfo> modsNeedingUpdates = new List<UpdateInfo>();

        public const string currentVersion = ModHelperData.currentVersion;

        private bool useModOptionsDEBUG = false;
        private ModOptionsMenu modOptionsUI;


        public override void OnApplicationStart()
        {
            MelonLogger.Msg("Mod has finished loading");
            MelonLogger.Msg("Checking for updates...");

            var updateDir = this.GetModDirectory() + "\\UpdateInfo";
            Directory.CreateDirectory(updateDir);

            UpdateHandler.SaveModUpdateInfo(updateDir);
            var allUpdateInfo = UpdateHandler.LoadAllUpdateInfo(updateDir);
            UpdateHandler.CheckForUpdates(allUpdateInfo, modsNeedingUpdates);

            string settingsDir = this.GetModSettingsDir(createIfNotExists: true);
            ModSettingsHandler.InitializeModSettings(settingsDir);
            ModSettingsHandler.LoadModSettings(settingsDir);
        }

        public override void OnUpdate()
        {
            KeyCodeHooks();

            // used to test new api methods
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
            }

            if (Game.instance is null)
                return;

            // commented out for now. Need to create a way for popups in BATTD
            /*if (PopupScreen.instance != null)
                UpdateHandler.AnnounceUpdates(modsNeedingUpdates, this.GetModDirectory());*/

            if (InGame.instance is null)
                return;

            if (useModOptionsDEBUG && modOptionsUI is null)
                modOptionsUI = new ModOptionsMenu();

            NotificationMgr.CheckForNotifications();
        }

        private void KeyCodeHooks()
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                    DoPatchMethods(mod => mod.OnKeyDown(key));

                if (Input.GetKeyUp(key))
                    DoPatchMethods(mod => mod.OnKeyUp(key));

                if (Input.GetKey(key))
                    DoPatchMethods(mod => mod.OnKeyHeld(key));
            }
        }

        public override void OnFrontEndWorld()
        {
            // this code won't work for BATTD because it doesn't have PopupScreen.instance. Need to find replacement
            /*if (UpdateHandler.updatedMods && PopupScreen.instance != null)
            {
                PopupScreen.instance.ShowPopup(PopupScreen.Placement.menuCenter, "Restart Required",
                    "You've downloaded new updates for mods, but still need to restart your game to apply them.\n" +
                    "\nWould you like to do that now?", new Action(() => MenuManager.instance.QuitGame()),
                    "Yes, quit the game", null, "Not now", Popup.TransitionAnim.Update);
                UpdateHandler.updatedMods = false;
            }*/

            //TODO: with only external changing, settings should load when going to the main menu
            //TODO: with in game changing, settings should save when going to the main menu
            //ModSettingsHandler.SaveModSettings(modSettingsDir);
            ModSettingsHandler.LoadModSettings(this.GetModSettingsDir());
        }

        public static void DoPatchMethods(Action<BloonsATMod> action) => DoPatchMethods<BloonsATMod>(action);

        public static void DoPatchMethods<T>(Action<T> action) where T : BloonsMod
        {
            foreach (var mod in MelonHandler.Mods.OfType<T>())
            {
                action.Invoke(mod);
            }
        }
    }
}