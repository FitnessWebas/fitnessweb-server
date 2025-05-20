using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using fitnessweb.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.User;

public class GetByEmailUserQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetByEmailUserQuery, Guid>
{
    public async Task<Guid> Handle(GetByEmailUserQuery request, CancellationToken cancellationToken)
    { 
        return await fitnessDbContext.Users
            .Where(u => u.Email == request.Email)
            .Select(u => u.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}