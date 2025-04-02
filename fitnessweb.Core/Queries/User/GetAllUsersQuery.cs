namespace fitnessweb.Core.Queries;

using fitnessweb.Domain.Entities;
using MediatR;

public class GetAllUsersQuery : IRequest<List<User>>;