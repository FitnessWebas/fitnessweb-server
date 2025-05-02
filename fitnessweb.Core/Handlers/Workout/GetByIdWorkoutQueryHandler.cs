using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.Workout;

public class GetByIdWorkoutQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetByIdWorkoutQuery, Domain.Dtos.WorkoutInfoDto>
{
    public async Task<Domain.Dtos.WorkoutInfoDto> Handle(GetByIdWorkoutQuery request, CancellationToken cancellationToken)
    {

        if (request.Id == Guid.Empty)
            throw new ArgumentException($"No workout id provided.");

        var workout = await fitnessDbContext.Workouts
                .Where(w => w.Id == request.Id)
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
                .FirstOrDefaultAsync(cancellationToken);

        return workout ?? throw new Exception($"Workout id: {request.Id} does not exist.");

    }
}