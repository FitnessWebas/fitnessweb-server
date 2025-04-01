using fitnessweb.Domain.Dtos;

namespace fitnessweb.Core.Queries;

using MediatR;

public class GetAllExercisesQuery : IRequest<List<ExerciseInfoDto>>;