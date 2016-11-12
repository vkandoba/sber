using NUnit.Framework;
using PluginCore.Link;
using PluginCore.Link.Mine;

namespace PluginCoreTests
{
    public class MineLinkTest
    {
        private ObjectLinkMiner objectLinkMiner;
        private const string page = "http://www.mtk.ru/business/sale/business/";

        [SetUp]
        public void Init()
        {
            objectLinkMiner = new ObjectLinkMiner();
        }

        [TestCase(@"<a href=""#"">")]
        [TestCase(@"<a href=""/"">")]
        [TestCase(@"<a href=""/business/sale/business/?sort=price_up"">")]
        [TestCase(@"<a href=""/business/sale/business/?phone=229"">")]
        [TestCase(@"<a href=""http://www.mtk.ru/egrp/"">")]
        [TestCase(@"<div link=""http://www.mtk.ru/list/business/id-198/"">")]
        [TestCase(@"<a link=""http://www.mtk.ru/list/business/id-198/"">")]
        public void NoLinkExtractTest(string input)
        {
            Assert.That(objectLinkMiner.Extract(input), Is.EquivalentTo(new string[0]));
        }

        [Test]
        public void LinkExtractTest()
        {
            Assert.That(objectLinkMiner.Extract(@"<a href=""http://www.mtk.ru/list/business/id-198/""/>"), Is.EquivalentTo(new [] {"http://www.mtk.ru/list/business/id-198/"}));
        }

        [Test]
        public void ManyLinkExtractTest()
        {
            var content = @"
                            <a href=""http://www.mtk.ru/list/business/id-198/""/>
                            <div/>
                            <a href=""http://www.mtk.ru/list/business/id-446/""/>
            ";
            Assert.That(objectLinkMiner.Extract(content), Is.EquivalentTo(new []
            {
                "http://www.mtk.ru/list/business/id-198/",
                "http://www.mtk.ru/list/business/id-446/"
            }));
        }

        [Test]
        public void LinkExtractUniqurTest()
        {
            var content = @"<a href=""http://www.mtk.ru/list/business/id-198/""/>
                            <a href=""http://www.mtk.ru/list/business/id-198/""/>";
            Assert.That(objectLinkMiner.Extract(content), Is.EquivalentTo(new [] {"http://www.mtk.ru/list/business/id-198/"}));
        }

        [TestCase("http://www.mtk.ru/list/business/id-198/", true)]
        [TestCase("http://www.mtk.ru/business/sale/business/", false)]
        [TestCase("http://www.mtk.ru/business/sale/business/?p=1", false)]
        public void IsMatchTest(string url, bool expected)
        {
            Assert.That(objectLinkMiner.IsMatch(url), Is.EqualTo(expected));
        }
    }
}