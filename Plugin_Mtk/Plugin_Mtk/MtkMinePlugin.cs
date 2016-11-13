using System.Collections.Generic;
using PluginCore.Link.Mine;
using PluginCore.Link.TypeResolve;

namespace Plugin_Mtk
{
    public class MtkMinePlugin : IMtkPlugin<HashSet<string>>
    {
        private readonly ILinkMiner objectLinkMiner;
        private readonly IPaginationLinkMinerFactory linkMinerFactory;

        public MtkMinePlugin(ILinkMiner objectLinkMiner, IPaginationLinkMinerFactory paginationLinkMinerFactory)
        {
            this.objectLinkMiner = objectLinkMiner;
            this.linkMinerFactory = paginationLinkMinerFactory;
        }

        public HashSet<string> Handle(MtkPluginParameters parameters, out string error)
        {
            error = "";
            return new HashSet<string>(MineLinks(parameters));
        }

        private string[] MineLinks(MtkPluginParameters parameters)
        {
            switch (parameters.PageType)
            {
                case PageType.Base:
                    return linkMinerFactory.CreatePaginationLinkMiner(parameters.Url).Extract(parameters.Content);
                case PageType.List:
                    return objectLinkMiner.Extract(parameters.Content);
                default:
                    return new string[0];
            }
        }
    }
}