using Application.SolicitudeTypes.Common;
using Domain.Repositories;

namespace Application.SolicitudeTypes.GetAll
{
    public class GetAllSolicitudeTypeQueryHandler(ISolicitudeTypeRepository solicitudeTypeRepository) : IRequestHandler<GetAllSolicitudeTypeQueryDto, ErrorOr<IReadOnlyList<SolicitudeTypeResponseDto>>>
    {
        public async Task<ErrorOr<IReadOnlyList<SolicitudeTypeResponseDto>>> Handle(
            GetAllSolicitudeTypeQueryDto request,
            CancellationToken cancellationToken)
        {
            var solicitudeTypes = solicitudeTypeRepository.ListAll();

            if (solicitudeTypes == null || !solicitudeTypes.Any())
                return Error.NotFound("SolicitudeTypes.NotFound", "No solicitude types found");

            var response = solicitudeTypes.Select(st => new SolicitudeTypeResponseDto(
                st.Id,
                st.Name,
                st.Time
            )).ToList();

            return response;
        }
    }
}
