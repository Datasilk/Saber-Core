﻿using System;

namespace Saber.Vendor
{
    /// <summary>
    /// Signifies that a specific Service class method is accessable by the Public API systenm
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class PublicApiAttribute : Attribute { 
        public string Description { get; set; }
        public string[] Parameters { get; set; }

        public PublicApiAttribute(string description, params string[] parameters)
        {
            Description = description;
            Parameters = parameters;
        }
    }
}
