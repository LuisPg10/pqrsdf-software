namespace Application.Users.AssignPQRSDF;

public class AssignPQRSDFCommandDto : IRequest<ErrorOr<Unit>>
{
  [Required] public Guid FunctionaryId { get; set; }
  [Required] public Guid SolicitudeId { get; set; }
}