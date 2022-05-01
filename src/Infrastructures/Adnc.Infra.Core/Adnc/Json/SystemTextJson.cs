using Adnc.Infra.Core.System.Extensions.DataTimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Adnc.Infra.Core.Adnc.Json
{
    public static class SystemTextJson
    {
        public static JsonSerializerOptions GetAdncDefaultOptions()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateTimeConverter());
            options.Converters.Add(new DateTimeNullableConverter());
            options.Encoder = GetAdncDefaultEncoder();
            //该值指示是否允许、不允许或跳过注释。
            options.ReadCommentHandling = JsonCommentHandling.Skip;
            //dynamic与匿名类型序列化设置
            options.PropertyNameCaseInsensitive = true;
            //dynamic
            options.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            //匿名类型
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            return options;
        }

        public static JavaScriptEncoder GetAdncDefaultEncoder() => JavaScriptEncoder.Create(new TextEncoderSettings(UnicodeRanges.All));
    }
}
