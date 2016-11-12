using System.Collections.Generic;

namespace Plugin_Mtk
{
    public interface IMtkPluginFactory
    {
        MtkLoadPlugin CreateLoadPlugin(Dictionary<string, object> parameters);
        MtkMinePlugin GetMinePlugin();
        MtkPluginParameters GetParameters(Dictionary<string, object> parameters);
    }
}