using System.Collections.Generic;

namespace Plugin_Mtk
{
    public static class ParametersExtension
    {
        public static object SafeGet(this Dictionary<string, object> parameters, string key, object defaultValue = null)
        {
            object value;
            return parameters.TryGetValue(key, out value) ? value : defaultValue;
        }
    }
}
