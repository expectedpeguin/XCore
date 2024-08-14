using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CitizenFX.Core;
using Newtonsoft.Json.Linq;

namespace XCore.Server.Utilities
{
    public abstract class VersionChecker
    {
        private const string GithubUser = "expectedpeguin";
        private const string GithubRepo = "XCore";
        public static async Task<string> CheckLatestVersion() 
        {
            var apiUrl = $"https://api.github.com/repos/{GithubUser}/{GithubRepo}/releases/latest";
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("XCore");
                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(responseBody);
                var version = json["tag_name"]?.ToString();
                return version;
            }
        }
        public static string CheckCurrentVersion() 
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly.GetName().Version;
            return version.ToString();
        }
        public static async Task<string> CompareVersions()
        {
            return CheckCurrentVersion() != await CheckLatestVersion() ? $"{ConsoleUtility.GetColorCode(ConsoleColor.Red)}You need an update, latest version is v{await CheckLatestVersion()} but you have v{CheckCurrentVersion()}\x1B[0m" : "";
        }
    }
}