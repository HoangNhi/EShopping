using AutoMapper;
using ENTITIES.DbContent;
using MODELS.DANHMUC.NHANHIEU.Dtos;
using MODELS.DANHMUC.NHANHIEU.Requests;
using MODELS.DANHMUC.THELOAI.Dtos;
using MODELS.DANHMUC.THELOAI.Requests;
using MODELS.HETHONG.NHOMQUYEN.Dtos;
using MODELS.HETHONG.NHOMQUYEN.Requests;
using MODELS.HETHONG.PHANQUYEN.Dtos;
using MODELS.HETHONG.ROLE.Dtos;
using MODELS.HETHONG.TAIKHOAN.Dtos;
using MODELS.HETHONG.TAIKHOAN.Requests;

namespace BE.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // HỆ THỐNG
            // TAIKHOAN
            CreateMap<ApplicationUser, MODELTaiKhoan>().ReverseMap();
            CreateMap<ApplicationUser, PostRegisterRequest>().ReverseMap();

            // Role
            CreateMap<Role, MODELRole>().ReverseMap();

            // PhanQuyen
            CreateMap<PhanQuyen, MODELPhanQuyen>().ReverseMap();

            // NHOMQUYEN
            CreateMap<PHANQUYEN_NHOMQUYEN, MODELNhomQuyen>().ReverseMap();
            CreateMap<PHANQUYEN_NHOMQUYEN, NhomQuyenRequest>().ReverseMap();

            //DANH MUC
            CreateMap<TheLoai, MODELTheLoai>().ReverseMap();
            CreateMap<TheLoai, TheLoaiRequest>().ReverseMap();
            CreateMap<NhanHieu, MODELNhanHieu>().ReverseMap();
            CreateMap<NhanHieu, NhanHieuRequests>().ReverseMap();
        }
    }
}
