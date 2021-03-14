using System.Collections.Generic;
using Saber.Core;

namespace Saber.Vendor
{
    /// <summary>
    /// Used to handle custom content fields within the Page Content section of Saber's UI.
    /// </summary>
    public interface IVendorContentField
    {
        /// <summary>
        /// Used to render the custom content field within the Page Content section of Saber's UI. .
        /// </summary>
        /// <param name="args">Key/Value pairs that were included in the HTML mustache variable associated with the content field</param>
        /// <param name="data">serialized user data that was saved for the content field</param>
        /// <param name="id">The id given to a hidden HTML input field that will be used to store the field's data 
        /// on the client-side, which is then used when saving content field data to a multilingual JSON file on the 
        /// server after the user modifies & saves their page content.</param>
        /// <param name="key">The full mustache variable name for this particular instance.</param>
        /// <param name="request">The current page request, which includes User session & page parameters</param>
        /// <param name="lang">The language being displayed. Default "en" for English.</param>
        /// <param name="container">The CSS selector used for the content fields HTML container (since there may be multiple content fields forms loaded in the editor)</param>
        /// <returns>An HTML string.</returns>
        string Render(IRequest request, Dictionary<string, string> args, string data, string id, string prefix, string key, string lang, string container);
    }
}
