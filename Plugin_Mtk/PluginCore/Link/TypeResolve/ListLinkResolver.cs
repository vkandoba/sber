using System;
using System.Text.RegularExpressions;

namespace PluginCore.Link.TypeResolve
{
    public class ListLinkResolver : IListLinkResolver
    {
        private readonly Regex queryPattern = new Regex(@"\?p=(\d+)$", RegexOptions.Multiline | RegexOptions.Compiled);

        public bool IsMatch(string url)
        {
            return queryPattern.IsMatch(url);
        }

        public string ParseLinkToList(string url, out int pageNumber)
        {
            pageNumber = int.Parse(queryPattern.Match(url).Groups[1].Value);
            return new Uri(url).GetLeftPart(UriPartial.Path);
        }
    }
}