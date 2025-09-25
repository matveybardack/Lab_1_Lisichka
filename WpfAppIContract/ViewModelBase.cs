using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfAppIContract
{
    /// <summary>
    /// Базовый класс для всех ViewModel, реализует INotifyPropertyChanged
    /// Обеспечивает автоматическое уведомление интерфейса об изменениях свойств
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие, которое вызывается при изменении свойства
        /// WPF автоматически подписывается на это событие для обновления интерфейса
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод для вызова события PropertyChanged
        /// </summary>
        /// <param name="propertyName">
        /// Имя изменившегося свойства (автоматически подставляется компилятором)
        /// </param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // ?. - безопасный вызов (если нет подписчиков)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}