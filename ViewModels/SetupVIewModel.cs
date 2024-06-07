using ComputerCheker.DAL.Entityes;
using ComputerCheker.Interfaces;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCheker.ViewModels
{
    class SetupVIewModel : ViewModel
    {

        private readonly IRepository<Setup> _SetupRepository;
        private readonly IRepository<Computer> _computerRepository;
        private IRepository<ComputerProgram> _programRepository;
        private IRepository<SetupMode> _setupModeRepository;


        public SetupVIewModel(IRepository<Setup> setupRepository, IRepository<Computer> computerRepository, IRepository<ComputerProgram> programRepository, IRepository<SetupMode> setupModeRepository)
        {
            _SetupRepository = setupRepository;
            _computerRepository = computerRepository;
            _programRepository = programRepository;
            _setupModeRepository = setupModeRepository;
        }
    }
}
