using Storm.Api.Core.Exceptions;
using Storm.Api.Core.Validations;
using TimeTracker.Api.Domains.Accounts.Converters;
using TimeTracker.Api.Domains.Accounts.Services;
using TimeTracker.Api.Domains.Authentications.Services;
using TimeTracker.Api.Domains.Bases.Actions;
using TimeTracker.Api.Models;
using TimeTracker.Dtos;
using TimeTracker.Dtos.Accounts;

namespace TimeTracker.Api.Domains.Accounts.Actions;

public class SetUserProfileCommandParameter
{
    public SetUserProfileRequest Data { get; set; }
}

public class SetUserProfileCommand : AuthenticatedAction<SetUserProfileCommandParameter, UserProfileResponse>
{
    public SetUserProfileCommand(IServiceProvider services) : base(services)
    {
    }

    protected override bool ValidateParameter(SetUserProfileCommandParameter parameter)
    {
        return base.ValidateParameter(parameter) &&
               parameter.Data.IsNotNull() &&
               parameter.Data.Email.IsNotNullOrEmpty();
    }

    protected override void PrepareParameter(SetUserProfileCommandParameter parameter)
    {
        base.PrepareParameter(parameter);

        parameter.Data.Email = parameter.Data.Email.ToLowerInvariant().Trim();
    }

    protected override async Task<UserProfileResponse> Action(SetUserProfileCommandParameter parameter)
    {
        IUserService userService = Services.GetRequiredService<IUserService>();
        User existingAccount = await userService.GetUser(parameter.Data.Email);

        if (existingAccount is { } && existingAccount.Id != Account.Id)
        {
            throw new DomainException(ErrorCodes.EMAIL_ALREADY_EXISTS, "This email is already used by another account");
        }

        bool requireLoginUpdate = Account.Email != parameter.Data.Email;
        Account.Email = parameter.Data.Email;
        Account.FirstName = parameter.Data.FirstName;
        Account.LastName = parameter.Data.LastName;

        await userService.UpdateUser(Account);

        if (requireLoginUpdate)
        {
            ICredentialAuthenticationService authenticationService = Services.GetRequiredService<ICredentialAuthenticationService>();
            LoginPasswordUserAuthentication credentials = await authenticationService.GetAuthenticationForUser(Account);
            if (credentials is { })
            {
                credentials.Login = Account.Email;
                await authenticationService.UpdateAuthentication(credentials);
            }
        }

        return Account.ToDtos();
    }
}