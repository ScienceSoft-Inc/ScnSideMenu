using ScnPage.Plugin.Forms.Helpers;
using System;
using Xamarin.Forms;

namespace ScnPage.Plugin.Forms
{
    public class BaseViewModel : NotifyPropertyChanged
	{
        public BaseContentPage ViewPage { get; private set; }

	    public BaseContentUI ContentUI { get; private set; }

	    //Caption back button in application bar
        private string _appBarBackBtnTitle;

        #region IsLoading - property
		private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                IsNotLoading = !value;

                OnPropertyChanged();
            }
        }
        #endregion

        #region IsNotLoading - property
		private bool _isNotLoading = true;
        public bool IsNotLoading
        {
            get => _isNotLoading;
            set
            {
                _isNotLoading = value;
                OnPropertyChanged();
            }
        }
        #endregion

        //Show loading layout and ban custom layout
        #region IsLoadActivity - property
		private bool _isLoadActivity;
        public bool IsLoadActivity
        {
            get => _isLoadActivity;
            set
            {
                IsLoading = value;

                NavigationPage.SetHasNavigationBar(ViewPage, !IsLoading);
                ViewPage.LoadingProcessSwitchGUI();

                _isLoadActivity = value;
                OnPropertyChanged();
            }
        }
        #endregion

        //Show loading bar in action bar
        #region IsLoadBusy - property
		private bool _isLoadBusy;
        public bool IsLoadBusy
        {
            get => _isLoadBusy;
            set
            {
                IsLoading = value;

                var btnTitle = IsLoading ? ContentUI.TxtAwait : _appBarBackBtnTitle;
                NavigationPage.SetBackButtonTitle(ViewPage, btnTitle);
                ViewPage.LoadingProcessSwitchGUI();

                _isLoadBusy = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Title - property
		private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value?.ToUpper();
                OnPropertyChanged();
            }
        }
        #endregion

        public void SetPage(BaseContentPage basePage, BaseContentUI baseContentUI)
        {
            ViewPage = basePage;
            ContentUI = baseContentUI;

            Title = ContentUI.Title;
            _appBarBackBtnTitle = NavigationPage.GetBackButtonTitle(ViewPage);

            InitProperty();

            ViewPage.Appearing += ViewPage_Appearing;
            InitLifecycle();
        }

	    protected virtual void InitProperty()
	    {
	    }

	    private void ViewPage_Appearing(object sender, EventArgs e)
        {
            ViewPage.LoadingProcessSwitchGUI();
            OnResuming(this, EventArgs.Empty);
        }

        #region Lifecycle
        protected void InitLifecycle()
        {
        }

        protected void ClearLifecycle()
        {
        }

        protected virtual void OnResuming(object sender, EventArgs e)
        {
        }

        protected virtual void OnSuspending(object sender, EventArgs e)
        {
        }
        #endregion
	}
}

