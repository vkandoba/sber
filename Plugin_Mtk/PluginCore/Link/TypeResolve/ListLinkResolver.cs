using System.Text.RegularExpressions;

namespace PluginCore.Link.TypeResolve
{
    public class ListLinkResolver : ILinkResolver
    {
        private readonly Regex queryPattern = new Regex(@"\?p=(\d+)$", RegexOptions.Multiline | RegexOptions.Compiled);

        public bool IsMatch(string url)
        {
            return queryPattern.IsMatch(url);
        }
    }
}