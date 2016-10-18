using System;
using Xamarin.Forms;
using ScnPage.Plugin.Forms.Helpers;

namespace ScnPage.Plugin.Forms
{
    public class BaseViewModel : NotifyPropertyChanged
	{
        public BaseContentPage ViewPage
        {
            get;
            private set;
        }

        public BaseContentUI ContentUI
        {
            get;
            private set;
        }

        //Caption back button in application bar
        private string appBarBackBtnTitle;

        #region IsLoading - property
		private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                IsNotLoading = !value;

                OnPropertyChanged();
            }
        }
        #endregion

        #region IsNotLoading - property
		private bool isNotLoading = true;
        public bool IsNotLoading
        {
            get { return isNotLoading; }
            set
            {
                isNotLoading = value;
                OnPropertyChanged();
            }
        }

        #endregion

        //Show loading layout and ban custom layout
        #region IsLoadActivity - property
		private bool isLoadActivity;
        public bool IsLoadActivity
        {
            get { return isLoadActivity; }
            set
            {
                IsLoading = value;

                NavigationPage.SetHasNavigationBar(ViewPage, !IsLoading);
                ViewPage.LoadingProcessSwitchGUI();

                isLoadActivity = value;
                OnPropertyChanged();
            }
        }
        #endregion

        //Show loading bar in action bar
        #region IsLoadBusy - property
		private bool isLoadBusy;
        public bool IsLoadBusy
        {
            get { return isLoadBusy; }
            set
            {
                IsLoading = value;

                var btnTitle = IsLoading ? ContentUI.TxtAwait : appBarBackBtnTitle;
                NavigationPage.SetBackButtonTitle(ViewPage, btnTitle);
                ViewPage.LoadingProcessSwitchGUI();

                isLoadBusy = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Title - property
		private string title = string.Empty;
        public string Title
        {
            get { return title; }
            set
            {
                title = value.ToUpper();
                OnPropertyChanged();
            }
        }
        #endregion

        public void SetPage(BaseContentPage basePage, BaseContentUI baseContentUI)
        {
            ViewPage = basePage;
            ContentUI = baseContentUI;

            Title = ContentUI.Title;
            appBarBackBtnTitle = NavigationPage.GetBackButtonTitle(ViewPage);

            InitProperty();

            ViewPage.Appearing += ViewPage_Appearing;
            InitLifecycle();
        }
  
        protected virtual void InitProperty()
        {}

        void ViewPage_Appearing(object sender, EventArgs e)
        {
            ViewPage.LoadingProcessSwitchGUI();
            OnResuming(this, EventArgs.Empty);
        }

        #region Lifecycle
        protected void InitLifecycle()
        {
            //CurrentApp.Resuming += OnResuming;
            //CurrentApp.Suspending += OnSuspending;
        }

        protected void ClearLifecycle()
        {
            //CurrentApp.Resuming -= OnResuming;
            //CurrentApp.Suspending -= OnSuspending;
        }

        protected virtual void OnResuming(object sender, EventArgs e)
        {
            //Implement in ViewModel
        }

        protected virtual void OnSuspending(object sender, EventArgs e)
        {
            //Implement in ViewModel
        }
        #endregion
	}
}

