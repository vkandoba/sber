namespace PluginCore.Link
{
    public interface ILinkMiner
    {
        string[] Extract(string content);
        bool IsMatch(string url);
    }
}