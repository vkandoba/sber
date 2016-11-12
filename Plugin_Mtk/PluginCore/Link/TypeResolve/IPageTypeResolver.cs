namespace PluginCore.Link.TypeResolve
{
    public interface IPageTypeResolver
    {
        PageType GetPageType(string url);
    }
}
