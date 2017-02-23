using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();
        public static async Task MainAsync()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            #region clientCredential
            // request token. To create an instance you need to pass in the token endpoint address, client id and secret.
            //var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");

            //use the RequestClientCredentialsAsync method to request a token for your API
            //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");
            #endregion

            #region ResoureceOwner
            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password1", "api1");
            
            #endregion

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/identity");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
