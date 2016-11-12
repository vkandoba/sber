namespace PluginCore.Link
{
    public interface ILInkTypeResolver
    {
        LinkType GetType(string url);
    }
}
