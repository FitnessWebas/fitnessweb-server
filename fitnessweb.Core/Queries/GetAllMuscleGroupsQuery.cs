namespace fitnessweb.Core.Queries;

using fitnessweb.Domain.Entities;
using MediatR;

public class GetAllMuscleGroupsQuery : IRequest<List<MuscleGroup>>;