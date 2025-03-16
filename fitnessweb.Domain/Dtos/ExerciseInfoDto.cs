using fitnessweb.Domain.Types;

namespace fitnessweb.Domain.Dtos;

public class ExerciseInfoDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Equipment Equipment { get; set; }
    public int MinutesPerSet { get; set; }
    public FitnessLevel Difficulty { get; set; }
    public string ImagePath { get; set; }
    public List<MuscleDto> Muscles { get; set; }
}