using Application.Areas.Common;
using Domain.Repositories;

namespace Application.Areas.GetAll
{
    public class GetAllAreaQueryHandler(IAreaRepository areaRepository) : IRequestHandler<GetAllAreaQueryDto, ErrorOr<IReadOnlyList<AreaResponseDto>>>
    {
        public async Task<ErrorOr<IReadOnlyList<AreaResponseDto>>> Handle(GetAllAreaQueryDto request, CancellationToken cancellationToken)
        {
            var areas = areaRepository.ListAll();

            var response = areas.Select(a => new AreaResponseDto(
                a.Id,
                a.Name
            )).ToList();

            return response;
        }
    }
}
