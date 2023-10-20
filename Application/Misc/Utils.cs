using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace SceletonAPI.Application.Misc
{
    public class Utils
    {
        private readonly IHttpContextAccessor _accessor;

        public Utils(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public static bool IsDevelopment()
        {
            return string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "development", StringComparison.InvariantCultureIgnoreCase);
        }

        public static long DateToTimestamps(DateTime tm)
        {
            return (tm.Ticks - 621355968000000000) / 10000000 * 1000;
        }

        public static string GetFileExtension(string filename)
        {
            return Path.GetExtension(filename).Replace(".", "");
        }

        public static string GenerateFileCode(string prefix, string path)
        {
            var encryptor = SHA256Managed.Create();
            byte[] bytes = encryptor.ComputeHash(Encoding.ASCII.GetBytes(path));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {  
                builder.Append(bytes[i].ToString("x2"));
            }  
            var checkshum = builder.ToString().Substring(0, 6);
            return prefix + "_" + checkshum;
        }

        public static string GetUploadLocation(string prefix, string filename)
        {
            return "/Uploads/" + prefix + "/" + DateTime.Now.Ticks + "_" + filename;
        }

        public static string GetValidTopicName(string str)
        {
            return Regex.Replace(str, "[^A-Za-z0-9]", "").Trim().ToLower();
        }

        public static List<string> GetAllTopics(string pos, string dealerGroup, string dealerCity, string dealerCode, string dealerName, string dealerArea, string teamCategory) 
        {
            List<string> topics = new List<string>();
            List<string> positions = new List<string> { Utils.GetValidTopicName(pos), "all" };
            List<string> groups = new List<string> { Utils.GetValidTopicName(dealerGroup), "all"};
            List<string> cities = new List<string> { Utils.GetValidTopicName(dealerCity), "all"};
            List<string> codes = new List<string> { Utils.GetValidTopicName(dealerCode), "all"};
            List<string> names = new List<string> { Utils.GetValidTopicName(dealerName), "all"};
            List<string> areas = new List<string> { Utils.GetValidTopicName(dealerArea), "all"};
            List<string> teams = new List<string> { Utils.GetValidTopicName(teamCategory), "all"};
            
            for (var i = 0; i < positions.Count; i++) 
            {
                var position = positions[i];
                for (var j = 0; j < groups.Count; j++) {
                    String group = groups[j];
                    for (var k = 0; k < cities.Count; k++) {
                        String city = cities[k];
                        for (var l = 0; l < codes.Count; l++) {
                            String code = codes[l];
                            for (var m = 0; m < names.Count; m++) {
                                String name = names[m];
                                for (var n = 0; n < areas.Count; n++) {
                                    String area = areas[n];
                                    for (var o = 0; o < teams.Count; o++) {
                                        String team = teams[o];
                                        topics.Add(position + "-" + group + "-" + city + "-" + code + "-" + name + "-" + area + "-" + team);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return topics;
        }

        public string GetValidUrl(string path)
        {
            if (this.IsValidUrl(path)) return path;

            var request = _accessor.HttpContext.Request;
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();
            return $"{request.Scheme}://{host}{pathBase}{path}";
        }

        private bool IsValidUrl(string path)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(path, UriKind.Absolute, out uriResult)  
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }
    }
}
