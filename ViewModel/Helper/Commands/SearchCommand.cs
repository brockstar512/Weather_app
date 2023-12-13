using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyWeatherApp.ViewModel.Helper.Commands
{
    public class SearchCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public WeatherVM VM { get; set; }

        public SearchCommand(WeatherVM vm)
        {
            VM = vm; 
        }

        public bool CanExecute(object parameter)
        {
            string query = parameter as string;
            if (string.IsNullOrWhiteSpace(query))
            {
                return false;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            VM.MakeQuery();
        }
    }
}
