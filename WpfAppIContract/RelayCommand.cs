using System;
using System.Windows.Input;

namespace WpfAppIContract
{
    /// <summary>
    /// Реализация интерфейса ICommand для делегатов
    /// Позволяет привязывать методы к командам в XAML (Button.Command)
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;        // Делегат для выполнения команды
        private readonly Func<bool> _canExecute; // Делегат для проверки возможности выполнения

        /// <summary>
        /// Конструктор команды
        /// </summary>
        /// <param name="execute">Метод, который выполняется при вызове команды</param>
        /// <param name="canExecute">Метод, проверяющий можно ли выполнить команду (опционально)</param>
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Проверяет можно ли выполнить команду в данный момент
        /// </summary>
        /// <returns>True если команда может быть выполнена</returns>
        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        /// <summary>
        /// Выполняет команду
        /// </summary>
        public void Execute(object parameter) => _execute();

        /// <summary>
        /// Событие, уведомляющее об изменении возможности выполнения команды
        /// Автоматически вызывается WPF при изменениях в интерфейсе
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}