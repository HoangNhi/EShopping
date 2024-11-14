using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.HETHONG.LOG
{
    public class NhatKiDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Event { get; set; } = null!;

        public DateTime? Date { get; set; }

        public Guid TargetId { get; set; }

        public Guid UserId { get; set; }
    }
}
