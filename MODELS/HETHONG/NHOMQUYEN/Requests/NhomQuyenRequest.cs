using MODELS.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.HETHONG.NHOMQUYEN.Requests
{
    public class NhomQuyenRequest : BaseRequest
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Tên gọi không được để trống")]
        public string TenGoi { get; set; } = null!;

        public string? Icon { get; set; }

        public int Status { get; set; } = 1;
    }
}
