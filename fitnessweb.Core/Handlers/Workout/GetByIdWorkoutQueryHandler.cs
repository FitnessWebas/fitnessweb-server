using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.Workout;

public class GetByIdWorkoutQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetByIdWorkoutQuery, Domain.Entities.Workout>
{
    public async Task<Domain.Entities.Workout> Handle(GetByIdWorkoutQuery request, CancellationToken cancellationToken)
    {
        
        if (request.Id == Guid.Empty)
            throw new ArgumentException($"No workout id provided.");
        
        var workout = await fitnessDbContext.Workouts
            .Include(w => w.MuscleGroups)
            .Include(w => w.WorkoutExercises)
            .ThenInclude(we => we.Exercise) 
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

        return workout ?? throw new Exception($"Workout id: {request.Id} does not exist.");
        
    }
}