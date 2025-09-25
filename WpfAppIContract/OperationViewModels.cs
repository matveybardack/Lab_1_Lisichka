using ClassLibrary;
using System;
using System.Windows;
using System.Windows.Input;

namespace WpfAppIContract
{
    /// <summary>
    /// ViewModel для операции "Удаление лишних пробелов"
    /// Управляет состоянием интерфейса и логикой для этой операции
    /// </summary>
    public class TrimSpacesOperationViewModel : ViewModelBase, IOperationViewModel
    {
        private string _inputText = "";          // Поле для входного текста
        private string _outputText = "";         // Поле для результата
        private bool _operationExecuted = false; // Флаг выполнения операции

        /// <summary>Отображаемое имя операции</summary>
        public string Name => "Удаление лишних пробелов";

        /// <summary>
        /// Входной текст с уведомлением об изменении
        /// При изменении сбрасывает флаг выполнения и результат
        /// </summary>
        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                OnPropertyChanged(); // Уведомляем интерфейс об изменении текста
                OnPropertyChanged(nameof(PreConditionSatisfied)); // Обновляем индикатор Pre

                // Сбрасываем состояние при изменении ввода
                _operationExecuted = false;
                OutputText = "";
                OnPropertyChanged(nameof(PostConditionSatisfied)); // Обновляем индикатор Post
            }
        }

        /// <summary>
        /// Результат обработки (только для чтения)
        /// </summary>
        public string OutputText
        {
            get => _outputText;
            private set
            {
                _outputText = value;
                OnPropertyChanged(); // Уведомляем интерфейс об изменении результата
                OnPropertyChanged(nameof(PostConditionSatisfied)); // Обновляем индикатор Post
            }
        }

        /// <summary>
        /// Проверка предусловия: входной текст не должен быть пустым или состоять только из пробелов
        /// </summary>
        public bool PreConditionSatisfied => !string.IsNullOrWhiteSpace(InputText);

        /// <summary>
        /// Проверка постусловия: 
        /// - Операция должна быть выполнена
        /// - Результат не должен содержать двойных пробелов
        /// - Результат должен быть обрезан по краям
        /// </summary>
        public bool PostConditionSatisfied
        {
            get
            {
                // Если операция не выполнялась или результат пустой - постусловие не выполнено
                if (!_operationExecuted || string.IsNullOrEmpty(OutputText))
                    return false;

                // Проверяем конкретные условия постусловия
                return !OutputText.Contains("  ") &&
                       OutputText.Trim() == OutputText &&
                       OutputText.Length <= InputText.Length;
            }
        }

        /// <summary>
        /// Команда выполнения операции
        /// Проверяет предусловия, выполняет операцию и проверяет постусловия
        /// </summary>
        public ICommand ExecuteCommand => new RelayCommand(() =>
        {
            // Проверка предусловия перед выполнением
            if (!PreConditionSatisfied)
            {
                MessageBox.Show("Предусловие не выполнено! Введите текст.", "Ошибка");
                return;
            }

            try
            {
                // Выполнение операции через бизнес-логику
                OutputText = TextPipeline.RemoveExtraSpaces(InputText);

                // Устанавливаем флаг выполнения и обновляем индикатор
                _operationExecuted = true;
                OnPropertyChanged(nameof(PostConditionSatisfied));

                //MessageBox.Show("Операция выполнена успешно!", "Успех");
            }
            catch (Exception ex)
            {
                // Обработка ошибок выполнения
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка выполнения");
                _operationExecuted = false;
                OnPropertyChanged(nameof(PostConditionSatisfied));
            }
        });

        /// <summary>
        /// Команда показа контракта операции
        /// </summary>
        public ICommand ShowContractCommand => new RelayCommand(() =>
        {
            string contract = @"ОПЕРАЦИЯ: Удаление лишних пробелов

ПРЕДУСЛОВИЕ (Pre):
- Входная строка не null и не пустая

ПОСТУСЛОВИЕ (Post):
- Все последовательности пробелов заменены на один пробел
- Пробелы в начале/конце удалены
- Результат не длиннее исходной строки

ГРАНИЧНЫЕ ПРИМЕРЫ:
✅ Валидный: '  hello    world  ' → 'hello world'
❌ Невалидный: '' (пустая строка) → предусловие не выполнено";

            MessageBox.Show(contract, "Контракт операции", MessageBoxButton.OK, MessageBoxImage.Information);
        });

        // Дополнительные свойства для контракта (могут использоваться для расширенного отображения)
        public string ContractDescription => @"Pre: input != null и не пустая
