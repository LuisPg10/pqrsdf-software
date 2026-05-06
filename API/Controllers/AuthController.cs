using Application.Auth.Login;

namespace API.Controllers;

[Route("api/[controller]")]
public class AuthController(ISender mediator) : ApiController
{
  [HttpPost("login")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Login([FromBody] LoginQueryDto request)
  {
    var result = await mediator.Send(request, CancellationToken.None);

    return result.Match(
      Ok,
      Problem
    );
  }
}