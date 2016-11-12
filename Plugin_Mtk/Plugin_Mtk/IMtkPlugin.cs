using System;

namespace Plugin_Mtk
{
    public interface IMtkPlugin<out T>
    {
        T Handle(MtkPluginParameters parameters, out string error);
    }
}