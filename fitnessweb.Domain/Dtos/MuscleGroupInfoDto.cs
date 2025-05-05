using System.Text.Json.Serialization;

namespace fitnessweb.Domain.Dtos;

public class MuscleGroupInfoDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("muscles")]
    public ICollection<MuscleDto> Muscles { get; set; }
}