using System.IO;
using System.Net;
using System.Text;

namespace PluginCore.Load
{
    public class ContentLoader : IContentLoader
    {
        private static readonly Encoding encoding = Encoding.UTF8;

        public string GetListContent(string url, int pageNumber)
        {
            PluginConfigrator.UnsafeHeaderParsingOn();
            try
            {
                var request = WebRequest.Create($"{url}?ajax={1}&p={pageNumber}");

                var listContent = "";
                using (var response = request.GetResponse())
                {
                    var responseStream = response.GetResponseStream();
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream, encoding))
                            listContent = reader.ReadToEnd();
                }
                return listContent;
            }
            finally
            {
                PluginConfigrator.UnsafeHeaderParsingOff();
            }
        }
    }
}