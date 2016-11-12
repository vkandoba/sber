using NUnit.Framework;
using PluginCore.Link;

namespace PluginCoreTests
{
    public class ListLinkResolverTest
    {
        private ListLinkResolver listLinkResolver;

        [SetUp]
        public void Init()
        {
            listLinkResolver = new ListLinkResolver();
        }

        [TestCase("http://www.mtk.ru/list/business/id-198/", false)]
        [TestCase("http://www.mtk.ru/business/sale/business/", false)]
        [TestCase("http://www.mtk.ru/business/sale/business/?p=1", true)]
        public void IsMatchTest(string url, bool expected)
        {
            Assert.That(listLinkResolver.IsMatch(url), Is.EqualTo(expected));
        }
    }
}