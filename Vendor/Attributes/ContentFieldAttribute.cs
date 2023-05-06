using System;

namespace Saber.Vendor
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ContentFieldAttribute : Attribute
    {
        public string FieldName { get; set; }
        public ContentFieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
    /// <summary>
    /// Used with IVendorContentField to replace the entire HTML row when rendering
    /// a content field in the Saber Editor Content Fields tab
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ReplaceRowAttribute : Attribute{}
}
