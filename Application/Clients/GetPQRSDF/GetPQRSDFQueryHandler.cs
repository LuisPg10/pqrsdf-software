using Application.Shared.Dtos;
using Domain.Repositories;

namespace Application.Clients.GetPQRSDF;

public class GetPQRSDFQueryHandler(ISolicitudeRepository solicitudeRepository)
  : IRequestHandler<GetPQRSDFQueryDto, ErrorOr<PQRSDFResponseDto>>
{
  public async Task<ErrorOr<PQRSDFResponseDto>> Handle(GetPQRSDFQueryDto request, CancellationToken cancellationToken)
  {
    var solicitude = await solicitudeRepository.GetSolicitudeByFiledNumber(request.FiledNumber);
    if (solicitude == null) return Error.NotFound("PQRSDF.NotFound", "PQRSDF not found");

    return new PQRSDFResponseDto
    {
      CurrentStatus = solicitude.Status,
      DateSolicitude = solicitude.DateSolicitude,
      ExpirationDate = DateTime.Now.AddDays(30),
      Days = 20,
      HasResponses = solicitude.Responses.Count != 0,
      Responses = solicitude.Responses.Select(response => response.Adapt<SolicitudeRespondeDto>()).ToList()
    };
  }
}