
using Application.Areas.Common;

namespace Application.Areas.GetAll
{
    public record GetAllAreaQueryDto() : IRequest<ErrorOr<IReadOnlyList<AreaResponseDto>>>;
}
