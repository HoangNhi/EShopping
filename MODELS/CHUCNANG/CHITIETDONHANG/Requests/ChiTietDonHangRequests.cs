using MODELS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.CHUCNANG.CHITIETDONHANG.Requests
{
    public class ChiTietDonHangRequests : BaseRequest
    {
        public Guid Id { get; set; }

        public Guid HoaDonId { get; set; }

        public Guid SanPhamId { get; set; }

        public Guid NhomPhanLoai1Id { get; set; }

        public Guid NhomPhanLoai2Id { get; set; }

        public int Quantity { get; set; }

        public int? TotalPrice { get; set; }

        public bool? Status { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
