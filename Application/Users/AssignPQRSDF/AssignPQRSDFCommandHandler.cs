using Application.Utilities.CurrentUsers;
using Domain.Primitives;
using Domain.Repositories;

namespace Application.Users.AssignPQRSDF;

public class AssignPQRSDFCommandHandler(
  ISolicitudeRepository solicitudeRepository,
  IUserRepository userRepository,
  ICurrentUserService currentUserService,
  IUnitOfWork unitOfWork)
  : IRequestHandler<AssignPQRSDFCommandDto, ErrorOr<Unit>>
{
  public async Task<ErrorOr<Unit>> Handle(AssignPQRSDFCommandDto request, CancellationToken cancellationToken)
  {
    var solicitude = await solicitudeRepository.ListById(request.SolicitudeId);
    if (solicitude == null) return Error.NotFound("PQRSDF.NotFound", "PQRSDF not found");

    var functionary = await userRepository.ListById(request.FunctionaryId);
    if (functionary == null) return Error.NotFound("Functionary.NotFound", "Functionary not found");

    var currentUser = currentUserService.GetCurrentUserName();

    solicitude.AssignToUser(functionary.Id, functionary.Name ?? functionary.Email ?? "Unknown", currentUser);
    solicitudeRepository.Update(solicitude);

    await unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}