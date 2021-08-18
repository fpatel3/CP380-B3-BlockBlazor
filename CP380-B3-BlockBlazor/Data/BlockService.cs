using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace CP380_B3_BlockBlazor.Data
{
    public class BlockService
    {
        // TODO: Add variables for the dependency-injected resources
        //       - httpClient
        //       - configuration
        //
        static HttpClient _httpClient;
        private readonly IConfiguration _config;

       

        //
        // TODO: Add a constructor with IConfiguration and IHttpClientFactory arguments
        //

         public BlockService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _config = configuration.GetSection("BlockService");
        }

        //
        // TODO: Add an async method that returns an IEnumerable<Block> (list of Blocks)
        //       from the web service
        //
         public async Task<IEnumerable<Block>> GetBlock()
        {
            var responce = await _httpClient.GetAsync(_config["url"]);

            if (responce.IsSuccessStatusCode)
            {
                JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
                return await JsonSerializer.DeserializeAsync<IEnumerable<Block>>(
                        await responce.Content.ReadAsStreamAsync() , options
                    );
            }

            return Array.Empty<Block>();
        }

    
    }
}
