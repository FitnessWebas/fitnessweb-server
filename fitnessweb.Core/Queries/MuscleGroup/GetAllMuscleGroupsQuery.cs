namespace fitnessweb.Core.Queries;

using Domain.Dtos;
using MediatR;

public class GetAllMuscleGroupsQuery : IRequest<List<MuscleGroupInfoDto>>;