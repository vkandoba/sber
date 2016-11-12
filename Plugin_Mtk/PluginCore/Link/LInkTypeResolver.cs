using System;

namespace PluginCore.Link
{
    public class LInkTypeResolver : ILInkTypeResolver
    {
        private readonly ObjectLinkMiner objectLinkMiner = new ObjectLinkMiner();

        public LinkType GetType(string url)
        {
            if (new PaginationLinkMiner(new Uri(url).GetLeftPart(UriPartial.Path)).IsMatch(url))
                return LinkType.List;
            if (objectLinkMiner.IsMatch(url))
                return LinkType.Object;
            return LinkType.Base;
        }
    }
}