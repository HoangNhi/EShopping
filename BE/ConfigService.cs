using AutoMapper;
using BE.AutoMapper;
using BE.Services.CHUCNANG.GIOHANG;
using BE.Services.DANHMUC.NHANHIEU;
using BE.Services.DANHMUC.SANPHAM;
using BE.Services.DANHMUC.THELOAI;
using BE.Services.HETHONG.MAIL;
using BE.Services.HETHONG.NHOMQUYEN;
using BE.Services.HETHONG.TAIKHOAN;

namespace BE
{
    public static class ConfigService
    {
        public static void Config(this IServiceCollection services)
        {
            StartSetting(services);

            services.AddHttpContextAccessor();

            // Register Services
            services.AddTransient<ITAIKHOANService, TAIKHOANService>();
            services.AddTransient<IMAILService, MAILService>();
            services.AddTransient<INHOMQUYENService, NHOMQUYENService>();
            
            // DANH MỤC
            services.AddTransient<ITHELOAIService, THELOAIService>();
            services.AddTransient<INHANHIEUService, NHANHIEUService>();
            services.AddTransient<ISANPHAMService, SANPHAMService>();

            //CHUC NANG
            services.AddTransient<IGIOHANGService, GIOHANGService>();
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
