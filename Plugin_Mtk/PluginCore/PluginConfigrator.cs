using System.Dynamic;
using System.Reflection;

namespace PluginCore
{
    public static class PluginConfigrator
    {
        private static object wLock = new object();
        private static int wCounter = 0;
        
        public static void UnsafeHeaderParsingOn()
        {
            lock (wLock)
            {
                if (wCounter <= 0)
                    SetUnsafeHeaderParsing(true);
                wCounter++;
            }
        }

        public static void UnsafeHeaderParsingOff()
        {
            lock (wLock)
            {
                wCounter--;
                if (wCounter <= 0)
                    SetUnsafeHeaderParsing(false);
            }
        }

        /// <summary>
        /// Способ выставить через Reflection настройку, определяемую в app.config
        ///       <httpWebRequest useUnsafeHeaderParsing = "true" />
        /// т. к. app.config приложения, куда загружается плагин, недоступен
        /// </summary>
        private static void SetUnsafeHeaderParsing(bool value)
        {
            var configSettingsType = typeof(System.Net.Configuration.SettingsSection);
            var settingsType = Assembly.GetAssembly(configSettingsType)?.GetType($"{configSettingsType.FullName}Internal");

            var sectionProperty = settingsType?.InvokeMember("Section", 
                                                             BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.NonPublic, 
                                                             null, null, new object[] { });

            var useUnsafeHeaderParsing = settingsType?.GetField("useUnsafeHeaderParsing",
                                                                BindingFlags.NonPublic | BindingFlags.Instance);
            useUnsafeHeaderParsing?.SetValue(sectionProperty, value);
        }
    }
}