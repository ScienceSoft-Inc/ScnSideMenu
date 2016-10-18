using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScnSideMenu.Forms
{
    public class MenuItemData : NotifyPropertyChanged
    {
        #region Item icon
        private string icon = "";
        public string Icon
        {
            get { return icon; }
            set 
            {
                icon = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Item title
        private string title = "";
        public string Title
        {
            get { return title; }
            set 
            { 
                title = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Item hint
        private string hint = "";
        public string Hint
        {
            get { return hint; }
            set 
            {
                hint = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Type page
        private Type typePage;
        public Type TypePage
        {
            get { return typePage; }
            set 
            { 
                typePage = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public string TypeName { get { return TypePage.ToString(); } }
    }
}
