using Mapster;
using Own.Trad.Framework.Authentication;
using Own.Trad.WebApi.Contracts.Authentication;

namespace Own.Trad.WebApi.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src);
        }
    }

}