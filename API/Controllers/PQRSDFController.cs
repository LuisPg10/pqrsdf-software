using Application.Clients.CreatePQRSDF;
using Application.Clients.GetPQRSDF;
using Application.Users.AssignPQRSDF;
using Application.Users.ChangePQRSDFState;
using Application.Users.GetPQRSDFDetails;
using Domain.Entities.Solicitudes;
using Mapster;

namespace API.Controllers;

[Authorize(Roles = "Admin,Functionary")]
[Route("api/[controller]")]
public class PQRSDFController(ISender mediator) : ApiController
{
  [AllowAnonymous]
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

  [AllowAnonymous]
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

  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetDetails(string id, CancellationToken cancellationToken)
  {
    var command = new GetPQRSDFDetailsQueryDto { Id = Guid.Parse(id) };
    var result = await mediator.Send(command, cancellationToken);

    return result.Match(
      Ok,
      Problem
    );
  }

  [HttpPatch("{id}/assign")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Assign([FromQuery] string userId, string id, CancellationToken cancellationToken)
  {
    var command = new AssignPQRSDFCommandDto() { SolicitudeId = Guid.Parse(id), FunctionaryId = Guid.Parse(userId) };
    var result = await mediator.Send(command, cancellationToken);

    return result.Match(
      _ => NoContent(),
      Problem
    );
  }

  [HttpPatch("{id}/status")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> ChangeState(string id, [FromQuery] int newStatus,
    CancellationToken cancellationToken)
  {
    var command = new ChangePQRSDFStateCommandDto
    {
      Id = Guid.Parse(id),
      NewStatus = newStatus
    };

    var result = await mediator.Send(command, cancellationToken);

    return result.Match(
      _ => NoContent(),
      Problem
    );
  }
}