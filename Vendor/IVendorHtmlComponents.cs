using System.Collections.Generic;

namespace Saber.Vendor
{
    public interface IVendorHtmlComponents
    {
        /// <summary>
        /// Bind one or more HTML components to to be used 
        /// as HTML mustache variables within Saber web pages
        /// </summary>
        /// <returns></returns>
        List<HtmlComponentModel> Bind();
    }
}
