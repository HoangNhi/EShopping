using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.DANHMUC.SANPHAM.Requests
{
    public class SanPhamCustomRequest
    {
        public int PageIndex { get; set; }
        public int RowsPerPage { get; set; }
        public string? TheLoaiId { get; set; }
        public Guid? NhanHieuId { get; set; }
        public bool? IsNew { get; set; }
        public bool? IsBestSelling { get; set; }
        public bool? IsSale { get; set; }
    }
}
