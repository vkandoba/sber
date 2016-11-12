namespace PluginCore.Link.TypeResolve
{
    public interface ILinkResolver
    {
        bool IsMatch(string url);
    }
}