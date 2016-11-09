using System.Net;
using System.Text;
using NUnit.Framework;

namespace PluginFunctionalTests
{
    [TestFixture]
    public class TestBase
    {
        private WebClient client;

        [SetUp]
        public void Init()
        {
            client = new WebClient();
        }

        protected string DownloadPage(string url)
        {
            return Encoding.UTF8.GetString(client.DownloadData(url));
        }

        [TearDown]
        public void Destroy()
        {
            client.Dispose();
        }
    }
}