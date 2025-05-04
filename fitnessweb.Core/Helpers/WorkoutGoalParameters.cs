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
    public static GoalParameters Get(Goal goal, int time)
    {
        var parameters = goal switch
        {
            Goal.LoseWeight => new GoalParameters
            {
                Sets = (3, 4),
                RepsPerSet = (12, 20),
                RestBetweenSetsSeconds = 60,
                RestBetweenExercisesSeconds = 60
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
        
        if (goal == Goal.LoseWeight && time >= 60 && time < 90)
        {
            parameters.Sets = (4, 5);
            parameters.RepsPerSet = (15, 25);
            parameters.RestBetweenSetsSeconds = 75;
            parameters.RestBetweenExercisesSeconds = 75;
        }
        if (goal == Goal.LoseWeight && time >= 90)
        {
            parameters.Sets = (4, 5);
            parameters.RepsPerSet = (20, 25);
            parameters.RestBetweenSetsSeconds = 90;
            parameters.RestBetweenExercisesSeconds = 90;
        }
        if (goal == Goal.GainMuscle && time > 75)
        {
            parameters.Sets = (4, 5);
            parameters.RepsPerSet = (8, 14);
            parameters.RestBetweenSetsSeconds = 120;
            parameters.RestBetweenExercisesSeconds = 120;
        }
        
        return parameters;
    }
}