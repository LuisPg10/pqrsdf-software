using Domain.Entities.SolicitudeResponses;
using Domain.Entities.Solicitudes;
using Domain.Primitives;
using Domain.Repositories;

namespace Application.Users.ChangePQRSDFState;

public class ChangePQRSDFStateCommandHandler(
  ISolicitudeRepository solicitudeRepository,
  IUnitOfWork unitOfWork) : IRequestHandler<ChangePQRSDFStateCommandDto, ErrorOr<Unit>>
{
  public async Task<ErrorOr<Unit>> Handle(ChangePQRSDFStateCommandDto request,
    CancellationToken cancellationToken)
  {
    var solitude = await solicitudeRepository.ListById(request.Id);
    if (solitude == null) return Error.NotFound("Solicituded.NotFound", "Solitude not found");

    solitude.Status = (SolicitudeStatusEnum)request.NewStatus;

    solicitudeRepository.Update(solitude);
    await unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}