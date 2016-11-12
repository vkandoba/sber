namespace PluginCore.Link.Mine
{
    public interface IPaginationLinkMinerFactory
    {
        ILinkMiner CreatePaginationLinkMiner(string baseUrl);
    }
}