using Domain.Repositories;
namespace Application.Traceabilities.GetAllByIdPQRDSF;

public class GetAllByIdPQRDSFQueryHandler(
    ISolicitudeRepository solicitudeRepository)
    : IRequestHandler<GetAllByIdPQRDSFQueryDto, ErrorOr<IReadOnlyList<GetAllByIdPQRDSFResponseDto>>>
{
    public async Task<ErrorOr<IReadOnlyList<GetAllByIdPQRDSFResponseDto>>> Handle(
        GetAllByIdPQRDSFQueryDto request,
        CancellationToken cancellationToken)
    {
        var solicitude = await solicitudeRepository.ListById(request.Id);

        if (solicitude == null)
            return Error.NotFound("PQRSDF.NotFound", "PQRSDF not found");

        var traces = solicitude.Traceabilities
            .OrderBy(t => t.CreationDate)
            .Select(t => new GetAllByIdPQRDSFResponseDto(
                t.Id,
                t.Action,
                t.Description,
                t.LastStatus,
                t.NewStatus,
                t.CreationDate,
                t.User
            ))
            .ToList();

        return traces;
    }
}