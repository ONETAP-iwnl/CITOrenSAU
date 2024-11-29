using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CITOGAU.Classes.Authors;

namespace CITOGAU.Interface.Authors
{
    public interface IAuthorsService //ISP
    {
        Task<List<Classes.Authors.Authors>> GetAllAuthorsAsync();
    }
}
