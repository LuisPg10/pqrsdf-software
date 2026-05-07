using Domain.Entities.Solicitudes;
using Domain.Primitives;
using Domain.Repositories;

namespace Application.SolicitudeTypes.CreateType;

public class CreateSolicitudeTypeCommandHandler(
  ISolicitudeTypeRepository solicitudeTypeRepository,
  IUnitOfWork unitOfWork)
  : IRequestHandler<CreateTypeSolicitudeCommandDto, ErrorOr<CreateSolicitudeTypeResponseDto>>
{
  public async Task<ErrorOr<CreateSolicitudeTypeResponseDto>> Handle(CreateTypeSolicitudeCommandDto request,
    CancellationToken cancellationToken)
  {
    var alreadyExists = await solicitudeTypeRepository.ExistsByNameAsync(request.Name, cancellationToken);
    if (alreadyExists)
      return Error.Validation("SolicitudeType.AlreadyExists", "A solicitude type with this name already exists");

    var solicitudeType = request.Adapt<SolicitudeType>();

    solicitudeTypeRepository.Create(solicitudeType);
    await unitOfWork.SaveChangesAsync(cancellationToken);

    return new CreateSolicitudeTypeResponseDto("SolicitudeType created successfully");
  }
}