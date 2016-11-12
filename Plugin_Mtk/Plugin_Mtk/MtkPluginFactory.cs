using System.Collections.Generic;
using LowLevel;
using PluginCore.Link.Mine;
using PluginCore.Link.TypeResolve;
using PluginCore.Load;

namespace Plugin_Mtk
{
    public class MtkPluginFactory : IMtkPluginFactory
    {
        private ContentLoader contentLoader;
        private ObjectLinkMiner objectLinkMiner;
        private ListLinkResolver listLinkResolver;
        private ILinkTypeResolver linkTypeResolver;
        private MtkMinePlugin mtkMinePlugin;

        public MtkPluginFactory()
        {
            contentLoader = new ContentLoader();
            objectLinkMiner = new ObjectLinkMiner();
            listLinkResolver = new ListLinkResolver();
            linkTypeResolver = new LinkTypeResolver(objectLinkMiner, listLinkResolver);
            mtkMinePlugin = new MtkMinePlugin(objectLinkMiner);
        }

        public MtkLoadPlugin CreateLoadPlugin(Dictionary<string, object> parameters)
        {
            var http = (DatacolHttp)parameters["datacolhttp"];
            return new MtkLoadPlugin(http, contentLoader, listLinkResolver);
        }

        public MtkMinePlugin GetMinePlugin()
        {
            return mtkMinePlugin;
        }

        public MtkPluginParameters GetParameters(Dictionary<string, object> parameters)
        {
            string url = parameters["url"].ToString();
            return new MtkPluginParameters
            {
                Url = url,
                Type = linkTypeResolver.GetType(url),
                Content = parameters["content"].ToString()
            };
        }
    }
}