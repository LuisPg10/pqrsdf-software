using Application.Clients.CreatePQRSDF;

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
}