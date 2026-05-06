using Application.Clients.CreatePQRSDF;
using Application.Clients.GetPQRSDF;

namespace API.Controllers;

[Route("api/[controller]")]
public class PQRSDFController(ISender mediator) : ApiController
{
  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Create(
    [FromBody] CreatePQRSDFCommandDto command,
    CancellationToken cancellationToken
  )
  {
    var result = await mediator.Send(command, cancellationToken);

    return result.Match(
      response => Created(string.Empty, response),
      Problem
    );
  }

  [HttpGet("filed/{filedNumber}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Get(string filedNumber, CancellationToken cancellationToken)
  {
    var command = new GetPQRSDFQueryDto { FiledNumber = filedNumber };
    var result = await mediator.Send(command, cancellationToken);

    return result.Match(
      Ok,
      Problem
    );
  }
}