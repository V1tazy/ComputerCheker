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
    class ProgramViewModel : ViewModel
    {
        private readonly IRepository<ComputerProgram> _ProgramRepository;
        public ProgramViewModel(IRepository<ComputerProgram> programRepository)
        {
            _ProgramRepository = programRepository;
        }
    }
}
