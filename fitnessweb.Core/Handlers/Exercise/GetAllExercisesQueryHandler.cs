using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.Exercise;

public class GetAllExercisesQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetAllExercisesQuery, List<Domain.Dtos.ExerciseInfoDto>>
{
    public async Task<List<Domain.Dtos.ExerciseInfoDto>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
    {
        return await fitnessDbContext.Exercises
            .Select(e => new Domain.Dtos.ExerciseInfoDto
            {
                Id = e.Id,
                Name = e.Name,
                Equipment = e.Equipment,
                MinutesPerSet = e.MinutesPerSet,
                Difficulty = e.Difficulty,
                ImagePath = e.ImagePath,
                Muscles = e.Muscles.Select(m => new Domain.Dtos.MuscleDto
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList()
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}