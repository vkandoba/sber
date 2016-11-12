namespace PluginCore.Link
{
    public interface ILinkTypeResolver
    {
        LinkType GetType(string url);
        string ParseLinkToList(string url, out int pageNumber);
    }
}
