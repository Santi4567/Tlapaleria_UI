//Clase padre encargado de los Estados dentro de la Interfaz de Usuario (Equivalente al los UseStade de React)
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UITlapaleria.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        // 1. El evento que WPF escucha
        public event PropertyChangedEventHandler? PropertyChanged;

        // 2. Método que dispara el evento
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 3. "setState" personalizado
        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}