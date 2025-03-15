namespace fitnessweb.Core.Queries;

using fitnessweb.Domain.Entities;
using MediatR;

public class GetByIdMuscleQuery : IRequest<Muscle>
{
    public required Guid Id { get; set; }
}