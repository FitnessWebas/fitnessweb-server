using fitnessweb.Domain.Dtos;
using fitnessweb.Domain.Entities;
using MediatR;

namespace fitnessweb.Core.Queries;

public class GetByIdExerciseQuery : IRequest<ExerciseInfoDto>
{
    public required Guid Id { get; set; }
}