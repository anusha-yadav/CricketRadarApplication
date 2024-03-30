using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CricketRadarApplication.ViewModels
{
    /// <summary>
    /// Relay Command
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
            CommandManager.RequerySuggested += OnRequerySuggested;
        }

        ~RelayCommand()
        {
            CommandManager.RequerySuggested -= OnRequerySuggested;
        }

        /// <summary>
        /// CanExecuteChanged
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// CanExecute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// OnRequerySuggested
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRequerySuggested(object sender, EventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
