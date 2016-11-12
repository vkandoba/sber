using System.Linq;
using System.Text.RegularExpressions;

namespace PluginCore.Link
{
    public class PaginationLinkMiner : ILinkMiner
    {
        private readonly string baseUrl;
        private readonly Regex paginatorAjaxArgPattern;
        private readonly Regex urlPattern;

        public PaginationLinkMiner(string baseUrl)
        {
            this.baseUrl = baseUrl;
            urlPattern = new Regex(@"\?p=(\d+)$", RegexOptions.Multiline | RegexOptions.Compiled);
            paginatorAjaxArgPattern = new Regex(@"pagesTotal: (\d+),", RegexOptions.Multiline | RegexOptions.Compiled);
        }

        public string[] Extract(string content)
        {
            var match = paginatorAjaxArgPattern.Match(content);
            var pageCount = int.Parse(match.Groups[1].Value);
            return Enumerable.Range(1, pageCount).Select(Generate).ToArray();
        }

        public bool IsMatch(string url)
        {
            return url.StartsWith(baseUrl) && urlPattern.IsMatch(url);
        }

        private string Generate(int pageNumber)
        {
            return $"{baseUrl}?p={pageNumber}";
        }
    }
}