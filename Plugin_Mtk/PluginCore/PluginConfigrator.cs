using System.Reflection;

namespace PluginCore
{
    public class PluginConfigrator
    {
        public static bool UnsafeHeaderParsingOn()
        {
            return SetUnsafeHeaderParsingBase(true);
        }

        public static bool UnsafeHeaderParsingOff()
        {
            return SetUnsafeHeaderParsingBase(false);
        }

        /// <summary>
        /// Способ выставить через Reflection настройку, определяемую в app.config
        ///       <httpWebRequest useUnsafeHeaderParsing = "true" />
        /// т. к. app.config приложения, куда загружается плагин, недостапун
        /// </summary>
        private static bool SetUnsafeHeaderParsingBase(bool value)
        {
            var configSettingsType = typeof(System.Net.Configuration.SettingsSection);
            var settingsType = Assembly.GetAssembly(configSettingsType)?.GetType($"{configSettingsType.FullName}Internal");

            object sectionProperty = settingsType?.InvokeMember("Section", 
                                                                BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.NonPublic, 
                                                                null, null, new object[] { });

            FieldInfo useUnsafeHeaderParsing = settingsType?.GetField("useUnsafeHeaderParsing",
                                                                     BindingFlags.NonPublic | BindingFlags.Instance);
            useUnsafeHeaderParsing?.SetValue(sectionProperty, value);
            return useUnsafeHeaderParsing != null;
        }
    }
}