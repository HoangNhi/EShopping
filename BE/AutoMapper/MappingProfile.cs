using AutoMapper;
using ENTITIES.DbContent;
using MODELS.HETHONG.TAIKHOAN.Dtos;
using MODELS.HETHONG.TAIKHOAN.Requests;

namespace BE.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // TAIKHOAN
            CreateMap<ApplicationUser, MODELTaiKhoan>().ReverseMap();
            CreateMap<ApplicationUser, PostRegisterRequest>().ReverseMap();
        }
    }
}
