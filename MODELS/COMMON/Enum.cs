using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.COMMON
{
    public enum Permission
    {
        Watch,
        Add,
        Update,
        Delete
    }

    public enum HttpAction
    {
        Post,
        Put,
        Delete,
        Get
    }
}
