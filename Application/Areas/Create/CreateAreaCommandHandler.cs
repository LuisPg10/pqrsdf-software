using Domain.Entities.Areas;
using Domain.Primitives;
using Domain.Repositories;

namespace Application.Areas.Create
{
    public class CreateAreaCommandHandler(IAreaRepository areaRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateAreaCommandDto, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(CreateAreaCommandDto request, CancellationToken cancellationToken)
        {
            var alreadyExists = await areaRepository.ExistsByNameAsync(request.Name, cancellationToken);
            if (alreadyExists)
                return Error.Validation("Area.AlreadyExists", "An area with this name already exists");

            var area = request.Adapt<Area>();

            areaRepository.Create(area);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
