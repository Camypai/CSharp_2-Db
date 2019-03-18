using System;
using System.Windows.Input;

namespace Charp_2_Db.Helpers
{
    /// <summary>
    /// Реализация собственных команд для работы кнопок
    /// </summary>
    public class Command : ICommand
    {
        /// <summary>
        /// Делегат результата команды
        /// </summary>
        private readonly Action<object> _execute;
        /// <summary>
        /// Предикат команды
        /// </summary>
        private readonly Func<object, bool> _canExecute;

        public Command(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Может ли команда выполняться
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Выполнение делегата
        /// </summary>
        /// <param name="parameter">Передаваемый парамент выполнения</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Событие, которое вызывается при изменении условий. Указывает, может ли команда выполняться в новых условиях
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}