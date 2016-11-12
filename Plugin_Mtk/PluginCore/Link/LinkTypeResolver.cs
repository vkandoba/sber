using System;
using System.Text.RegularExpressions;

namespace PluginCore.Link
{
    public class LinkTypeResolver : ILinkTypeResolver
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

        public string ParseLinkToList(string url, out int pageNumber)
        {
            var queryPatten = new Regex(@"\?p=(\d+)$", RegexOptions.Multiline | RegexOptions.Compiled);
            pageNumber = int.Parse(queryPatten.Match(url).Groups[1].Value);
            return new Uri(url).GetLeftPart(UriPartial.Path);
        }
    }
}