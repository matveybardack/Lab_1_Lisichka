using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfAppIContract
{
    /// <summary>
    /// Главная ViewModel приложения, управляет списком операций и текущей выбранной операцией
    /// Является DataContext для главного окна (MainWindow)
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Коллекция доступных операций текстового конвейера
        /// ObservableCollection автоматически уведомляет интерфейс об изменениях
        /// </summary>
        public ObservableCollection<IOperationViewModel> Operations { get; } =
            new ObservableCollection<IOperationViewModel>();

        private IOperationViewModel _selectedOperation;

        /// <summary>
        /// Текущая выбранная операция в ListBox
        /// При изменении свойства уведомляет интерфейс через OnPropertyChanged
        /// </summary>
        public IOperationViewModel SelectedOperation
        {
            get => _selectedOperation;
            set
            {
                _selectedOperation = value;
                OnPropertyChanged(nameof(SelectedOperation)); // Уведомляем интерфейс об изменении
            }
        }

        /// <summary>
        /// Конструктор главной ViewModel - инициализирует список операций
        /// </summary>
        public MainViewModel()
        {
            // Добавляем наши операции в коллекцию
            Operations.Add(new TrimSpacesOperationViewModel());
            Operations.Add(new LowerCaseOperationViewModel());
            Operations.Add(new RemoveEmptyLinesOperationViewModel());

            SelectedOperation = Operations[0]; // Выбираем первую операцию по умолчанию
        }
    }
}