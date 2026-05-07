using Application.Utilities.CurrentUsers;
using Domain.Entities.SolicitudeResponses;
using Domain.Entities.Solicitudes;
using Domain.Primitives;
using Domain.Repositories;

namespace Application.Users.MakePQRSDFResponse;

public class MakePQRSDFResponseCommandHandler(
  ISolicitudeRepository solicitudeRepository,
  ISolicitudeResponseRepository solicitudeResponseRepository,
  ICurrentUserService currentUserService,
  IUnitOfWork unitOfWork
)
  : IRequestHandler<MakePQRSDFCommandDto, ErrorOr<SolicitudeCreatedResponseDto>>
{
  public async Task<ErrorOr<SolicitudeCreatedResponseDto>> Handle(MakePQRSDFCommandDto request,
    CancellationToken cancellationToken)
  {
    var solicitude = await solicitudeRepository.ListById(request.Id);
    if (solicitude == null) return Error.NotFound("PQRSDF.NotFound", "PQRSDF not found");

    if (!solicitude.UserId.HasValue)
      return Error.Validation("PQRSDF.NotAssigned", "Cannot add response to unassigned solicitude");

    if (solicitude.Status == SolicitudeStatusEnum.Completed)
      return Error.Validation("PQRSDF.AlreadyCompleted", "Solicitude already completed");

    if (solicitude.Status == SolicitudeStatusEnum.Rejected)
      return Error.Validation("PQRSDF.AlreadyRejected", "Solicitude already rejected");

    var currentUser = currentUserService.GetCurrentUserName();
    var currentUserId = currentUserService.GetCurrentUserId();

    if (!currentUserId.HasValue)
      return Error.Unauthorized("User.NotFound", "Current user not found");

    // use rich method in the entity
    solicitude.AddResponse(request.Content, currentUser);

    // make response
    var solicitudeResponse = SolicitudeResponse.Create(
      request.Content,
      solicitude.Id,
      currentUserId.Value
    );


    solicitudeResponseRepository.Create(solicitudeResponse);
    solicitudeRepository.Update(solicitude);

    await unitOfWork.SaveChangesAsync(cancellationToken);

    return new SolicitudeCreatedResponseDto();
  }
}