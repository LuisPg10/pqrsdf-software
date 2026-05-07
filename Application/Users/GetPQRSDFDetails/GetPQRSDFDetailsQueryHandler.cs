using Domain.Repositories;

namespace Application.Users.GetPQRSDFDetails;

public class GetPQRSDFDetailsQueryHandler(ISolicitudeRepository solicitudeRepository)
  : IRequestHandler<GetPQRSDFDetailsQueryDto, ErrorOr<DetailsSolicitudeDto>>
{
  public async Task<ErrorOr<DetailsSolicitudeDto>> Handle(GetPQRSDFDetailsQueryDto request,
    CancellationToken cancellationToken)
  {
    var solicitude = await solicitudeRepository.ListById(request.Id);
    if (solicitude == null) return Error.NotFound("PQRSDF.NotFound", "PQRSDF not found.");

    return solicitude.Adapt<DetailsSolicitudeDto>();
  }
}