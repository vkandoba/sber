using PluginCore.Link.Mine;

namespace PluginCore.Link.TypeResolve
{
    public class PageTypeResolver : IPageTypeResolver
    {
        private readonly ILinkResolver objectLinkResolver;
        private readonly IListLinkResolver listLinkResolver;

        public PageTypeResolver(ILinkResolver objectLinkResolver, IListLinkResolver listLinkResolver)
        {
            this.objectLinkResolver = objectLinkResolver;
            this.listLinkResolver = listLinkResolver;
        }

        public PageType GetPageType(string url)
        {
            if (listLinkResolver.IsMatch(url))
                return PageType.List;
            if (objectLinkResolver.IsMatch(url))
                return PageType.Object;
            return PageType.Base;
        }
    }
}