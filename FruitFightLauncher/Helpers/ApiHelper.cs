using FruitFightLauncher.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FruitFightLauncher.Helpers
{
    public class ApiHelper
    {
        private const string releasesUrl = "https://api.github.com/repos/adamtovatt/fruitfight/releases";
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly WebClient webClient = new WebClient();

        public static async Task<string> GetAsync(string uri)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            HttpRequestMessage request = new HttpRequestMessage()
            {
                Headers = { { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:96.0) Gecko/20100101 Firefox/96.0" }, { HttpRequestHeader.Accept.ToString(), "application/json" } },
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri)
            };

            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public static async Task<List<Release>> GetReleasesAsync()
        {
            string json = await GetAsync(releasesUrl);
            return JsonConvert.DeserializeObject<List<Release>>(json);
        }

        public async static Task<string> DownloadFileAsync(Uri fileUri, string savePath)
        {
            await webClient.DownloadFileTaskAsync(fileUri, savePath);
            return savePath;
        }
    }
}
