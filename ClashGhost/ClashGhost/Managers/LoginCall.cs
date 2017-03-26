using ClashGhost.Models;
using ClashGhost.Pages;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClashGhost.Managers
{
    class LoginCall
    {
        private static readonly string URI = Constants.LOGIN;
        internal static async Task<CommonResponse> LogInsideAsync(RegisterData data)
        {
            var http = new HttpClient();

            var parameters = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("user", data.UID),
                                                               new KeyValuePair<string, string>("pass", data.Password)});            

            var response = await http.PostAsync(URI, parameters);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(CommonResponse));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));

            var datax = (CommonResponse)serializer.ReadObject(ms);

            return datax;
        }
    }
}
