using System;
using System.Linq;
using NUnit.Framework;
using PluginCore;
using PluginCore.Link;

namespace PluginFunctionalTests
{
    public class MineLinkTest : TestBase
    {
        private ObjectLinkMiner objectLinkMiner;
        private const string page = "http://www.mtk.ru/business/sale/business/";

        public override void Init()
        {
            base.Init();
            PluginConfigrator.UnsafeHeaderParsingOn();
            objectLinkMiner = new ObjectLinkMiner();
        }

        [Test]
        public void TestExtractLinkToObjects()
        {
            var urls = objectLinkMiner.Extract(loader.GetListContent(page, 1));

            Assert.True(urls.All(x => x.Contains("id-") && objectLinkMiner.IsMatch(x)));

            Console.WriteLine(urls.Aggregate("", (acc, url) => $"{acc}\n{url}"));
        }

        [Test]
        public void TestExtractLinkToPages()
        {
            var paginationLinkMiner = new PaginationLinkMiner(page);
            var urls = paginationLinkMiner.Extract(Download(page));

            Assert.That(urls, Is.EquivalentTo(new[]
            {
                "http://www.mtk.ru/business/sale/business/?p=1",
                "http://www.mtk.ru/business/sale/business/?p=2",
                "http://www.mtk.ru/business/sale/business/?p=3",
                "http://www.mtk.ru/business/sale/business/?p=4",
                "http://www.mtk.ru/business/sale/business/?p=5"
            }));

            Console.WriteLine(urls.Aggregate("", (acc, url) => $"{acc}\n{url}"));
        }

        public override void Destroy()
        {
            base.Destroy();
            PluginConfigrator.UnsafeHeaderParsingOff();
        }
    }
}