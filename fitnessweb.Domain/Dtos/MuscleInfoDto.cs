namespace fitnessweb.Domain.Dtos;

public class MuscleInfoDto 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid MuscleGroupId { get; set; }
    public string MuscleGroupName { get; set; }
}