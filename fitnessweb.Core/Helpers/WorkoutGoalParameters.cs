namespace fitnessweb.Core.Helpers;

using fitnessweb.Domain.Types;

public class GoalParameters
{
    public (int Min, int Max) Sets { get; set; }
    public (int Min, int Max) RepsPerSet { get; set; }
    public int RestBetweenSetsSeconds { get; set; }
    public int RestBetweenExercisesSeconds { get; set; }
}

public static class WorkoutGoalParameters
{
    public static GoalParameters Get(Goal goal) =>
        goal switch
        {
            Goal.LoseWeight => new GoalParameters
            {
                Sets = (2, 3),
                RepsPerSet = (12, 20),
                RestBetweenSetsSeconds = 30,
                RestBetweenExercisesSeconds = 45
            },
            Goal.GainMuscle => new GoalParameters
            {
                Sets = (3, 5),
                RepsPerSet = (6, 12),
                RestBetweenSetsSeconds = 90,
                RestBetweenExercisesSeconds = 90
            },
            Goal.GainStrength => new GoalParameters
            {
                Sets = (4, 5),
                RepsPerSet = (3, 6),
                RestBetweenSetsSeconds = 180,
                RestBetweenExercisesSeconds = 180
            },
            _ => throw new ArgumentOutOfRangeException(nameof(goal), "Unknown goal type")
        };
}