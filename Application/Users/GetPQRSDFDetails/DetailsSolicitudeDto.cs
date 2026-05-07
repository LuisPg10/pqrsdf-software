using Application.Shared.Dtos;
using Domain.Entities.Solicitudes;
using AreaResponseDto = Application.Areas.Common.AreaResponseDto;

namespace Application.Users.GetPQRSDFDetails;

public record DetailsSolicitudeDto(
  Guid Id,
  AreaResponseDto Area,
  string Subject,
  string Description,
  DateTime DateSolicitude,
  string FiledNumber,
  SolicitudeStatusEnum Status,
  ClientResponseDto Client,
  SolicitudeTypeResponseDto Type,
  List<SolicitudeRespondeDto> SolicitudeResponde
);