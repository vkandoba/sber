namespace PluginCore.Link.Mine
{
    public class PaginationLinkMinerFactory : IPaginationLinkMinerFactory
    {
        public ILinkMiner CreatePaginationLinkMiner(string baseUrl)
        {
            return new PaginationLinkMiner(baseUrl);
        }
    }
}