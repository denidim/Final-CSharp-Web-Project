using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindATrade.Services.Data
{
    public interface ILikeService
    {
        Task SetLike(int companyId, string userId);
    }
}
