using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Infrastructure.Services
{
    public interface IUserService
    {
        bool Authenticate(string user, string password);
        void CreateUser(string user, string password);
    }
}
