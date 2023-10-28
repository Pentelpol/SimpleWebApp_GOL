using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace SimpleWebApp.Controllers.API
{
    /// <summary>
    /// API controller for JSON data calls.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DataClientController : ControllerBase
    {
        HttpClient client;
        ILogger<DataClientController> _logger;
        public DataClientController(ILogger<DataClientController> logger) 
        {
            _logger = logger;

            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7015");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpPost("getNextGeneratioDataFromData")]
        public async Task<ActionResult<int[][]?>?> GetNextGeneratioDataFromData([FromBody] int[][] data, int col, int row)
        {
            var uri = "api/Data/MakeNextGenFromData";
            var url = uri + "?col=" + col + "&row=" + row;
            var info = JsonConvert.SerializeObject(data);
            var body = new StringContent(info, Encoding.UTF8, "application/json");
            using (var resource = await client.PostAsync(url, body))
            {
                resource.EnsureSuccessStatusCode();
                try
                {
                    var result = await resource.Content.ReadFromJsonAsync<int[][]>(); ;
                    return result;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create data");
                    return null;
                }
            }
        }
    }
}
