using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PluginCore.Link
{
    public class ObjectLinkMiner : ILinkMiner
    {
        private readonly Regex urlPattern = new Regex(@"<a[^>]+href\s*=\s*[\""\']{0,1}([^\s""'>]+)[""'\s >]+");

        public string[] Extract(string content)
        {
            return GetUrls(content).ToArray();
        }

        private IEnumerable<string> GetUrls(string content)
        {
            foreach (Match match in urlPattern.Matches(content))
            {
                yield return match.Groups[1].Value;
            }
        }
    }
}