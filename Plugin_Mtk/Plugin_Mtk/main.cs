using System;
using System.Collections.Generic;
using LowLevel;

namespace Plugin_Mtk
{
    public class HandlerClass : PluginInterface.IPlugin
    {
        private readonly MtkPluginFactory mtkPluginFactory;

        public HandlerClass()
        {
            mtkPluginFactory = new MtkPluginFactory();
        }

        public object pluginHandler(Dictionary<string, object> parameters, out string error)
        {
            try
            {
                error = "";
                string type = parameters["type"].ToString();

                if (extra.sc(type, "load_page_plugin"))
                    return mtkPluginFactory.CreateLoadPlugin(parameters).Handle(mtkPluginFactory.GetParameters(parameters), out error);

                if (extra.sc(type, "links_gather_plugin"))
                    return mtkPluginFactory.GetMinePlugin().Handle(mtkPluginFactory.GetParameters(parameters), out error);
            }
            catch (Exception exp)
            {
                error = $"{exp.Message}\n{exp.StackTrace}";
            }

            return null;
        }

        public void Init()
        {
        }

        public void Destroy()
        {
        }

        public string Name => "Mtk";

        public string Description => "Парсер http://www.mtk.ru/";
    }
}
