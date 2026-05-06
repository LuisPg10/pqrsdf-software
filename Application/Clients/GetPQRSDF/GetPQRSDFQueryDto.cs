namespace Application.Clients.GetPQRSDF;

public record GetPQRSDFQueryDto : IRequest<ErrorOr<PQRSDFResponseDto>>
{
  [Required(ErrorMessage = "FiledNumber is required")]
  public string FiledNumber { get; init; } = string.Empty;
}