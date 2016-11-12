using NUnit.Framework;
using PluginCore.Link;

namespace PluginCoreTests
{
    public class LinkTypeResolverTest
    {
        private LInkTypeResolver linkTypeResolver;

        [SetUp]
        public void Init()
        {
            linkTypeResolver = new LInkTypeResolver();
        }

        [TestCase("http://www.mtk.ru/list/business/id-198/", LinkType.Object)]
        [TestCase("http://www.mtk.ru/business/sale/business/", LinkType.Base)]
        [TestCase("http://www.mtk.ru/business/sale/business/?p=1", LinkType.List)]
        public void IsMatchTest(string url, LinkType expected)
        {
           Assert.That(linkTypeResolver.GetType(url), Is.EqualTo(expected));
        }
    }
}