using Application.Auth.Login;
using Application.Auth.Register;

namespace API.Controllers;

[Route("api/[controller]")]
public class AuthController(ISender mediator) : ApiController
{
  [HttpPost("login")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Login([FromBody] LoginQueryDto request, CancellationToken cancellationToken)
  {
    var result = await mediator.Send(request, cancellationToken);

    return result.Match(
      Ok,
      Problem
    );
  }

  [HttpPost("register")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Register([FromBody] RegisterCommandDto request, CancellationToken cancellationToken)
  {
    var result = await mediator.Send(request, cancellationToken);

    return result.Match(
      Ok,
      Problem
    );
  }
}