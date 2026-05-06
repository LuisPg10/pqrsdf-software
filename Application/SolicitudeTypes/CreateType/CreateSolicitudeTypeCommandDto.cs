
namespace Application.SolicitudeTypes.CreateType
{
    public record CreateTypeSolicitudeCommandDto : IRequest<ErrorOr<Unit>>
    {
        [Required(ErrorMessage = "Solicitude type name is required.")]
        [MinLength(3, ErrorMessage = "Name must have at least 3 characters.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Time in business days is required.")]
        [Range(1, 365, ErrorMessage = "Time must be between 1 and 365 business days.")]
        public int Time { get; set; }
    }
}
