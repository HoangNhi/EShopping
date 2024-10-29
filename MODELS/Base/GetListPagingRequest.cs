using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.BASE
{
    public class GetListPagingRequest
    {
        public int PageIndex { get; set; }
        public int RowsPerPage { get; set; }
        public string? TextSearch { get; set; }
    }
}
