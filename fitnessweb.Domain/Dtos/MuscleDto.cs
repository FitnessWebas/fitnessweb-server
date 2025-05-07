using System.Text.Json.Serialization;

namespace fitnessweb.Domain.Dtos;

public class MuscleDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
}