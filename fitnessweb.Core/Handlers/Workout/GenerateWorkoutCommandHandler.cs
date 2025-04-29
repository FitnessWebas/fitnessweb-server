using fitnessweb.Core.Commands;
using fitnessweb.Domain.Entities;
using fitnessweb.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace fitnessweb.Core.Handlers.Workout;

public class GenerateWorkoutCommandHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GenerateWorkoutCommand, Unit>
{
    public async Task<Unit> Handle(GenerateWorkoutCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("----- GenerateWorkoutCommand Received -----");
        Console.WriteLine($"UserId: {request.UserId}");
        Console.WriteLine($"Name: {request.Name}");
        Console.WriteLine($"TargetDurationMinutes: {request.TargetDurationMinutes}");
        Console.WriteLine($"Goal: {request.Goal}");
        Console.WriteLine($"Difficulty: {request.Difficulty}");

        Console.WriteLine("Equipment:");
        foreach (var equipment in request.Equipment)
        {
            Console.WriteLine($" - {equipment}");
        }

        Console.WriteLine("Muscle Groups:");
        foreach (var muscle in request.MuscleGroups)
        {
            Console.WriteLine($" - {muscle}");
        }

        Console.WriteLine("-------------------------------------------");

        return Unit.Value;
    }
}