using ComputerCheker.DAL.Entityes;
using ComputerCheker.Interfaces;
using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComputerCheker.ViewModels
{
    class ComputerViewModel : ViewModel
    {

        #region Computer
        private ObservableCollection<Computer> _computers;

        public ObservableCollection<Computer> Computers
        {
            get { return _computers; }
            set => Set(ref _computers, value);
        }
        #endregion

        private readonly IRepository<Computer> _computerRepository;

        public ComputerViewModel(IRepository<Computer> computerRepository)
        {
            _computerRepository = computerRepository;

            Computers = new ObservableCollection<Computer>(_computerRepository.Items);
        }
    }
}
