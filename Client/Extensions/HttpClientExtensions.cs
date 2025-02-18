

using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Options;
using System.IO;
using BH.Tool;
using System;
using System.Text.Json;
using System.Net.Http.Json;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace BH.Tool.RoomConfigurator.Client.Extensions
{
    public static class HttpClientExtensions
    {
        // This allows you to use the BHoM serialiser instead of the default Json serialiser used by Blazor.
        // Make sure that the corresponding controller on the server is also sending the data using the BHoM serialiser
        public static async Task<TValue> GetFromBHoMAsync<TValue>(this HttpClient client, string? requestUri, CancellationToken cancellationToken = default)
        {
            try
            {
                Task<HttpResponseMessage> taskResponse = client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

                using (HttpResponseMessage response = await taskResponse.ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    string json = await response.Content.ReadAsStringAsync(cancellationToken);

                    return BH.Tool.RoomConfigurator.Engine.Convert.FromJson<TValue>(json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default(TValue);
            }
        }

        /***************************************************/

        public static async Task<List<TValue>> GetFromBHoMListAsync<TValue>(this HttpClient client, string? requestUri, CancellationToken cancellationToken = default)
        {
            try
            {
                Task<HttpResponseMessage> taskResponse = client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

                using (HttpResponseMessage response = await taskResponse.ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    string json = await response.Content.ReadAsStringAsync(cancellationToken);

                    return BH.Tool.RoomConfigurator.Engine.Convert.FromJsonArray<TValue>(json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<TValue>();
            }
        }

        /***************************************************/

        public static async Task<HttpResponseMessage> PostAsBHoMAsync<TValue>(this HttpClient client, string? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            string json = BH.Tool.RoomConfigurator.Engine.Convert.ToJson(value);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/bhom");
            return await client.PostAsync(requestUri, content, cancellationToken);
        }
    }
}
