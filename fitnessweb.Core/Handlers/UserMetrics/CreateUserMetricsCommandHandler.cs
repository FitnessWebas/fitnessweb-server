using fitnessweb.Core.Commands;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.UserMetrics;

public class CreateUserMetricsCommandHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<CreateUserMetricsCommand, Unit>
{
    public async Task<Unit> Handle(CreateUserMetricsCommand request, CancellationToken cancellationToken)
    {
        var userMetricAlreadyExists = await fitnessDbContext.UsersMetrics
            .AnyAsync(u => u.UserId == request.UserId, cancellationToken);
        
        if (userMetricAlreadyExists)
            throw new Exception($"UserMetrics for userId \"{request.UserId}\" already exists.");
        
        var userMetrics = new Domain.Entities.UserMetrics
        {
            UserId = request.UserId,
            Height = request.Height,
            Birthday = request.Birthday,
            Gender = request.Gender,
            FitnessLevel = request.FitnessLevel,
        };

        await fitnessDbContext.UsersMetrics.AddAsync(userMetrics, cancellationToken);
        await fitnessDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}