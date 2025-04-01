namespace fitnessweb.Core.Queries;

using Domain.Dtos;
using MediatR;

public class GetByIdMuscleGroupQuery : IRequest<MuscleGroupInfoDto>
{ 
    public required Guid Id { get; set; }
}