using fitnessweb.Domain.Dtos;

namespace fitnessweb.Core.Queries;

using MediatR;

public class GetAllWorkoutsQuery : IRequest<List<WorkoutInfoDto>>;