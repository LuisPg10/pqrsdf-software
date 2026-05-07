using Application.Utilities.CurrentUsers;
using Domain.Entities.SolicitudeResponses;
using Domain.Entities.Solicitudes;
using Domain.Primitives;
using Domain.Repositories;

namespace Application.Users.ChangePQRSDFState;

public class ChangePQRSDFStateCommandHandler(
  ISolicitudeRepository solicitudeRepository,
  ICurrentUserService currentUserService,
  IUnitOfWork unitOfWork
) : IRequestHandler<ChangePQRSDFStateCommandDto, ErrorOr<Unit>>
{
  public async Task<ErrorOr<Unit>> Handle(ChangePQRSDFStateCommandDto request,
    CancellationToken cancellationToken)
  {
    var solicitude = await solicitudeRepository.ListById(request.Id);
    if (solicitude == null) return Error.NotFound("Solicituded.NotFound", "Solitude not found");

    if (solicitude.Status == SolicitudeStatusEnum.Completed)
    {
      return Error.Failure("Solicituded.Failure", "Cannot change status of a completed solicitude");
    }


    var currentUser = currentUserService.GetCurrentUserName();

    solicitude.ChangeStatus(request.NewStatus, currentUser, request.Justification);

    solicitudeRepository.Update(solicitude);
    await unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}