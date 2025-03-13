using fitnessweb.Core.Commands;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.UserMetrics;

public class UpdateUserMetricsCommandHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<UpdateUserMetricsCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserMetricsCommand command, CancellationToken cancellationToken)
    {
        var user = await fitnessDbContext.UsersMetrics.FirstOrDefaultAsync(userMetric => userMetric.UserId == command.UserId, cancellationToken);
        
        if (user == null)
            throw new Exception($"UserMetrics for userId \"{command.UserId}\" doesn't exist.");
        
        if (command.Height != null)
            user.Height = command.Height.Value;
            
        if (command.Birthday != null)
            user.Birthday = command.Birthday.Value;
            
        if (command.Gender != null)
            user.Gender = command.Gender.Value;
            
        if (command.FitnessLevel != null)
            user.FitnessLevel = command.FitnessLevel.Value;

        await fitnessDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}