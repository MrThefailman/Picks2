using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Picks.infrastructure.Extensions
{
    public static class SessionExtensions
    {
        public static async Task Set<T>(this ISession session, string key, T value)
        {
            if (!session.IsAvailable)
            {
                await session.LoadAsync();
            }
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static async Task<T> Get<T>(this ISession session, string key)
        {
            if (session.IsAvailable)
            {
                await session.LoadAsync();
            }
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
