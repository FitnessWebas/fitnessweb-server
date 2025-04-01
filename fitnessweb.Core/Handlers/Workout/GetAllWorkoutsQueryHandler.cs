using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.Workout;

public class GetAllWorkoutsQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetAllWorkoutsQuery, List<Domain.Entities.Workout>>
{
    public async Task<List<Domain.Entities.Workout>> Handle(GetAllWorkoutsQuery request, CancellationToken cancellationToken)
    {
        return await fitnessDbContext.Workouts
            .Include(m => m.Muscles)
            .Include(w => w.WorkoutExercises)
            .ThenInclude(we => we.Exercise) // 🔥 Add this line to load the Exercise!
            .ToListAsync(cancellationToken);
    }
}