Post: нет двойных пробелов, обрезаны края";

        public string ValidExample => "Вход: '  hello    world  ' → Выход: 'hello world'";
        public string InvalidExample => "Вход: '' → Ошибка: предусловие не выполнено";
    }

    /// <summary>
    /// ViewModel для операции "Приведение к нижнему регистру"
    /// </summary>
    public class LowerCaseOperationViewModel : ViewModelBase, IOperationViewModel
    {
        private string _inputText = "";
        private string _outputText = "";
        private bool _operationExecuted = false;

        /// <summary>Отображаемое имя операции</summary>
        public string Name => "Приведение к нижнему регистру";

        /// <summary>
        /// Входной текст с уведомлением об изменении
        /// При изменении сбрасывает флаг выполнения и результат
        /// </summary>
        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PreConditionSatisfied));
                _operationExecuted = false;
                OutputText = "";
                OnPropertyChanged(nameof(PostConditionSatisfied));
            }
        }
        /// <summary>
        /// Результат обработки (только для чтения)
        /// </summary>
        public string OutputText
        {
            get => _outputText;
            private set
            {
                _outputText = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PostConditionSatisfied));
            }
        }
        /// <summary>
        /// Проверка предусловия: входной текст не должен быть пустым или состоять только из пробелов
        /// </summary>
        public bool PreConditionSatisfied => !string.IsNullOrWhiteSpace(InputText);

        /// <summary>
        /// Проверка постусловия: 
        /// - Операция должна быть выполнена
        /// - Результат не должен содержать двойных пробелов
        /// - Результат должен быть обрезан по краям
        /// </summary>
        public bool PostConditionSatisfied
        {
            get
            {
                if (!_operationExecuted || string.IsNullOrEmpty(OutputText))
                    return false;

                return OutputText == InputText.ToLower();
            }
        }

        /// <summary>
        /// Команда выполнения операции
        /// Проверяет предусловия, выполняет операцию и проверяет постусловия
        /// </summary>
        public ICommand ExecuteCommand => new RelayCommand(() =>
        {
            if (!PreConditionSatisfied)
            {
                MessageBox.Show("Предусловие не выполнено! Введите текст.", "Ошибка");
                return;
            }

            try
            {
                OutputText = TextPipeline.ToLowerCase(InputText);
                _operationExecuted = true;
                OnPropertyChanged(nameof(PostConditionSatisfied));
                //MessageBox.Show("Операция выполнена успешно!", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка выполнения");
                _operationExecuted = false;
                OnPropertyChanged(nameof(PostConditionSatisfied));
            }
        });

        /// <summary>
        /// Команда показа контракта операции
        /// </summary>
        public ICommand ShowContractCommand => new RelayCommand(() =>
        {
            string contract = @"ОПЕРАЦИЯ: Приведение к нижнему регистру

ПРЕДУСЛОВИЕ (Pre):
- Входная строка не null и не пустая

ПОСТУСЛОВИЕ (Post):
- Все символы строки в нижнем регистре
- Результат должен быть равен input.ToLower()

ГРАНИЧНЫЕ ПРИМЕРЫ:
✅ Валидный: 'Hello WORLD' → 'hello world'
❌ Невалидный: '' (пустая строка) → предусловие не выполнено";

            MessageBox.Show(contract, "Контракт операции", MessageBoxButton.OK, MessageBoxImage.Information);
        });

        public string ContractDescription => @"Pre: input != null и не пустая
