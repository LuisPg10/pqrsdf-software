using Domain.Primitives;
using Domain.Entities.Solicitudes;

namespace Domain.Events;

public abstract record SolicitudeDomainEvent(Guid Id) : DomainEvent(Id);

public record SolicitudeCreatedEvent(
    Guid Id,
    Guid SolicitudeId,
    string FiledNumber,
    string User,
    SolicitudeStatusEnum InitialStatus
) : SolicitudeDomainEvent(Id);

public record SolicitudeStatusChangedEvent(
    Guid Id,
    Guid SolicitudeId,
    string User,
    SolicitudeStatusEnum OldStatus,
    SolicitudeStatusEnum NewStatus,
    string? Justification
) : SolicitudeDomainEvent(Id);

public record SolicitudeAssignedEvent(
    Guid Id,
    Guid SolicitudeId,
    string AssignedBy,
    Guid AssignedToUserId,
    string AssignedToUserName
) : SolicitudeDomainEvent(Id);

public record SolicitudeResponseAddedEvent(
    Guid Id,
    Guid SolicitudeId,
    string User,
    string ResponseContent,
    SolicitudeStatusEnum NewStatus
) : SolicitudeDomainEvent(Id);