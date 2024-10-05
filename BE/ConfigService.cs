using AutoMapper;
using BE.AutoMapper;
using BE.Services.HETHONG.MAIL;
using BE.Services.HETHONG.TAIKHOAN;

namespace BE
{
    public static class ConfigService
    {
        public static void Config(this IServiceCollection services)
        {
            StartSetting(services);

            // Register Services
            services.AddTransient<ITAIKHOANService, TAIKHOANService>();
            services.AddTransient<IMAILService, MAILService>();
        }

        public static void StartSetting(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddHttpContextAccessor();
        }
    }
}
