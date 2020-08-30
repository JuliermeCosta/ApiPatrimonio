using System;
using System.Text.Json;

namespace ApiPatrimonio.Data.Extension
{
    public static class JsonExtension
    {
        public static T FromJson<T>(this string json) where T : class
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };

                T objeto = (T)JsonSerializer.Deserialize(json, typeof(T) , options);

                return objeto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
