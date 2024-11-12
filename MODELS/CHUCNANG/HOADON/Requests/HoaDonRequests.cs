using MODELS.Base;
using MODELS.CHUCNANG.CHITIETDONHANG.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.CHUCNANG.HOADON.Requests
{
    public class HoaDonRequests : BaseRequest
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int Total { get; set; }

        public DateTime DateCreate { get; set; }

        public Guid DiaChiId { get; set; }

        public string? PhoneNumber { get; set; }

        /// <summary>
        /// 1: Chờ thanh toán, 2: Chờ giao hàng, 3: Đã giao, 4: Đã hủy
        /// </summary>
        public int Status { get; set; }

        public List<ChiTietDonHangRequests> ChiTietDonHangRequests { get; set; }
    }
}
