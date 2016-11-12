using System;
using System.Linq;
using NUnit.Framework;
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
            objectLinkMiner = new ObjectLinkMiner();
        }

        [Test]
        public void ExtractObjectLinkTest()
        {
            var urls = objectLinkMiner.Extract(loader.GetListContent(page, 1));
            Assert.True(urls.All(x => x.Contains("id-")));
            Console.WriteLine(urls.Aggregate("", (acc, url) => $"{acc}\n{url}"));
        }
    }
}