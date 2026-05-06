using Domain.Entities.Clients;
using Domain.Entities.Solicitudes;
using Domain.Primitives;
using Domain.Repositories;

namespace Application.Clients.CreatePQRSDF;

public class CreatePQRSDFCommandHandler(
  ISolicitudeRepository solicitudeRepository,
  IClientRepository clientRepository,
  IUnitOfWork unitOfWork)
  : IRequestHandler<CreatePQRSDFCommandDto, Unit>
{
  public async Task<Unit> Handle(CreatePQRSDFCommandDto solicitudeDto, CancellationToken cancellationToken)
  {
    var client = solicitudeDto.Client.Adapt<Client>();
    clientRepository.Create(client);

    var solicitude = solicitudeDto.Adapt<Solicitude>();
    solicitudeRepository.Create(solicitude);

    await unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}