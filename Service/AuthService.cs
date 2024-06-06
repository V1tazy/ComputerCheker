using ComputerCheker.DAL.Entityes;
using ComputerCheker.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCheker.Service
{
    class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;

        public IEnumerable<User> Users => _userRepository.Items;

        public AuthService(IRepository<User> UserRepository)
        {
            _userRepository = UserRepository;
        }

        public bool AuthUser(string login, string password)
        {
            var auth = _userRepository.Items.FirstOrDefaultAsync(b => b.Login == login && b.Password == password).ConfigureAwait(false);

            if (auth.IsNull()) return false;

            return true;
        }
    }
}
