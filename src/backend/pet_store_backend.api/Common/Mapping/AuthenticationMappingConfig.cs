using Mapster;
using pet_store_backend.application.Authentication.Commands.ForgotPassword;
using pet_store_backend.application.Authentication.Commands.Register;
using pet_store_backend.application.Authentication.Commands.ResetPassword;
using pet_store_backend.application.Authentication.Commands.Verify;
using pet_store_backend.application.Authentication.Common;
using pet_store_backend.application.Authentication.Queries.Login;
using pet_store_backend.contracts.Authentication;

namespace pet_store_backend.api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<VerifyRequest, VerifyCommand>();
        config.NewConfig<ForgotPasswordRequest, ForgotPasswordCommand>();
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<PasswordResetRequest, ResetPasswordCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.UserId, src => src.user.Id.Value)
            .Map(dest => dest, src => src.user);
    }
}