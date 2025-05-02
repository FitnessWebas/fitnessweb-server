using fitnessweb.Core.Queries;
using fitnessweb.Domain.Dtos;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.Workout;

public class GetByUserIdWorkoutsQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetByUserIdWorkoutsQuery, List<Domain.Dtos.WorkoutInfoDto>>
{
    public async Task<List<Domain.Dtos.WorkoutInfoDto>> Handle(GetByUserIdWorkoutsQuery request, CancellationToken cancellationToken)
    {
        if (request.UserId == Guid.Empty)
            throw new ArgumentException($"No User id provided.");

        var workouts = await fitnessDbContext.Workouts
            .Where(w => w.UserId == request.UserId)
            .Select(w => new Domain.Dtos.WorkoutInfoDto
            {
                Id = w.Id,
                UserId = w.UserId,
                DateOfCreation = w.CreatedAt,
                Name = w.Name,
                Difficulty = w.Difficulty,
                Equipment = w.Equipment,
                TargetDurationMinutes = w.TargetDurationMinutes,
                Goal = w.Goal,
                MuscleGroups = w.MuscleGroups.Select(m => new Domain.Dtos.MuscleGroupDto
                {
                    Id = m.Id,
                    Name = m.MuscleGroupName
                }).ToList(),
                WorkoutExercises = w.WorkoutExercises.Select(e => new Domain.Dtos.WorkoutExerciseDto
                {
                    ExerciseId = e.ExerciseId,
                    ExerciseName = e.Exercise.Name,
                    Equipment = e.Exercise.Equipment,
                    Muscles = e.Exercise.Muscles.Select(m => new Domain.Dtos.MuscleDto
                    {
                        Id = m.Id,
                        Name = m.Name
                    }).ToList(),
                    Sets = e.Sets,
                    RepsPerSet = e.RepsPerSet,
                }).ToList(),
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);


        return workouts ?? throw new Exception($"User id: {request.UserId} not found or the user has no workouts");
    }
}