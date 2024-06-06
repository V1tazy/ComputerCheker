using ComputerCheker.DAL.Entityes;
using ComputerCheker.Interfaces;
using ComputerCheker.Service;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ComputerCheker.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<User> _UserRepository;
        private readonly ISearchService _searchService;
        private readonly IRepository<Computer> _computerRepository;
        private readonly IRepository<SetupMode> setupModeRepository;
        private readonly IRepository<ComputerProgram> _programRepository;
        private readonly IRepository<Setup> _setupRepository;

        #region Title: string - отображает название окна
        private string _Title = "Окно";

        public string Title
        {
            get { return _Title; }
            set => Set(ref _Title, value);
        }
        #endregion

        #region AuthStatus: Boolean Отображает статус авторизации
        private bool _AuthStatus;

        public bool AuthStatus
        {
            get { return _AuthStatus; }
            set => Set(ref _AuthStatus, value);
        }
        #endregion

        #region CurrentModel: ViewModel Отображает используемую дочернюю модель
        private ViewModel _CurrenModel;

        public ViewModel CurrenModel
        {
            get { return _CurrenModel; }
            set => Set(ref _CurrenModel, value);
        }
        #endregion


        #region Command ShowComputerViewCommand: Отображет модель Компьютеров
        private ICommand _ShowComputerViewCommand;

        public ICommand ShowComputerViewCommand => _ShowComputerViewCommand
            ??= new LambdaCommand(OnShowComputerViewCommandExecuted, CanShowComputerViewCommandExecute);

        private bool CanShowComputerViewCommandExecute(object? arg)
        {

        }

        private void OnShowComputerViewCommandExecuted(object? obj)
        {
            CurrenModel = new ComputerViewModel(_computerRepository);
        }
        #endregion

        #region Command ShowProgramViewCommand: Отображает Модель Программ
        private ICommand _ShowProgramViewCommand;

        public ICommand ShowProgramViewCommand => _ShowProgramViewCommand
            ??= new LambdaCommand(OnShowProgramViewCommandExecuted, CanShowProgramViewCommandExecute);

        private bool CanShowProgramViewCommandExecute(object? arg)
        {
        }

        private void OnShowProgramViewCommandExecuted(object? obj)
        {
            CurrenModel = new ProgramViewModel(_programRepository);
        }
        #endregion

        #region Command ShowSetupViewCommand: Отображает Модель Программ
        private ICommand _ShowSetupViewCommand;

        public ICommand ShowSetupViewCommand => _ShowSetupViewCommand
            ??= new LambdaCommand(OnShowSetupViewCommandExecuted, CanShowSetupViewCommandExecute);

        private bool CanShowSetupViewCommandExecute(object? arg)
        {
        }

        private void OnShowSetupViewCommandExecuted(object? obj)
        {
            CurrenModel = new SetupVIewModel(_setupRepository, _computerRepository, _programRepository, setupModeRepository);
        }
        #endregion

        public MainWindowViewModel(IRepository<User> User, ISearchService searchService, 
            IRepository<Computer> ComputerRepository,
            IRepository<SetupMode> SetupModeRepository,
            IRepository<ComputerProgram> ProgramRepository, IRepository<Setup> SetupRepository) 
        {
            _UserRepository = User;
            _searchService = searchService;
            _computerRepository = ComputerRepository;
            setupModeRepository = SetupModeRepository;
            _programRepository = ProgramRepository;
            _setupRepository = SetupRepository;

        }
    }
}
