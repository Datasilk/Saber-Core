using System.Text;

namespace Saber.Core
{
    public interface IController: Datasilk.Core.Web.IController
    {
        bool UsePlatform { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string Theme { get; set; }
        StringBuilder Footer { get; set; }
        EditorType EditorUsed { get; }
        bool ContainsResource(string url);
    }
}