using System.Net;
using System.Text;
using NUnit.Framework;
using PluginCore.Load;

namespace PluginFunctionalTests
{
    [TestFixture]
    public class TestBase
    {
        protected ContentLoader loader;
        private WebClient client;

        [SetUp]
        public virtual void Init()
        {
            loader = new ContentLoader();
            client = new WebClient();
        }

        [TearDown]
        public virtual void Destroy()
        {
            client.Dispose();
        }

        protected virtual string Download(string url)
        {
            return Encoding.UTF8.GetString(client.DownloadData(url));
        }
    }
}