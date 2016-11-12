namespace PluginCore.Link.TypeResolve
{
    public interface ILinkTypeResolver
    {
        LinkType GetType(string url);
        string ParseLinkToList(string url, out int pageNumber);
    }
}
