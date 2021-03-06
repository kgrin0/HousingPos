﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Dalamud.Plugin;

namespace HousingPos.Objects
{
    public class CloudMap
    {
        public static CloudMap Empty => new CloudMap("", "", "", "", "");

        public string Location;
        public string Size;
        public string Name;
        public string Hash;
        public string Tags;
        public CloudMap(string location, string size, string named, string hash, string tags)
        {
            Location = location;
            Size = size;
            Name = named;
            Hash = hash;
            Tags = tags;
        }
    }
    
    public class HttpPost
    {
        public static async Task<string> Post(string Uri,string Location, string Size, string UploadName, string str, string tags, string Uploader)
        {
            HttpClient httpClient = new HttpClient();
            if (str==null)
            {
                return "You Can't Upload An Empty List.";
            }
            var values = new Dictionary<string, string>
            {
                {"Location", Location},
                {"Size", Size },
                {"Named", UploadName },
                {"Items", str },
                {"Tags", tags },
                {"Uper", Uploader }
            };
            HttpContent data = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await httpClient.PostAsync(Uri + "/index.php", data);
            response.EnsureSuccessStatusCode();
            string resultStr = await response.Content.ReadAsStringAsync();
            return resultStr;
        }
        public static async Task<string> GetMap(string Uri)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(Uri + "/map.json");
            response.EnsureSuccessStatusCode();
            string resultStr = await response.Content.ReadAsStringAsync();
            return resultStr;
        }
        public static async Task<string> GetItems(string Uri, string hash)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(Uri + "/result/" + hash + ".json");
            response.EnsureSuccessStatusCode();
            string resultStr = await response.Content.ReadAsStringAsync();
            return resultStr;
        }
    }
}
