using Microsoft.AspNetCore.Mvc;
using SimpleWebAppBackend.Model;
using SimpleWebAppBackend.Repository;
using System.Collections;

namespace SimpleWebAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataRepository _dataRepositoryFactory;

        public  DataController(IDataRepository dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [HttpPost("MakeNextGenFromData")]
        [ProducesResponseType(typeof(int[][]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> MakeNextGenFromData([FromBody] int[][] input, int col, int row)
        {
            var result = _dataRepositoryFactory.RetriveNextGenerationFromInput(input, col, row);

            return Task.FromResult<IActionResult>(result == null ? BadRequest() : Ok(result));
        }
    }
}
