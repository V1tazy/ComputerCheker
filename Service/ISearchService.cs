using ComputerCheker.DAL.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCheker.Service
{
    interface ISearchService
    {
        IEnumerable<Setup> Setups { get; }
        Task<Setup> MakeASetup(int setupId, Computer computer, ComputerProgram program, SetupMode setupMode);

    }
}
