using Domain.Entities.Solicitudes;

namespace Application.Traceabilities.GetAllByIdPQRDSF;

public record GetAllByIdPQRDSFResponseDto(
    Guid Id,
    string Action,
    string Description,
    SolicitudeStatusEnum? LastStatus,
    SolicitudeStatusEnum? NewStatus,
    DateTime CreationDate,
    string User
);
