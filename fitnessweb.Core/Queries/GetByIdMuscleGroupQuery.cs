namespace fitnessweb.Core.Queries;

using fitnessweb.Domain.Entities;
using MediatR;

public class GetByIdMuscleGroupQuery : IRequest<MuscleGroup>
{ 
    public required Guid Id { get; set; }
}