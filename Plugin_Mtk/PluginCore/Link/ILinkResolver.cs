namespace PluginCore.Link
{
    public interface ILinkResolver
    {
        LinkType GetType(string url);
        string ParseLinkToList(string url, out int pageNumber);
    }
}
