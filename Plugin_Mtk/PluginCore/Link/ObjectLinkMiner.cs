using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PluginCore.Link
{
    public class ObjectLinkMiner : ILinkMiner
    {
        private readonly Regex objectUrlPattern = new Regex(@"<a[^>]+href\s*=\s*[\""\']{0,1}(http:\/\/(?:w{3}\.)?mtk\.ru.+\/id-\d+\/)[""'\s>]+", RegexOptions.Multiline | RegexOptions.Compiled);

        public string[] Extract(string content)
        {
            return GetUrls(content).ToArray();
        }

        private IEnumerable<string> GetUrls(string content)
        {
            foreach (Match match in objectUrlPattern.Matches(content))
            {
                yield return match.Groups[1].Value;
            }
        }
    }
}