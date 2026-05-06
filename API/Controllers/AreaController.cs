using Application.Areas.Create;
using Application.Areas.GetAll;

namespace API.Controllers
{
  [Authorize(Roles = "Admin,Functionary")]
  [Route("api/[controller]")]
  public class AreaController(ISender mediator) : ApiController
  {
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateArea([FromBody] CreateAreaCommandDto request)
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
      var result = await mediator.Send(new GetAllAreaQueryDto());
      return result.Match(
        areas => Ok(areas),
        errors => Problem(errors));
    }
  }
}