using MODELS.Base;
using MODELS.HETHONG.PHANQUYEN.Dtos;
using MODELS.HETHONG.ROLE.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.HETHONG.TAIKHOAN.Dtos
{
    public class MODELTaiKhoan
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Address { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public Guid? RoleId { get; set; }
        public bool? Vertify { get; set; }
        public string Token { get; set; }
        public MODELRole? Role { get; set; }
        public List<MODELPhanQuyen> ListPhanQuyen { get; set; } = new List<MODELPhanQuyen>();
    }
}
