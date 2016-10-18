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
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