Post: все символы в нижнем регистре";

        public string ValidExample => "Вход: 'Hello WORLD' → Выход: 'hello world'";
        public string InvalidExample => "Вход: '' → Ошибка: предусловие не выполнено";
    }

    public class RemoveEmptyLinesOperationViewModel : ViewModelBase, IOperationViewModel
    {
        private string _inputText = "";
        private string _outputText = "";
        private bool _operationExecuted = false;

        /// <summary>Отображаемое имя операции</summary>
        public string Name => "Удаление пустых строк";

        /// <summary>
        /// Входной текст с уведомлением об изменении
        /// При изменении сбрасывает флаг выполнения и результат
        /// </summary>
        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PreConditionSatisfied));
                _operationExecuted = false;
                OutputText = "";
                OnPropertyChanged(nameof(PostConditionSatisfied));
            }
        }

        /// <summary>
        /// Результат обработки (только для чтения)
        /// </summary>
        public string OutputText
        {
            get => _outputText;
            private set
            {
                _outputText = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PostConditionSatisfied));
            }
        }

        /// <summary>
        /// Проверка предусловия: входной текст не должен быть пустым или состоять только из пробелов
        /// </summary>
        public bool PreConditionSatisfied => !string.IsNullOrWhiteSpace(InputText);

        /// <summary>
        /// Проверка постусловия: 
        /// - Операция должна быть выполнена
        /// - Результат не должен содержать двойных пробелов
        /// - Результат должен быть обрезан по краям
        /// </summary>
        public bool PostConditionSatisfied
        {
            get
            {
                if (!_operationExecuted || string.IsNullOrEmpty(OutputText))
                    return false;

                return !OutputText.Contains("\n\n") &&
                       !OutputText.Contains("\r\n\r\n") &&
                       OutputText.Length <= InputText.Length;
            }
        }

        /// <summary>
        /// Команда выполнения операции
        /// Проверяет предусловия, выполняет операцию и проверяет постусловия
        /// </summary>
        public ICommand ExecuteCommand => new RelayCommand(() =>
        {
            if (!PreConditionSatisfied)
            {
                MessageBox.Show("Предусловие не выполнено! Введите текст.", "Ошибка");
                return;
            }

            try
            {
                OutputText = TextPipeline.RemoveEmptyLines(InputText);
                _operationExecuted = true;
                OnPropertyChanged(nameof(PostConditionSatisfied));
                //MessageBox.Show("Операция выполнена успешно!", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка выполнения");
                _operationExecuted = false;
                OnPropertyChanged(nameof(PostConditionSatisfied));
            }
        });

        /// <summary>
        /// Команда показа контракта операции
        /// </summary>
        public ICommand ShowContractCommand => new RelayCommand(() =>
        {
            string contract = @"ОПЕРАЦИЯ: Удаление пустых строк

ПРЕДУСЛОВИЕ (Pre):
- Входная строка не null и не пустая

ПОСТУСЛОВИЕ (Post):
- Все пустые строки удалены
- Сохранён порядок непустых строк
- Результат не длиннее исходной строки

ГРАНИЧНЫЕ ПРИМЕРЫ:
✅ Валидный: 'line1\n\nline2' → 'line1\nline2'
❌ Невалидный: '' (пустая строка) → предусловие не выполнено";

            MessageBox.Show(contract, "Контракт операции", MessageBoxButton.OK, MessageBoxImage.Information);
        });

        public string ContractDescription => @"Pre: input != null и не пустая
Post: пустые строки удалены, порядок сохранён";

        public string ValidExample => "Вход: 'line1\n\nline2' → Выход: 'line1\nline2'";
        public string InvalidExample => "Вход: '' → Ошибка: предусловие не выполнено";
    }
}