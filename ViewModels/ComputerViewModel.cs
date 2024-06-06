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
    class ComputerViewModel : ViewModel
    {
        private readonly IRepository<Computer> _computerRepository;

        public ComputerViewModel(IRepository<Computer> computerRepository)
        {
            _computerRepository = computerRepository;
        }
    }
}
