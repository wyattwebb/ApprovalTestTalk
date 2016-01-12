using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalTests.Web.Services
{
    public interface IValidateInput
    {
        string ValidateGet(int? count);
    }
}
