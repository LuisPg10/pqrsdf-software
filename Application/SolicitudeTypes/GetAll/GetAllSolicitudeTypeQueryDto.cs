
using Application.SolicitudeTypes.Common;

namespace Application.SolicitudeTypes.GetAll
{
    public record GetAllSolicitudeTypeQueryDto() : IRequest<ErrorOr<IReadOnlyList<SolicitudeTypeResponseDto>>>;
}
