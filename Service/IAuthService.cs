using ComputerCheker.DAL.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCheker.Service
{
    internal interface IAuthService
    {
        IEnumerable<User> Users { get; }
        public bool AuthUser(string login, string password);
    }
}
