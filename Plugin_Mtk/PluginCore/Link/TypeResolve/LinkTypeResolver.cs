using System;
using System.Text.RegularExpressions;
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

        public string ParseLinkToList(string url, out int pageNumber)
        {
            var queryPatten = new Regex(@"\?p=(\d+)$", RegexOptions.Multiline | RegexOptions.Compiled);
            pageNumber = int.Parse(queryPatten.Match(url).Groups[1].Value);
            return new Uri(url).GetLeftPart(UriPartial.Path);
        }
    }
}