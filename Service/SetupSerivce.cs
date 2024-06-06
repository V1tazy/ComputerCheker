using ComputerCheker.DAL.Entityes;
using ComputerCheker.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCheker.Service
{
    class SetupSerivce: ISearchService
    {
        private readonly IRepository<ComputerProgram> _ComputerPrograms;
        private readonly IRepository<Computer> _Computers;
        private readonly IRepository<Setup> _Setups;

        public IEnumerable<Setup> Setups => _Setups.Items;

        public SetupSerivce(IRepository<Setup> SetupRepository, IRepository<Computer> ComputerRepository, IRepository<ComputerProgram> ComputerProgrammRepository) 
        {
            _ComputerPrograms = ComputerProgrammRepository;
            _Computers = ComputerRepository;
            _Setups = SetupRepository;

        }

        public async Task<Setup> MakeASetup(int setupId,Computer computer, ComputerProgram program, SetupMode setupMode)
        {
            var SetupId = _Setups.Items.FirstOrDefaultAsync(b => b.Id == setupId && b.Computer == computer).ConfigureAwait(false);

            if (SetupId.IsNotNull()) return null;

            var setup = new Setup
            {
                Id = setupId,
                Computer = computer,
                Programm = program,
                SetupMode = setupMode
            };

            return await _Setups.AddAsync(setup);
        }
    }
}
