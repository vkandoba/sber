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
        public void UnsafeHeaderParsingTest()
        {
            Assert.Throws<WebException>(LoadPage);

            Assert.True(PluginConfigrator.UnsafeHeaderParsingOn());
            Assert.DoesNotThrow(LoadPage);

            Assert.True(PluginConfigrator.UnsafeHeaderParsingOff());
            Assert.Throws<WebException>(LoadPage);
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
