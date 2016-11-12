using System.Collections.Generic;
using LowLevel;
using PluginCore;
using PluginCore.Link.TypeResolve;
using PluginCore.Load;

namespace Plugin_Mtk
{
    public class MtkLoadPlugin : IMtkPlugin<string>
    {
        private readonly DatacolHttp httpClient;
        private readonly IContentLoader contentLoader;
        private readonly IListLinkResolver listLinkResolver;

        public MtkLoadPlugin(DatacolHttp httpClient, IContentLoader contentLoader, IListLinkResolver listLinkResolver)
        {
            this.httpClient = httpClient;
            this.contentLoader = contentLoader;
            this.listLinkResolver = listLinkResolver;
        }

        public string Handle(MtkPluginParameters parameters, out string error)
        {
            error = "";
            string content;
            if (parameters.Type == LinkType.Base || parameters.Type == LinkType.Object)
            {
                var outParams = new Dictionary<string, object>();
                PluginConfigrator.UnsafeHeaderParsingOn();
                try
                {
                    content = httpClient.request(parameters.Url, null, out outParams, out error);
                }
                finally
                {
                    PluginConfigrator.UnsafeHeaderParsingOff();
                }
            }
            else
            {
                int pageNumber;
                var url = listLinkResolver.ParseLinkToList(parameters.Url, out pageNumber);
                content = contentLoader.GetListContent(url, pageNumber);
            }
            return content;
        }
    }
}