using NUnit.Framework;
using PluginCore.Link;

namespace PluginCoreTests
{
    public class LinkResolverTest
    {
        private ILinkResolver linkResolver;

        [SetUp]
        public void Init()
        {
            linkResolver = new LinkResolver();
        }

        [TestCase("http://www.mtk.ru/list/business/id-198/", LinkType.Object)]
        [TestCase("http://www.mtk.ru/business/sale/business/", LinkType.Base)]
        [TestCase("http://www.mtk.ru/business/sale/business/?p=1", LinkType.List)]
        public void IsMatchTest(string url, LinkType expected)
        {
           Assert.That(linkResolver.GetType(url), Is.EqualTo(expected));
        }
    }
}