using fitnessweb.Domain.Types;
using MediatR;

namespace fitnessweb.Core.Commands;

public class CreateUserMetricsCommand : IRequest<Unit>
{
    public Guid UserId { get; set; }
    public int Height { get; set; }
    public DateTime Birthday { get; set; }
    public Gender Gender { get; set; }
    public FitnessLevel FitnessLevel { get; set; }
}