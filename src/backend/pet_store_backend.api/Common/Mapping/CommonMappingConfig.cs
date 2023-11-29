using Mapster;
using pet_store_backend.application.Common;
using pet_store_backend.contracts;

namespace pet_store_backend.api.Common.Mapping;

public class CommonMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MessageResult, MessageResponse>();
    }
}