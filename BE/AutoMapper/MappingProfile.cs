using AutoMapper;
using ENTITIES.DbContent;
using MODELS.CHUCNANG.CHITIETDONHANG.Dtos;
using MODELS.CHUCNANG.CHITIETDONHANG.Requests;
using MODELS.CHUCNANG.GIOHANG.Dtos;
using MODELS.CHUCNANG.GIOHANG.Requests;
using MODELS.CHUCNANG.HOADON.Dtos;
using MODELS.CHUCNANG.HOADON.Requests;
using MODELS.DANHMUC.ANHSAN_HAM.Dtos;
using MODELS.DANHMUC.BINHLUAN.Dtos;
using MODELS.DANHMUC.BINHLUAN.Request;
using MODELS.DANHMUC.CAUHINHSANPHAM.Dtos;
using MODELS.DANHMUC.CAUHINHSANPHAM.Request;
using MODELS.DANHMUC.NHANHIEU.Dtos;
using MODELS.DANHMUC.NHANHIEU.Requests;
using MODELS.DANHMUC.NHOMPHANLOAI1.Dtos;
using MODELS.DANHMUC.NHOMPHANLOAI1.Request;
using MODELS.DANHMUC.NHOMPHANLOAI2.Dtos;
using MODELS.DANHMUC.NHOMPHANLOAI2.Request;
using MODELS.DANHMUC.SANPHAM.Dtos;
using MODELS.DANHMUC.SANPHAM.Requests;
using MODELS.DANHMUC.THELOAI.Dtos;
using MODELS.DANHMUC.THELOAI.Requests;
using MODELS.DANHMUC.TRALOIBINHLUAN.Dtos;
using MODELS.DANHMUC.TRALOIBINHLUAN.Request;
using MODELS.HETHONG.LOG;
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

            //LOG
            CreateMap<NhatKi,NhatKiRequest>().ReverseMap();
            CreateMap<NhatKi,NhatKiDTO>().ReverseMap();

            //DANH MUC
            CreateMap<TheLoai, MODELTheLoai>().ReverseMap();
            CreateMap<TheLoai, TheLoaiRequest>().ReverseMap();
            CreateMap<NhanHieu, MODELNhanHieu>().ReverseMap();
            CreateMap<NhanHieu, NhanHieuRequests>().ReverseMap();
            CreateMap<SanPham, MODELSanPham>().ReverseMap();
            CreateMap<SanPham, SanPhamRequests>().ReverseMap();
            CreateMap<SanPham, SanPhamResponse>().ReverseMap();
            CreateMap<SanPham, SanPhamRequestAll>().ReverseMap();
            CreateMap<SanPhamRequestAll, SanPhamResponse>().ReverseMap();
            CreateMap<CauHinhSanPham, MODELCauHinhSanPham>().ReverseMap();
            CreateMap<CauHinhSanPham, CauHinhSanPhamRequests>().ReverseMap();
            CreateMap<MODELCauHinhSanPham, CauHinhSanPhamRequests>().ReverseMap();
            CreateMap<SanPham_Anh, MODELSanPhamAnh>().ReverseMap();
            CreateMap<SanPham_Anh, SanPhamRequests>().ReverseMap();
            CreateMap<NhomPhanLoai1, MODELNhomPhanLoai1>().ReverseMap();
            CreateMap<NhomPhanLoai1, NhomPhanLoai>().ReverseMap();
            CreateMap<NhomPhanLoai1, NhomPhanLoai1Requests>().ReverseMap();
            CreateMap<NhomPhanLoai2, MODELNhomPhanLoai2>().ReverseMap();
            CreateMap<NhomPhanLoai2, NhomPhanLoai2Request>().ReverseMap();
            

            //CHUC NANG
            CreateMap<GioHang,MODELGioHang>().ReverseMap();
            CreateMap<GioHang,GioHangRequests>().ReverseMap();
            CreateMap<HoaDon, MODELHoaDon>().ReverseMap();
            CreateMap<HoaDon,HoaDonRequests>().ReverseMap();
            CreateMap<HoaDon,HoaDonResponse>().ReverseMap();
            CreateMap<ChiTietDonHang,MODELChiTietDonHang>().ReverseMap();
            CreateMap<ChiTietDonHang,ChiTietDonHangRequests>().ReverseMap();
            CreateMap<BinhLuan, BinhLuanResponse>().ReverseMap();
            CreateMap<BinhLuan, MODELBinhLuan>().ReverseMap();
            CreateMap<BinhLuan, BinhLuanRequests>().ReverseMap();
            CreateMap<TraLoiBinhLuan, MODELTraLoiBinhLuan>().ReverseMap();
            CreateMap<TraLoiBinhLuan, TraLoiBinhLuanRequests>().ReverseMap();
        }
    }
}
