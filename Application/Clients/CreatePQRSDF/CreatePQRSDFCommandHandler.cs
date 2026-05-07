using Application.Utilities.CurrentUsers;
using Application.Utilities.Holidays;
using Application.Utilities.IFiledGenerators;
using Domain.Entities.Clients;
using Domain.Entities.Solicitudes;
using Domain.Primitives;
using Domain.Repositories;

namespace Application.Clients.CreatePQRSDF;

public class CreatePQRSDFCommandHandler(
  ISolicitudeRepository solicitudeRepository,
  IClientRepository clientRepository,
  IAreaRepository areaRepository,
  ISolicitudeTypeRepository solicitudeTypeRepository,
  IFiledGenerator filedGenerator,
  IHolidayService holidayService,
  ICurrentUserService currentUserService,
  IUnitOfWork unitOfWork
)
  : IRequestHandler<CreatePQRSDFCommandDto, ErrorOr<SolicitudeResponseDto>>
{
  public async Task<ErrorOr<SolicitudeResponseDto>> Handle(CreatePQRSDFCommandDto request,
    CancellationToken cancellationToken)
  {
    var area = await areaRepository.ListById(request.AreaId);
    if (area == null) return Error.NotFound("AreaId.NotFound", "The specified area was not found");

    var solicitudeType = await solicitudeTypeRepository.ListById(request.TypeId);
    if (solicitudeType == null)
    {
      return Error.NotFound("SolicitudeType.NotFound", "The specified solicitude type was not found");
    }

    var existingClient = await clientRepository.GetByEmail(request.Client.Email);
    if (existingClient == null)
    {
      existingClient = request.Client.Adapt<Client>();
      clientRepository.Create(existingClient);
    }

    var filedNumber = await filedGenerator.GenerateFiledAsync(cancellationToken);
    if (string.IsNullOrEmpty(filedNumber)) Error.Failure("Filed.GenerationFailed", "Failed to generate filed number");

    var dueDate = holidayService.AddBusinessDays(DateTime.Now.Date, solicitudeType.Time);
    var createdBy = currentUserService.GetCurrentUserName();

    var solicitude = Solicitude.Create(
      request.Subject,
      request.Description,
      area.Id,
      existingClient.Id,
      solicitudeType.Id,
      filedNumber,
      dueDate,
      createdBy
    );

    solicitudeRepository.Create(solicitude);
    await unitOfWork.SaveChangesAsync(cancellationToken);

    return new SolicitudeResponseDto
    {
      FiledNumber = solicitude.FiledNumber,
      DueDate = dueDate,
      Message = "PQRSDF successfully created",
    };
  }
}