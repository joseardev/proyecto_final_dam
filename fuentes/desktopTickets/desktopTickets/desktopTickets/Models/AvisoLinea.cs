using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace desktopTickets.Models
{
    // Clase AvisoLinea que implementa la interfaz INotifyPropertyChanged.
    // Esta interfaz es crucial para la actualización automática de la UI en arquitecturas MVVM.
    public class AvisoLinea : INotifyPropertyChanged
    {
        private int _id;
        private int _numeroLinea;
        private string _detalles;
        private string _estado;

        // Propiedad ID con métodos getter y setter.
        // Usa SetProperty para asignar el valor y notificar los cambios.
        public int ID
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
     
        public int NumeroLinea
        {
            get => _numeroLinea;
            set => SetProperty(ref _numeroLinea, value);
        }
        
        public string Detalles
        {
            get => _detalles;
            set => SetProperty(ref _detalles, value);
        }

        public string Estado
        {
            get { return _estado; }
            set => SetProperty(ref _estado, value);
        }

        // Evento PropertyChanged que se dispara cuando una propiedad cambia.
        public event PropertyChangedEventHandler PropertyChanged;

        // Método OnPropertyChanged que invoca el evento PropertyChanged.
        // [CallerMemberName] permite obtener el nombre del miembro que llama al método.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Método SetProperty que se utiliza para cambiar el valor de una propiedad.
        // Comprueba si el valor es diferente al actual antes de asignarlo y notificar el cambio.
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
