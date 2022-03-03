using System.Data;
using ServiceStack.OrmLite;
using Storm.Api.Core.Databases;
using Storm.Api.Core.Exceptions;
using Storm.Api.Core.Extensions;
using TimeTracker.Api.Domains.Accounts.Services;
using TimeTracker.Api.Domains.Authentications.Actions;
using TimeTracker.Api.Domains.Authentications.Services;
using TimeTracker.Api.Models;
using TimeTracker.Dtos;
using TimeTracker.Dtos.Accounts;

namespace TimeTracker.Api.Domains.Accounts.Actions;

public class CreateUserCommandParameter : ILoginCommandParameter
{
    public CreateUserRequest Data { get; set; }
    public string ClientId => Data.ClientId;
    public string ClientSecret => Data.ClientSecret;
}
    
public class CreateUserCommand : BaseLoginCommand<CreateUserCommandParameter>
{
    public CreateUserCommand(IServiceProvider services) : base(services)
    {
    }

    protected override bool ValidateParameter(CreateUserCommandParameter parameter)
    {
        return base.ValidateParameter(parameter) &&
               parameter.Data.Email.NotNullOrEmpty() &&
               parameter.Data.Password.NotNullOrEmpty();
    }
        
    protected override void PrepareParameter(CreateUserCommandParameter parameter)
    {
        base.PrepareParameter(parameter);

        parameter.Data.Email = parameter.Data.Email.ToLowerInvariant().Trim();
        parameter.Data.Password = parameter.Data.Password.Trim();
    }

    protected override async Task<IAuthentication> LoadAuthentication(CreateUserCommandParameter parameter)
    {
        IUserService userService = Services.GetRequiredService<IUserService>();
        User existingAccount = await userService.GetUser(parameter.Data.Email);

        if (existingAccount is {})
        {
            throw new DomainException(ErrorCodes.EMAIL_ALREADY_EXISTS, "This email is already used by another account");
        }

        IDbConnection connection = await Services.GetRequiredService<IDatabaseService>().Connection;

        User user = new User
        {
            CollationId = Guid.NewGuid(),
            Email = parameter.Data.Email,
            FirstName = parameter.Data.FirstName,
            LastName = parameter.Data.LastName,
        };
        user.Id = await connection.InsertAsync(user, selectIdentity: true);
            
        ICredentialAuthenticationService authenticationService = Resolve<ICredentialAuthenticationService>();
        return await authenticationService.CreateAuthenticationForUser(user, parameter.Data.Password);
    }
}