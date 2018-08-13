using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ScnPage.Plugin.Forms.Helpers
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        #region PropertyChanged pattern
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
