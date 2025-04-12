using fitnessweb.Core.Queries;
using fitnessweb.Domain.Dtos;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.Workout;

public class GetAllWorkoutsQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetAllWorkoutsQuery, List<Domain.Dtos.WorkoutInfoDto>>
{
    public async Task<List<Domain.Dtos.WorkoutInfoDto>> Handle(GetAllWorkoutsQuery request, CancellationToken cancellationToken)
    {
        return await fitnessDbContext.Workouts
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
                    Sets = e.Sets,
                    RepsPerSet = e.RepsPerSet,
                }).ToList(),
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}