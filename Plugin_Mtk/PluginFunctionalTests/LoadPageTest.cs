using System.Net;
using NUnit.Framework;
using PluginCore;

namespace PluginFunctionalTests
{
    public class LoadPageTest : TestBase
    {
        private const string page = "http://www.mtk.ru/commercial-buildings/sale/commercial/";

        [Test]
        public void TestUnsafeHeaderParsing()
        {
            Assert.Throws<WebException>(Load);

            PluginConfigrator.UnsafeHeaderParsingOn();
            Assert.DoesNotThrow(Load);

            PluginConfigrator.UnsafeHeaderParsingOff();
            Assert.Throws<WebException>(Load);
        }

        [Test]
        public void TestConcurentUnsafeHeaderParsing()
        {
            PluginConfigrator.UnsafeHeaderParsingOn();
            PluginConfigrator.UnsafeHeaderParsingOn();
            Assert.DoesNotThrow(Load);

            PluginConfigrator.UnsafeHeaderParsingOff();
            Assert.DoesNotThrow(Load);

            PluginConfigrator.UnsafeHeaderParsingOff();
            Assert.Throws<WebException>(Load);

            PluginConfigrator.UnsafeHeaderParsingOff();
            PluginConfigrator.UnsafeHeaderParsingOff();
            PluginConfigrator.UnsafeHeaderParsingOn();
            Assert.DoesNotThrow(Load);
            PluginConfigrator.UnsafeHeaderParsingOff();
        }

        private void Load()
        {
            DownloadPage(page);
        }
    }
}
