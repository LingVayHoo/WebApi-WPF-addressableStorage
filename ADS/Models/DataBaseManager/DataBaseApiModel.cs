using Azure.Core;
using DataBase;
using DataBase.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ADS.Models.DataBaseManager
{
    public class DataBaseApiModel : IDataBase
    {
        private HttpClient Http_Client { get; set; }

        public DataBaseApiModel()
        {
            Http_Client = new HttpClient();

        }

        public IEnumerable<AddressDBModel>? GetAllContent()
        {
            string url = @"https://localhost:44387/api/AddressDBModels";
            string json = Http_Client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<AddressDBModel>>(json);
        }

        public IEnumerable<AddressDBModel>? GetContentByArticle(string article)
        {
            var all = GetAllContent();
            List<AddressDBModel> result = [];
            foreach (var e in all)
            {
                if (e.Article == article)
                    result.Add(e);
            }
            return result;
        }

        public async Task<string> GetDataFromMyWarehouseByArt(string article)
        {
            string url = $"https://api.moysklad.ru/api/remap/1.2/entity/assortment?filter=article~{article}";
            return await GetDataFromMyWarehouse(article, url);
        }

        public async Task<string> GetDataFromMyWarehouseByName(string article)
        {
            string url = $"https://api.moysklad.ru/api/remap/1.2/entity/assortment?filter=name~{article}";
            return await GetDataFromMyWarehouse(article, url);
        }

        public async Task<string> GetDataFromMyWarehouse(string article, string targetUrl)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            HttpClient client = new(handler);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "32811361d1b7369778cf699223fe8cccd16bcfb6");
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            string url = targetUrl;
            var r = client.GetAsync(url).Result;
            string content = r.Content.ReadAsStringAsync().Result;
            //var json = JObject.Parse(content);
            return content;
        }

        public async Task<bool> PutAddressDBModel(Guid id, AddressDBModel addressDBModel)
        {
            string url = $"https://localhost:44387/api/AddressDBModels/{id}";
            var r = await Http_Client.PutAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(addressDBModel),
                Encoding.UTF8,
                mediaType: "application/json")
                );

            if (r.StatusCode == HttpStatusCode.OK) 
            { 
                return true; 
            }
            return false;
        }

        public async Task<bool> PostAddressDBModel(AddressDBModel addressDBModel)
        {
            string url = $"https://localhost:44387/api/AddressDBModels";
            var r = await Http_Client.PostAsync(
            requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(addressDBModel),
                Encoding.UTF8,
                mediaType: "application/json")
                );

            if (r.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAddressDBModel(Guid id)
        {
            string url = $"https://localhost:44387/api/AddressDBModels/{id}";

            var r = await Http_Client.DeleteAsync(requestUri: url);

            if (r.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
    }
}
