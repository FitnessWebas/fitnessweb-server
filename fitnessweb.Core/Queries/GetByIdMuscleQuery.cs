namespace fitnessweb.Core.Queries;

using Domain.Dtos;
using MediatR;

public class GetByIdMuscleQuery : IRequest<MuscleInfoDto>
{
    public required Guid Id { get; set; }
}