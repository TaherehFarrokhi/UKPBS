using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace UKPBS.Services.UnitTests.Helpers
{
    public static class SerializationHelper
    {
        public static string SerializeToJson(object value)
        {
            if (value == null)
                return null;

            var stringBuilder = new StringBuilder();
            var stringWriter = new StringWriter(stringBuilder);
            var jsonWriter = new JsonTextWriter(stringWriter);

            var serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, value);

            return stringBuilder.ToString();
        }
    }
}
