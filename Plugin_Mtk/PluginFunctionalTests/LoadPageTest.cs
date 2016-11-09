using System.Net;
using NUnit.Framework;
using PluginCore;

namespace PluginFunctionalTests
{
    [TestFixture]
    public class LoadPageTest
    {
        private WebClient client;
        private const string pageUrl = "http://www.mtk.ru/commercial-buildings/sale/commercial/";

        [SetUp]
        public void Init()
        {
            client = new WebClient();
        }

        [Test]
        public void TestUnsafeHeaderParsing()
        {
            Assert.Throws<WebException>(LoadPage);

            PluginConfigrator.UnsafeHeaderParsingOn();
            Assert.DoesNotThrow(LoadPage);

            PluginConfigrator.UnsafeHeaderParsingOff();
            Assert.Throws<WebException>(LoadPage);
        }

        [Test]
        public void TestConcurentUnsafeHeaderParsing()
        {
            PluginConfigrator.UnsafeHeaderParsingOn();
            PluginConfigrator.UnsafeHeaderParsingOn();
            Assert.DoesNotThrow(LoadPage);

            PluginConfigrator.UnsafeHeaderParsingOff();
            Assert.DoesNotThrow(LoadPage);

            PluginConfigrator.UnsafeHeaderParsingOff();
            Assert.Throws<WebException>(LoadPage);

            PluginConfigrator.UnsafeHeaderParsingOff();
            PluginConfigrator.UnsafeHeaderParsingOff();
            PluginConfigrator.UnsafeHeaderParsingOn();
            Assert.DoesNotThrow(LoadPage);
        }

        private void LoadPage()
        {
            client.DownloadData(pageUrl);
        }

        [TearDown]
        public void Destroy()
        {
            client.Dispose();
        }
    }
}
