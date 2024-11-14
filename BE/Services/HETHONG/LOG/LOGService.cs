using AutoMapper;
using ENTITIES.DbContent;
using MODELS.Base;
using MODELS.BASE;
using MODELS.CHUCNANG.GIOHANG.Dtos;
using MODELS.CHUCNANG.HOADON.Dtos;
using MODELS.DANHMUC.NHANHIEU.Dtos;
using MODELS.DANHMUC.SANPHAM.Dtos;
using MODELS.HETHONG.LOG;
using MODELS.HETHONG.TAIKHOAN.Dtos;

namespace BE.Services.HETHONG.LOG
{
    public class LOGService : ILOGService
    {
        private readonly EShoppingContext _context;
        private readonly IMapper _mapper;
        public LOGService(EShoppingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public BaseResponse<GetListPagingResponse> GetAllListPaging(GetListPagingRequest request)
        {
            var res = new BaseResponse<GetListPagingResponse>();
            try
            {
                var data = new List<Log>();
                if (!string.IsNullOrEmpty(request.TextSearch))
                {
                    var result = _context.NhatKis.OrderBy(hd => hd.Date).Where(x => x.Name == request.TextSearch).Skip((request.PageIndex - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
                    data = _mapper.Map<List<Log>>(result);
                }
                else
                {
                    var result = _context.NhatKis.OrderByDescending(hd => hd.Date).Skip((request.PageIndex - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
                    data = _mapper.Map<List<Log>>(result);
                    
                }
                foreach (var item in data)
                {
                    var user = _mapper.Map<MODELTaiKhoan>( _context.ApplicationUsers.FirstOrDefault(x => x.Id == item.UserId));
                    var product = new object();
                    switch (item.Name)
                    {
                        case "Sản phẩm":
                            var sanPham = _context.SanPhams.FirstOrDefault(x => x.Id == item.TargetId);
                            product = _mapper.Map<MODELSanPham>(sanPham);
                            break;
                        case "Giỏ hàng":
                            var giohang = _context.GioHangs.FirstOrDefault(x => x.Id == item.TargetId);
                            product = _mapper.Map<MODELGioHang>(giohang);
                            break;
                        case "Nhãn hiệu":
                            var nhanhieu = _context.NhanHieus.FirstOrDefault(x => x.Id == item.TargetId);
                            product = _mapper.Map<MODELNhanHieu>(nhanhieu);
                            break;
                        case "Người dùng":
                            var nguoidung = _context.NhanHieus.FirstOrDefault(x => x.Id == item.TargetId);
                            product = _mapper.Map<MODELTaiKhoan>(nguoidung);
                            break;
                        case "Hoá đơn":
                            var hoadon = _context.HoaDons.FirstOrDefault(x => x.Id == item.TargetId);
                            product = _mapper.Map<MODELHoaDon>(hoadon);
                            break;
                        case "Thể loại":
                            var theloai = _context.TheLoais.FirstOrDefault(x => x.Id == item.TargetId.ToString());
                            product = _mapper.Map<TheLoai>(theloai);
                            break;
                        default:
                            // Handle other cases
                            break;
                    }
                    if (user != null && product != null)
                    {
                        item.user = _mapper.Map<MODELTaiKhoan>(user);
                        item.data = product;
                    }

                }
                var page = new GetListPagingResponse();
                page.PageIndex = request.PageIndex;
                page.TotalRow = _context.NhatKis.Count();
                page.Data = data;
                res.Data = page;
            }
            catch (Exception ex)
            {
                res.Error = true;
                res.Message = ex.Message;
                res.StatusCode = 500;
            }
            return res;
        }

        public BaseResponse<GetListPagingResponse> GetListPagingBinhLuan(GetListPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<GetListPagingResponse> GetListPagingGioHang(GetListPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<GetListPagingResponse> GetListPagingHoaDon(GetListPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<GetListPagingResponse> GetListPagingNhanHieu(GetListPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<GetListPagingResponse> GetListPagingSanPham(GetListPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<GetListPagingResponse> GetListPagingTaiKhoan(GetListPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<GetListPagingResponse> GetListPagingTheLoai(GetListPagingRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
