using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCheker.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _Title = "Окно";

        public string Title
        {
            get { return _Title; }
            set => Set(ref _Title, value);
        }
    }
}
