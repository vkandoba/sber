namespace PluginCore.Link.TypeResolve
{
    public interface ILinkTypeResolver
    {
        LinkType GetType(string url);
    }
}
