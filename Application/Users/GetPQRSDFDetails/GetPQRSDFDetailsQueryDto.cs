namespace Application.Users.GetPQRSDFDetails;

public class GetPQRSDFDetailsQueryDto : IRequest<ErrorOr<DetailsSolicitudeDto>>
{
  [Required] public Guid Id { get; set; }
}