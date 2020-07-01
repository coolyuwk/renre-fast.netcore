using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RenRen.Domain.Common.Extension
{
    public static class JsonExtension
    {
        public static string ToJson(this object obj, bool ignoreDefault = false)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = ignoreDefault ? DefaultValueHandling.Ignore : DefaultValueHandling.Include
            });
        }

        public static T ToObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
