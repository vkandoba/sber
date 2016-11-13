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
            if (parameters.PageType == PageType.List)
                return DownloadListPage(parameters.Url);

            return DonwloadPageByDatacol(parameters.Url, out error);
        }

        private string DonwloadPageByDatacol(string url, out string error)
        {
            var outParams = new Dictionary<string, object>();
            PluginConfigrator.UnsafeHeaderParsingOn();
            try
            {
                 return httpClient.request(url, null, out outParams, out error);
            }
            finally
            {
                PluginConfigrator.UnsafeHeaderParsingOff();
            }
        }

        private string DownloadListPage(string url)
        {
            int pageNumber;
            var baseUrl = listLinkResolver.ParseLinkToList(url, out pageNumber);
            return contentLoader.GetListContent(baseUrl, pageNumber);
        }
    }
}