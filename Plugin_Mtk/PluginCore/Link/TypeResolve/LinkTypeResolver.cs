using PluginCore.Link.Mine;

namespace PluginCore.Link.TypeResolve
{
    public class LinkTypeResolver : ILinkTypeResolver
    {
        private readonly ILinkResolver objectLinkResolver;
        private readonly IListLinkResolver listLinkResolver;

        public LinkTypeResolver(ILinkResolver objectLinkResolver, IListLinkResolver listLinkResolver)
        {
            this.objectLinkResolver = objectLinkResolver;
            this.listLinkResolver = listLinkResolver;
        }

        public LinkType GetType(string url)
        {
            if (listLinkResolver.IsMatch(url))
                return LinkType.List;
            if (objectLinkResolver.IsMatch(url))
                return LinkType.Object;
            return LinkType.Base;
        }
    }
}