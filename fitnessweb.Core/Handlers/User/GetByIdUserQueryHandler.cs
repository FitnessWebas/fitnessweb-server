using fitnessweb.Core.Queries;
using fitnessweb.Domain.Dtos;
using fitnessweb.Infrastructure;
using MediatR;

namespace fitnessweb.Core.Handlers.User;

public class GetByIdUserQueryHandler(FitnessWebDbContext fitnessDbContext) : IRequestHandler<GetByIdUserQuery, UserInfoDto>
{
    public async Task<UserInfoDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var user = await fitnessDbContext.Users.FindAsync([request.UserId], cancellationToken)
            ?? throw new NullReferenceException();
        return new UserInfoDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Username = user.Username,
        };
    }
}