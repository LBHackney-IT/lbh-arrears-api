using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.UseCase.Interfaces;
using Hackney.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ArrearsApi.V1.Controllers
{
    [ApiController]
    //TODO: Rename to match the APIs endpoint
    [Route("api/v1/test")]
    [Produces("application/json")]
    [ApiVersion("1.0")]

    public class TestController : BaseController
    {
        private readonly IGetAllBatchLogUseCase _getAllBatchLogUseCase;
        private readonly IGetAllEvictionsUseCase _getAllEvictionsUseCase;
        private readonly IGetBatchLogByIdUseCase _getBatchLogByIdUseCase;
        private readonly IGetEvictionsByIdUseCase _getEvictionsByIdUseCase;
        public TestController(IGetAllBatchLogUseCase getAllBatchLogUseCase,
            IGetAllEvictionsUseCase getAllEvictionsUseCase,
            IGetBatchLogByIdUseCase getBatchLogByIdUseCase,
            IGetEvictionsByIdUseCase getEvictionsByIdUseCase)
        {
            _getAllBatchLogUseCase = getAllBatchLogUseCase;
            _getAllEvictionsUseCase = getAllEvictionsUseCase;
            _getBatchLogByIdUseCase = getBatchLogByIdUseCase;
            _getEvictionsByIdUseCase = getEvictionsByIdUseCase;
        }

        /// <summary>
        /// Get a list of BatchLog models
        /// </summary>
        /// <response code="200">Success. BatchLog models was received successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">BathLog cannot be found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(List<BatchLogResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("batchlog")]
        public async Task<IActionResult> GetAllBatchLogAsync()
        {
            var batchLogs = await _getAllBatchLogUseCase.ExecuteAsync().ConfigureAwait(false);

            if (batchLogs == null)
                return NotFound();

            return Ok(batchLogs);
        }

        /// <summary>
        /// Get a list of Evictions models
        /// </summary>
        /// <response code="200">Success. Evictions models was received successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Evictions cannot be found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(List<EvictionsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("evictions")]
        public async Task<IActionResult> GetAllEvictionsAsync()
        {
            var evictions = await _getAllEvictionsUseCase.ExecuteAsync().ConfigureAwait(false);

            if (evictions == null)
                return NotFound();

            return Ok(evictions);
        }

        /// <summary>
        /// Get an BatchLog model by provided id.
        /// </summary>
        /// <param name="id">The value by which we are looking for batchlog</param>
        /// <response code="200">Success. BatchLog model was received successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">BatchLog with provided id cannot be found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(BatchLogResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("batchlog/{id}")]
        public async Task<IActionResult> GetBatchLogByIdAsync([FromRoute] long id)
        {
            if (id == 0)
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, "The id cannot be 0!"));

            var batchLog = await _getBatchLogByIdUseCase.ExecuteAsync(id).ConfigureAwait(false);

            if (batchLog == null)
                return NotFound(new BaseErrorResponse((int) HttpStatusCode.NotFound, "The batchLog by provided id not found!"));

            return Ok(batchLog);
        }

        /// <summary>
        /// Get an evictions model by provided id.
        /// </summary>
        /// <param name="id">The value by which we are looking for evictions</param>
        /// <response code="200">Success. Evictions model was received successfully</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Evictions with provided id cannot be found</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesResponseType(typeof(EvictionsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("evictions/{id}")]
        public async Task<IActionResult> GetEvictionsByIdAsync([FromRoute] int id)
        {
            if (id == 0)
                return BadRequest(new BaseErrorResponse((int) HttpStatusCode.BadRequest, "The id cannot be 0!"));

            var evictions = await _getEvictionsByIdUseCase.ExecuteAsync(id).ConfigureAwait(false);

            if (evictions == null)
                return NotFound(new BaseErrorResponse((int) HttpStatusCode.NotFound, "The evictions by provided id not found!"));

            return Ok(evictions);
        }
    }
}
