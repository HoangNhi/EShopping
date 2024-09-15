using AutoMapper;
using BE.AutoMapper;
using BE.Services.HETHONG.TAIKHOAN;
using FluentValidation.AspNetCore;
using MODELS.HETHONG.TAIKHOAN.Requests;

namespace BE.Services
{
    public static class ConfigService
    {
        public static void Config(this IServiceCollection services)
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

            // Register Services
            services.AddTransient<ITAIKHOANService, TAIKHOANService>();
        }
    }
}
