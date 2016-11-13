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
        private ListLinkResolver listLinkResolver;
        private IPageTypeResolver pageTypeResolver;
        private MtkMinePlugin mtkMinePlugin;

        public MtkPluginFactory()
        {
            contentLoader = new ContentLoader();
            var objectLinkMiner = new ObjectLinkMiner();
            listLinkResolver = new ListLinkResolver();
            pageTypeResolver = new PageTypeResolver(objectLinkMiner, listLinkResolver);
            mtkMinePlugin = new MtkMinePlugin(objectLinkMiner, new PaginationLinkMinerFactory());
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
                PageType = pageTypeResolver.GetPageType(url),
                Content = parameters.SafeGet("content", "").ToString()
            };
        }
    }
}