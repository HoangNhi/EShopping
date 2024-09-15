using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.Base
{
    public class BaseRequest
    {
        public bool IsActived { get; set; } = true;
        public bool IsEdit { get; set; } = false;
    }
}
