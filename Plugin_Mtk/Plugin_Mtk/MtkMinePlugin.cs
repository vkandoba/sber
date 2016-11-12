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
            switch (parameters.PageType)
            {
                case PageType.Base:
                    return new HashSet<string>(linkMinerFactory.CreatePaginationLinkMiner(parameters.Url).Extract(parameters.Content));
                case PageType.List:
                    return new HashSet<string>(objectLinkMiner.Extract(parameters.Content));
                default:
                    return new HashSet<string>();
            }
        }
    }
}