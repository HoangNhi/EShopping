using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.Base
{
    public class GetByIdRequest
    {
        [Required(ErrorMessage = "Mã không được để trống")]
        public Guid Id { get; set; }
    }
}
