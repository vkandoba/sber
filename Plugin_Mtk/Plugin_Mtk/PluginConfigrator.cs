using System;
using System.Reflection;

namespace Plugin_Mtk
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

        private static bool SetUnsafeHeaderParsingBase(bool value)
        {
            var configSettingsType = typeof(System.Net.Configuration.SettingsSection);
            var settingsType = Assembly.GetAssembly(configSettingsType)?.GetType(configSettingsType.Name);

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