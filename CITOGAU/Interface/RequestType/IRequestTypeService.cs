using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.Interface.RequestType
{
    public interface IRequestTypeService
    {
        Task<List<Classes.RequestType.RequestTypes>> GetAllRequestsAsync();
    }
}
