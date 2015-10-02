namespace UmbracoBase.Web.Globals.Attributes
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Allows the display attribute to get string values from the Umbraco dictionary based on key.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class UmbracoDisplayAttribute : DisplayNameAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UmbracoDisplayAttribute"/>.
        /// </summary>
        /// <param name="dictionaryKey">The Umbraco dictionary key.</param>
        public UmbracoDisplayAttribute(string dictionaryKey)
            : base(dictionaryKey)
        {

        }

        /// <summary>
        /// Gets the display name from the Umbraco dictionary based on key.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                string displayName = umbraco.library.GetDictionaryItem(base.DisplayName);
                return string.IsNullOrWhiteSpace(displayName) ? base.DisplayName : displayName;
            }
        }
    }
}