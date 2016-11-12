using System.Collections.Generic;
using PluginCore.Link.Mine;
using PluginCore.Link.TypeResolve;

namespace Plugin_Mtk
{
    public class MtkMinePlugin : IMtkPlugin<HashSet<string>>
    {
        private readonly ILinkMiner objectLinkMiner;

        public MtkMinePlugin(ILinkMiner objectLinkMiner)
        {
            this.objectLinkMiner = objectLinkMiner;
        }

        public HashSet<string> Handle(MtkPluginParameters parameters, out string error)
        {
            error = "";
            switch (parameters.Type)
            {
                case LinkType.Base:
                    return new HashSet<string>(new PaginationLinkMiner(parameters.Url).Extract(parameters.Content));
                case LinkType.List:
                    return new HashSet<string>(objectLinkMiner.Extract(parameters.Content));
                default:
                    return new HashSet<string>();
            }
        }
    }
}