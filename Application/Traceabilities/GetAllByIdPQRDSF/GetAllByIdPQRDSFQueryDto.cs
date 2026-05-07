namespace Application.Traceabilities.GetAllByIdPQRDSF;

public record GetAllByIdPQRDSFQueryDto(Guid Id) : IRequest<ErrorOr<IReadOnlyList<GetAllByIdPQRDSFResponseDto>>>;
