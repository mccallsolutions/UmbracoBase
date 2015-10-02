namespace UmbracoBase.Web.Globals.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>
    /// Allows the required attribute to get string values from the Umbraco dictionary based on key.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class UmbracoRequiredAttribute : RequiredAttribute, IClientValidatable
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UmbracoRequiredAttribute"/>.
        /// </summary>
        /// <param name="dictionaryKey">The Umbraco dictionary key.</param>
        public UmbracoRequiredAttribute(string dictionaryKey)
        {
            ErrorMessage = dictionaryKey;
        }

        /// <summary>
        /// Format the error string to be from the Umbraco dictionary.
        /// </summary>
        /// <param name="name">The dictionary key.</param>
        /// <returns>The error message from the Umbraco dictionary.</returns>
        public override string FormatErrorMessage(string name)
        {
            return umbraco.library.GetDictionaryItem(base.FormatErrorMessage(name));
        }

        /// <summary>
        /// Helps setup client side validation for the required attribute.
        /// </summary>
        /// <param name="metadata">The <see cref="ModelMetadata"/> object.</param>
        /// <param name="context">The <see cref="ControllerContext"/> object.</param>
        /// <returns>A list of <see cref="ModelClientValidationRule"/> objects.</returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            // Kodus to "Chad" http://stackoverflow.com/a/9914117
            yield return new ModelClientValidationRule
            {
                ErrorMessage = umbraco.library.GetDictionaryItem(ErrorMessage),
                ValidationType = "required"
            };
        }
    }
}