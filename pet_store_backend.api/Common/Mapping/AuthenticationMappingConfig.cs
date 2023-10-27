using Mapster;
using pet_store_backend.application.Authentication.Commands.Register;
using pet_store_backend.application.Authentication.Queries.Login;
using pet_store_backend.application.Services.Authentication;
using pet_store_backend.contracts.Authentication;

namespace pet_store_backend.api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.UserId, src => src.user.Id.Value)
            .Map(dest => dest, src => src.user);
    }
}