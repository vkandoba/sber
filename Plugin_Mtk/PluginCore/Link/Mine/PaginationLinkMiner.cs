using System.Linq;
using System.Text.RegularExpressions;

namespace PluginCore.Link.Mine
{
    public class PaginationLinkMiner : ILinkMiner
    {
        private readonly string baseUrl;
        private readonly Regex paginatorAjaxArgPattern = new Regex(@"pagesTotal: (\d+),", RegexOptions.Multiline | RegexOptions.Compiled);

        public PaginationLinkMiner(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public string[] Extract(string content)
        {
            var match = paginatorAjaxArgPattern.Match(content);
            var pageCount = int.Parse(match.Groups[1].Value);
            return Enumerable.Range(1, pageCount).Select(Generate).ToArray();
        }

        private string Generate(int pageNumber)
        {
            return $"{baseUrl}?p={pageNumber}";
        }
    }
}