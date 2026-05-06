using Domain.Entities.Solicitudes;
using Domain.Events;

namespace Domain.Entities.Traceabilities;

public class Traceability
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Action { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public SolicitudeStatusEnum? LastStatus { get; private set; }
    public SolicitudeStatusEnum? NewStatus { get; private set; }
    public DateTime CreationDate { get; private set; } = DateTime.UtcNow;
    public string User { get; private set; } = string.Empty;

    public Guid SolicitudeId { get; private set; }

    [ForeignKey("SolicitudeId")]
    public Solicitude? Solicitude { get; set; }

    private Traceability() { }

    public static Traceability? CreateFromDomainEvent(dynamic domainEvent, Guid solicitudeId)
    {
        var trace = new Traceability
        {
            Id = Guid.NewGuid(),
            SolicitudeId = solicitudeId,
            CreationDate = DateTime.UtcNow
        };

        switch (domainEvent)
        {
            case SolicitudeCreatedEvent e:
                trace.Action = "Creation";
                trace.Description = $"Solicitude created with filed number: {e.FiledNumber}";
                trace.User = e.User;
                trace.NewStatus = e.InitialStatus;
                break;

            case SolicitudeStatusChangedEvent e:
                trace.Action = "Status Change";
                trace.Description = $"Status changed from {e.OldStatus} to {e.NewStatus}";
                if (!string.IsNullOrEmpty(e.Justification))
                    trace.Description += $". Justification: {e.Justification}";
                trace.User = e.User;
                trace.LastStatus = e.OldStatus;
                trace.NewStatus = e.NewStatus;
                break;

            case SolicitudeAssignedEvent e:
                trace.Action = "Assignment";
                trace.Description = $"Assigned to user: {e.AssignedToUserName} (ID: {e.AssignedToUserId})";
                trace.User = e.AssignedBy;
                break;

            case SolicitudeResponseAddedEvent e:
                trace.Action = "Response";
                trace.Description = $"Response added: {(e.ResponseContent.Length > 100 ? e.ResponseContent[..100] : e.ResponseContent)}...";
                trace.User = e.User;
                trace.NewStatus = e.NewStatus;
                break;

            default:
                return null;
        }

        return trace;
    }
}