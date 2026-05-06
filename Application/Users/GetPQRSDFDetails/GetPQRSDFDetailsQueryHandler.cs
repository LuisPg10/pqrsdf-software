using Domain.Repositories;

namespace Application.Users.GetPQRSDFDetails;

public class GetPQRSDFDetailsQueryHandler(ISolicitudeRepository solicitudRepository)
  : IRequestHandler<GetPQRSDFDetailsQueryDto, ErrorOr<DetailsSolicitudeDto>>
{
  public async Task<ErrorOr<DetailsSolicitudeDto>> Handle(GetPQRSDFDetailsQueryDto request,
    CancellationToken cancellationToken)
  {
    var solicitude = await solicitudRepository.ListById(request.Id);
    if (solicitude == null) return Error.NotFound("Solicitude.NotFound", "Solicitude Not found.");

    return solicitude.Adapt<DetailsSolicitudeDto>();
  }
}