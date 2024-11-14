using MODELS.HETHONG.TAIKHOAN.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.HETHONG.LOG
{
    public class Log
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Event { get; set; } = null!;

        public DateTime? Date { get; set; }

        public Guid TargetId { get; set; }

        public Guid UserId { get; set; }
        public MODELTaiKhoan? user { get; set; } = new MODELTaiKhoan();
        public object? data { get; set; } = null!;
    }
}
