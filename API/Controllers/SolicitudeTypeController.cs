using Application.SolicitudeTypes.CreateType;
using Application.SolicitudeTypes.GetAll;

namespace API.Controllers
{
    [Authorize(Roles = "Admin,Functionary")]
    [Route("api/[controller]")]
    public class SolicitudeTypeController(ISender mediator) : ApiController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateArea([FromBody] CreateTypeSolicitudeCommandDto request)
        {
            var result = await mediator.Send(request);
            return result.Match(
                _ => CreatedAtAction(string.Empty, new { }),
                errors => Problem(errors));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAreas()
        {
            var result = await mediator.Send(new GetAllSolicitudeTypeQueryDto());
            return result.Match(
                areas => Ok(areas),
                errors => Problem(errors));
        }
    }
}
