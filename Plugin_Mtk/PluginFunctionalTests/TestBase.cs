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

        [SetUp]
        public virtual void Init()
        {
            loader = new ContentLoader();
        }

        [TearDown]
        public virtual void Destroy()
        {
        }
    }
}