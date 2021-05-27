﻿using MelonLoader;
using UnityEngine;

namespace BTD_Mod_Helper.Api.ModOptions
{
    /// <summary>
    /// Class for keeping track of a variable for a Mod that can be changed in game via the Mod Setings menu
    /// </summary>
    /// <typeparam name="T">The type that this ModSetting holds</typeparam>
    public abstract class ModSetting<T> : ModSetting
    {
        internal T value;
        private readonly T defaultValue;
        private string displayName;

        /// <summary>
        /// Constructs a new ModSetting for the given value
        /// </summary>
        /// <param name="value"></param>
        protected ModSetting(T value)
        {
            this.value = value;
            defaultValue = value;
        }

        /// <inheritdoc />
        public virtual object GetValue()
        {
            return value;
        }

        /// <inheritdoc />
        public virtual object GetDefaultValue()
        {
            return defaultValue;
        }

        /// <inheritdoc />
        public virtual void SetValue(object val)
        {
            if (val is T v)
            {
                value = v;
            }
            else
            {
                MelonLogger.Warning($"Error: ModSetting type mismatch between {typeof(T).Name} and {val.GetType().Name}");
            }
        }

        /// <inheritdoc />
        public string GetName()
        {
            return displayName;
        }


        /// <inheritdoc />
        public void SetName(string name)
        {
            displayName = name;
        }

        /// <inheritdoc />
        public abstract ModOption ConstructModOption(GameObject parent);

        /// <inheritdoc />
        public abstract SharedOption ConstructModOption2(GameObject parent);
    }

    
    /// <summary>
    /// 
    /// </summary>
    public interface ModSetting
    {
        /// <summary>
        /// Gets the current value that this ModSetting holds
        /// </summary>
        /// <returns>The value</returns>
        object GetValue();
        
        /// <summary>
        /// Gets the default value for this ModSetting
        /// </summary>
        /// <returns>The default value</returns>
        object GetDefaultValue();

        
        /// <summary>
        /// Sets the current value of this ModSetting
        /// </summary>
        /// <param name="val">The new value</param>
        void SetValue(object val);

        /// <summary>
        /// Gets the Display name for this ModSetting
        /// </summary>
        /// <returns>The display name</returns>
        string GetName();

        /// <summary>
        /// Sets the Display name for this ModSetting
        /// </summary>
        /// <param name="name">The display name</param>
        void SetName(string name);

        /// <summary>
        /// Constructs a visual ModOption for this ModSetting
        /// </summary>
        /// <param name="parent">The parent GameObject to attach to</param>
        /// <returns>The constructed ModOption</returns>
        ModOption ConstructModOption(GameObject parent);

        /// <summary>
        /// Constructs a visual SharedModOption for this ModSetting
        /// </summary>
        /// <param name="parent">The parent GameObject to attach to</param>
        SharedOption ConstructModOption2(GameObject parent);
    }
}