namespace PluginCore.Link.TypeResolve
{
    public interface IListLinkResolver : ILinkResolver
    {
        string ParseLinkToList(string url, out int pageNumber);
    }
}