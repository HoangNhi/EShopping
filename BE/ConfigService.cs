using AutoMapper;
using BE.AutoMapper;
using BE.Services.HETHONG.MAIL;
using BE.Services.HETHONG.TAIKHOAN;
using FluentValidation.AspNetCore;
using MODELS.HETHONG.TAIKHOAN.Requests;

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
            services.AddMvc()
                .AddFluentValidation(config =>
                {
                    config.ImplicitlyValidateChildProperties = true;
                    config.DisableDataAnnotationsValidation = true;
                    config.RegisterValidatorsFromAssemblyContaining<PostLoginRequestValidator>();
                })
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

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
