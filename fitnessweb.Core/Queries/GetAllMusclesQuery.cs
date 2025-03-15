namespace fitnessweb.Core.Queries;

using Domain.Dtos;
using MediatR;

public class GetAllMusclesQuery : IRequest<List<MuscleInfoDto>>;