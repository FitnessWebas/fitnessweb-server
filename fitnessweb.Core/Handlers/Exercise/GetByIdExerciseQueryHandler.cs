using fitnessweb.Core.Queries;
using fitnessweb.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace fitnessweb.Core.Handlers.Exercise;

public class GetByIdExerciseQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetByIdExerciseQuery, Domain.Dtos.ExerciseInfoDto>
{
    public async Task<Domain.Dtos.ExerciseInfoDto> Handle(GetByIdExerciseQuery request, CancellationToken cancellationToken)
    {
        
        var exercise = await fitnessDbContext.Exercises
            .Include(e => e.Muscles)
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
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        
        return exercise ?? throw new NullReferenceException("Exercise not found");
    }
}