
using ComputerCheker.DAL.Entityes;
using ComputerCheker.Interfaces;
using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCheker.ViewModels
{
    class AuthViewModel : ViewModel
    {
        private readonly IRepository<User> userRepository;

        public AuthViewModel(IRepository<User> userRepository)
        {
            userRepository = userRepository;
        }
    }
}
