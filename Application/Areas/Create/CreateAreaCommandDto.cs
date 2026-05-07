namespace Application.Areas.Create
{
    public record CreateAreaCommandDto : IRequest<ErrorOr<Unit>>
    {
        [Required(ErrorMessage = "AreaId name is required.")]
        [MinLength(3, ErrorMessage = "Name must have at least 3 characters.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; init; } = string.Empty;
    }
}
