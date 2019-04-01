using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Charp_2_Db.Models;
using Newtonsoft.Json;

namespace Charp_2_Db.Helpers
{
    public static class WebDbController
    {
        private const string BaseUrl = "http://localhost:54512/";

        public static T GetAsync<T>(string query, string filter = null)
        {
            if (filter != null)
            {
                query += $"?$filter={filter}";
            }

            using (var client = new HttpClient())
            {
                var result = client.GetStringAsync($"{BaseUrl}{query}").Result;
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        public static T PostAsync<T>(T item)
        {
            var json = JsonConvert.SerializeObject(item);

            using (var client = new HttpClient())
            {
                var response = client.PostAsync($"{BaseUrl}{typeof(T).Name}s",
                    new StringContent(json, Encoding.UTF8, "application/json")).Result;

                var result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
        }

        public static HttpResponseMessage PatchAsync<T>(string query, T item)
        {
            var json = JsonConvert.SerializeObject(item);

            using (var client = new HttpClient())
            {
                return client.SendAsync(new HttpRequestMessage(new HttpMethod("Patch"),
                    $"{BaseUrl}{query}")
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                }).Result;
            }
        }

        public static void PatchAsync<T>(IEnumerable<T> items) where T : BaseModel
        {
            using (var client = new HttpClient())
            {
                foreach (var item in items)
                {
                    var json = JsonConvert.SerializeObject(item);

                    var result = client.SendAsync(new HttpRequestMessage(new HttpMethod("Patch"),
                        $"{BaseUrl}{typeof(T).Name}s?key={item.Id}")
                    {
                        Content = new StringContent(json, Encoding.UTF8, "application/json")
                    }).Result;
                }
            }
        }

        public static HttpResponseMessage DeleteAsync(string query)
        {
            using (var client = new HttpClient())
            {
                return client.SendAsync(new HttpRequestMessage(new HttpMethod("Delete"),
                    $"{BaseUrl}{query}")).Result;
            }
        }
    }
}