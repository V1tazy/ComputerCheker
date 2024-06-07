using ComputerCheker.DAL.Entityes;
using ComputerCheker.Interfaces;
using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCheker.ViewModels
{
    class ProgramViewModel : ViewModel
    {

        #region ComputerRepository
        private ObservableCollection<ComputerProgram> _programs;

        public ObservableCollection<ComputerProgram> Programs
        {
            get { return _programs; }
            set => Set(ref _programs, value);
        }
        #endregion

        private readonly IRepository<ComputerProgram> _ProgramRepository;
        public ProgramViewModel(IRepository<ComputerProgram> programRepository)
        {
            _ProgramRepository = programRepository;


            Programs = new ObservableCollection<ComputerProgram>(programRepository.Items);
        }
    }
}
