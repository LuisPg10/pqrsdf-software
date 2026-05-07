using Domain.Entities.Areas;
using Domain.Entities.Clients;
using Domain.Entities.SolicitudeResponses;
using Domain.Entities.Traceabilities;
using Domain.Events;
using Domain.Generics;

namespace Domain.Entities.Solicitudes;

public class Solicitude : GenericEntity
{
  [Required] public string Subject { get; private set; } = string.Empty;
  [Required] public string Description { get; private set; } = string.Empty;
  [Required] public DateTime DateSolicitude { get; private set; } = DateTime.UtcNow;
  [Required] public string FiledNumber { get; private set; } = string.Empty;
  [Required] public SolicitudeStatusEnum Status { get; set; }
  [Required] public DateTime DueDate { get; private set; }

  // Foreign keys
  [Required] public Guid AreaId { get; private set; }
  [Required] public Guid ClientId { get; private set; }
  [Required] public Guid TypeId { get; private set; }
  public Guid? UserId { get; set; }

  [ForeignKey("AreaId")] public Area? Area { get; set; }
  [ForeignKey("ClientId")] public Client? Client { get; set; }
  [ForeignKey("TypeId")] public SolicitudeType? Type { get; set; }

  public ICollection<SolicitudeResponse> Responses { get; private set; } = [];
  public ICollection<Traceability> Traceabilities { get; private set; } = [];

  public static Solicitude Create(
    string subject,
    string description,
    Guid areaId,
    Guid clientId,
    Guid typeId,
    string filedNumber,
    DateTime dueDate,
    string createdBy,
    SolicitudeStatusEnum initialStatus = SolicitudeStatusEnum.Pending)
  {
    var solicitude = new Solicitude
    {
      Id = Guid.NewGuid(),
      Subject = subject,
      Description = description,
      DateSolicitude = DateTime.UtcNow,
      FiledNumber = filedNumber,
      AreaId = areaId,
      ClientId = clientId,
      TypeId = typeId,
      Status = initialStatus,
      DueDate = dueDate,
      CreatedAt = DateTime.UtcNow,
      UpdatedAt = DateTime.UtcNow,
    };

    solicitude.Raise(new SolicitudeCreatedEvent(
      Guid.NewGuid(),
      solicitude.Id,
      filedNumber,
      createdBy,
      initialStatus
    ));

    return solicitude;
  }

  public void ChangeStatus(SolicitudeStatusEnum newStatus, string changedBy, string? justification = null)
  {
    if (Status == newStatus) return;

    // Validaciones de negocio
    if (Status == SolicitudeStatusEnum.Completed && newStatus != SolicitudeStatusEnum.Completed)
      throw new InvalidOperationException("Cannot change status of a completed solicitude");

    if (Status == SolicitudeStatusEnum.Rejected && newStatus != SolicitudeStatusEnum.Rejected)
      throw new InvalidOperationException("Cannot change status of a rejected solicitude");

    var oldStatus = Status;
    Status = newStatus;
    UpdatedAt = DateTime.UtcNow;

    Raise(new SolicitudeStatusChangedEvent(
      Guid.NewGuid(),
      Id,
      changedBy,
      oldStatus,
      newStatus,
      justification
    ));
  }

  public void AssignToUser(Guid userId, string assignedToName, string assignedBy)
  {
    if (UserId == userId) return;

    UserId = userId;
    UpdatedAt = DateTime.UtcNow;

    Raise(new SolicitudeAssignedEvent(
      Guid.NewGuid(),
      Id,
      assignedBy,
      userId,
      assignedToName
    ));

    if (Status == SolicitudeStatusEnum.Pending)
    {
      ChangeStatus(SolicitudeStatusEnum.InProcess, assignedBy, "Assigned automatically to functionary");
    }
  }

  public void AddResponse(string content, string respondedBy,
    SolicitudeStatusEnum newStatus = SolicitudeStatusEnum.Completed)
  {
    if (!UserId.HasValue && newStatus == SolicitudeStatusEnum.Completed)
      throw new InvalidOperationException("Cannot complete an unassigned solicitude");

    Raise(new SolicitudeResponseAddedEvent(
      Guid.NewGuid(),
      Id,
      respondedBy,
      content,
      newStatus
    ));

    Status = newStatus;
    UpdatedAt = DateTime.UtcNow;
  }
}