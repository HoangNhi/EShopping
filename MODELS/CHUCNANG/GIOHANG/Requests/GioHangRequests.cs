using MODELS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.CHUCNANG.GIOHANG.Requests
{
    public class GioHangRequests : BaseRequest
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid SanPhamId { get; set; }

        public Guid NhomPhanLoai1Id { get; set; }

        public Guid NhanPhanLoai2Id { get; set; }

        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
