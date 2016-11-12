using System.Net;
using System.Text;
using NUnit.Framework;
using PluginCore.Load;

namespace PluginFunctionalTests
{
    [TestFixture]
    public class TestBase
    {
        private WebClient client;
        protected ContentLoader loader;

        [SetUp]
        public virtual void Init()
        {
            client = new WebClient();
            loader = new ContentLoader();
        }

        protected string DownloadPage(string url)
        {
            return Encoding.UTF8.GetString(client.DownloadData(url));
        }

        [TearDown]
        public virtual void Destroy()
        {
            client.Dispose();
        }
    }
}