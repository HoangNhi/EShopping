using AutoMapper;
using BE.AutoMapper;
using BE.Services.HETHONG.TAIKHOAN;

namespace BE.Services
{
    public static class ConfigService
    {
        public static void Config(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Register Services
            services.AddTransient<ITAIKHOANService, TAIKHOANService>();
        }
    }
}
