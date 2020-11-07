namespace Saber.Core
{
    public interface IService : Datasilk.Core.Web.IService
    {
        EditorType EditorUsed { get; }
        string JsonResponse(dynamic obj);
    }
}