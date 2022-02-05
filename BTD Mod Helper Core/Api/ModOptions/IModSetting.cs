using BTD_Mod_Helper.Api.Components;

namespace BTD_Mod_Helper.Api.ModOptions
{
    /// <summary>
    /// Base interface for a ModSetting
    /// </summary>
    public interface IModSetting
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
        /// Constructs a visual ModHelperComponent for this ModSetting
        /// </summary>
        /// <returns>The created ModHelperComponent</returns>
        ModHelperOption CreateComponent();

        /// <summary>
        /// Validates the current value using the customValidation function, if there is one.
        /// If there were no issues, performs the onSave action
        /// </summary>
        bool OnSave();

        /// <summary>
        /// Gets the category that this is a part of, or null
        /// </summary>
        ModSettingCategory GetCategory();

        /// <summary>
        /// Gets the current ModHelperComponent for this setting. Will be null if outside the settings menu
        /// </summary>
        ModHelperOption GetComponent();
        
        /// <summary>
        /// Sets the currently created component
        /// </summary>
        void SetComponent(ModHelperOption component);
    }
}