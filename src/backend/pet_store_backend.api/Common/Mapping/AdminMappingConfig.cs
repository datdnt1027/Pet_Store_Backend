using Mapster;
using pet_store_backend.application.Admin.Commands;
using pet_store_backend.application.Admin.Common;
using pet_store_backend.application.Admin.Queries;
using pet_store_backend.application.User.Common;
using pet_store_backend.contracts.Admin;

namespace pet_store_backend.api.Common.Mapping;

public class AdminMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateRoleStatusRequest, UpdateRoleStatusCommand>();
        config.NewConfig<UpdateAdminProfileRequest, UpdateAdminProfileCommand>();
        config.NewConfig<AdminProfileResult, AdminProfileResponse>()
            .Map(dest => dest.Sex, src => ((int?)src.Sex).ToString())
            .Map(dest => dest.Avatar, src => src.Avatar.Length > 0 ? $"data:image/jpeg;base64, {Convert.ToBase64String(src.Avatar)}" : null);
        config.NewConfig<FindUserRequest, CustomerQuery>();
        config.NewConfig<UserProfileWithStatusResult, FindCustomerResponse>()
            .Map(dest => dest.Avatar, src => src.Avatar.Length > 0 ? $"data:image/jpeg;base64, {Convert.ToBase64String(src.Avatar)}" : null); ;
    }
}