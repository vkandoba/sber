using System;

namespace PluginCore.Load
{
    public interface IContentLoader
    {
        string GetListContent(string url, int pageNumber);
    }
}