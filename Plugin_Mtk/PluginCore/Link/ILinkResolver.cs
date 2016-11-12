namespace PluginCore.Link
{
    public interface ILinkResolver
    {
        bool IsMatch(string url);
    }
}