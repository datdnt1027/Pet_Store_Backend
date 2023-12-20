using Mapster;
using pet_store_backend.application.Customer.Commands;
using pet_store_backend.application.Customer.Common;
using pet_store_backend.contracts.User;

namespace pet_store_backend.api.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateUserProfileRequest, UpdateUserProfileCommand>();
        config.NewConfig<UserProfileResult, UserProfileResponse>()
            .Map(dest => dest.Sex, src => ((int?)src.Sex).ToString())
            .Map(dest => dest.Avatar, src => src.Avatar.Length > 0 ? $"data:image/jpeg;base64, {Convert.ToBase64String(src.Avatar)}" : null);
    }
}