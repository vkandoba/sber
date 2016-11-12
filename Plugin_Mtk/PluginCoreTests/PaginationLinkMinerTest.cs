using NUnit.Framework;
using PluginCore.Link;

namespace PluginCoreTests
{
    public class PaginationLinkMinerTest
    {
        private PaginationLinkMiner paginationLinkMiner;
        private const string baseUrl = "http://www.mtk.ru/business/sale/business/";

        [SetUp]
        public void Init()
        {
            paginationLinkMiner = new PaginationLinkMiner(baseUrl);
        }

        [Test]
        public void ExtractTest()
        {
            var ajax = @"
	                    $('#paginator').paginator({
		                    pagesTotal: 3, 
		                    pagesSpan: 10, 
		                    pageCurrent: pagesArr, 
		                    baseUrl: 'pages=%number%',
		                    pageScroll: 3,
		                    events: {
			                    keyboard: false,
			                    scroll: true
		                    }
	                    });";
            Assert.That(paginationLinkMiner.Extract(ajax), Is.EquivalentTo(new []
            {
                "http://www.mtk.ru/business/sale/business/?p=1",
                "http://www.mtk.ru/business/sale/business/?p=2",
                "http://www.mtk.ru/business/sale/business/?p=3",
            }));
        }

        [TestCase("http://www.mtk.ru/list/business/id-198/", false)]
        [TestCase("http://www.mtk.ru/business/sale/business/", false)]
        [TestCase("http://www.mtk.ru/business/sale/business/?p=1", true)]
        public void IsMatchTest(string url, bool expected)
        {
            Assert.That(paginationLinkMiner.IsMatch(url), Is.EqualTo(expected));
        }
    }
}