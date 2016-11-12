using PluginCore.Link.Mine;

namespace PluginCore.Link.TypeResolve
{
    public class LinkTypeResolver : ILinkTypeResolver
    {
        private readonly ObjectLinkMiner objectLinkMiner = new ObjectLinkMiner();
        private readonly ListLinkResolver listLinkResolver = new ListLinkResolver();

        public LinkType GetType(string url)
        {
            if (listLinkResolver.IsMatch(url))
                return LinkType.List;
            if (objectLinkMiner.IsMatch(url))
                return LinkType.Object;
            return LinkType.Base;
        }
    }
}