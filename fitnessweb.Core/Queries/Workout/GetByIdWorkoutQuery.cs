using fitnessweb.Domain.Entities;
using MediatR;

namespace fitnessweb.Core.Queries;

public class GetByIdWorkoutQuery : IRequest<Workout>
{
    public required Guid Id { get; set; }
}