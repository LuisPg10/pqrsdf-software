using Domain.Entities.SolicitudeResponses;
using Domain.Primitives;
using Domain.Repositories;

namespace Application.Users.MakePQRSDFResponse;

public class MakePQRSDFResponseCommandHandler(
  ISolicitudeRepository solicitudeRepository,
  ISolicitudeResponseRepository solicitudeResponseRepository,
  IUnitOfWork unitOfWork)
  : IRequestHandler<MakePQRSDFCommandDto, ErrorOr<SolicitudeCreatedResponseDto>>
{
  public async Task<ErrorOr<SolicitudeCreatedResponseDto>> Handle(MakePQRSDFCommandDto request,
    CancellationToken cancellationToken)
  {
    var solicitude = await solicitudeRepository.ListById(request.Id);
    if (solicitude == null) return Error.NotFound("PQRSDF.NotFound", "PQRSDF not found");

    var solicitudeResponse = request.Adapt<SolicitudeResponse>();

    solicitudeResponseRepository.Create(solicitudeResponse);

    await unitOfWork.SaveChangesAsync(cancellationToken);

    return new SolicitudeCreatedResponseDto();
  }
}