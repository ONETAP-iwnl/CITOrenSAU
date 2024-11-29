using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.Interface.Executors
{
    public interface IExecutorsService //ISP
    {
        Task<List<Classes.Executors.Executors>> GetAllExecutorsAsync();
    }
}
