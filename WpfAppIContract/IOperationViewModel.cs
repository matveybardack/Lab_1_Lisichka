using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfAppIContract
{
    /// <summary>
    /// Интерфейс для ViewModel операций текстового конвейера
    /// Определяет контракт, который должны реализовать все ViewModel операций
    /// </summary>
    public interface IOperationViewModel
    {
        /// <summary>Отображаемое имя операции</summary>
        string Name { get; }

        /// <summary>Входной текст для обработки (TwoWay binding)</summary>
        string InputText { get; set; }

        /// <summary>Результат обработки (OneWay binding)</summary>
        string OutputText { get; }

        /// <summary>Команда для выполнения операции</summary>
        ICommand ExecuteCommand { get; }

        /// <summary>Команда для показа контракта операции</summary>
        ICommand ShowContractCommand { get; }

        /// <summary>Флаг выполнения предусловия (для индикатора)</summary>
        bool PreConditionSatisfied { get; }

        /// <summary>Флаг выполнения постусловия (для индикатора)</summary>
        bool PostConditionSatisfied { get; }

        /// <summary>Описание контракта операции</summary>
        string ContractDescription { get; }

        /// <summary>Пример валидного выполнения</summary>
        string ValidExample { get; }

        /// <summary>Пример невалидного выполнения</summary>
        string InvalidExample { get; }
    }
}
