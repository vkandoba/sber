using NUnit.Framework;
using PluginCore.Link;
using PluginCore.Link.Mine;
using PluginCore.Link.TypeResolve;

namespace PluginCoreTests
{
    public class PageTypeResolverTest
    {
        private IPageTypeResolver psgeTypeResolver;

        [SetUp]
        public void Init()
        {
            psgeTypeResolver = new PageTypeResolver(new ObjectLinkMiner(), new ListLinkResolver());
        }

        [TestCase("http://www.mtk.ru/list/business/id-198/", PageType.Object)]
        [TestCase("http://www.mtk.ru/business/sale/business/", PageType.Base)]
        [TestCase("http://www.mtk.ru/business/sale/business/?p=1", PageType.List)]
        public void IsMatchTest(string url, PageType expected)
        {
           Assert.That(psgeTypeResolver.GetPageType(url), Is.EqualTo(expected));
        }
    }
}