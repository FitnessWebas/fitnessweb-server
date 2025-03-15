namespace fitnessweb.Domain.Dtos;

public class MuscleGroupInfoDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<MuscleDto> Muscles { get; set; }
}