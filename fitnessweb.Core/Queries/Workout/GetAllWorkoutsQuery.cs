namespace fitnessweb.Core.Queries;

using fitnessweb.Domain.Entities;
using MediatR;

public class GetAllWorkoutsQuery : IRequest<List<Workout>>;