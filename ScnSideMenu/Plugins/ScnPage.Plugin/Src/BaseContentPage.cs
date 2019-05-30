using System;
using Xamarin.Forms;

namespace ScnPage.Plugin.Forms
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentUI ContentUI { get; }

        public BaseViewModel ViewModel { get; }

        //base layout
        protected RelativeLayout BaseLayout { get; private set; }

        //layout with loading progressbar
        public StackLayout LoadingLayout { get; private set; }

        public Label LoadingActivityText { get; private set; }

        public ActivityIndicator LoadingActivityIndicator { get; private set; }

        //layout for custom content
        public StackLayout ContentLayout { get; private set; }

        public event EventHandler Disposing;
        public void OnDisposing()
        {
            Disposing?.Invoke(this, EventArgs.Empty);
        }

        public BaseContentPage()
        {
            InitContentLayout();
        }

        public BaseContentPage(Type viewModelType, Type contentUIType)
        {
            ViewModel = (BaseViewModel)Activator.CreateInstance(viewModelType);
            ContentUI = (BaseContentUI)Activator.CreateInstance(contentUIType);

            //Binding ContentUI with ViewModel
            ViewModel.SetPage(this, ContentUI);

            //Set binding model.
            BindingContext = ViewModel;

            //Binding property for screen title 
            this.SetBinding(TitleProperty, "Title");

            this.SetBinding(IsBusyProperty, "IsLoadBusy");

            InitContentLayout();
            InitLoadingLayout();
        }

        private void InitContentLayout()
        {
            BaseLayout = new RelativeLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            Content = BaseLayout;

            ContentLayout = new StackLayout
            {
                Padding = new Thickness(0),
                Spacing = 0
            };

            BaseLayout.Children.Add(ContentLayout, 
                Constraint.Constant(0), 
                Constraint.Constant(0), 
                Constraint.RelativeToParent(parent => parent.Width), 
                Constraint.RelativeToParent(parent => parent.Height));
        }
        
        private void InitLoadingLayout()
        {
            LoadingLayout = new StackLayout
            {
                BackgroundColor = new Color(0, 0, 0, 0.8)
            };
            LoadingLayout.SetBinding(IsVisibleProperty, "IsLoadActivity");

            LoadingActivityIndicator = new ActivityIndicator
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.EndAndExpand
            };
            LoadingActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoadActivity");

            LoadingActivityText = new Label
            {
                Text = ContentUI.TxtLoading,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.StartAndExpand
            };

            LoadingLayout.Children.Add(LoadingActivityIndicator);
            LoadingLayout.Children.Add(LoadingActivityText);

            BaseLayout.Children.Add(LoadingLayout, 
                Constraint.Constant(0), 
                Constraint.Constant(0), 
                Constraint.RelativeToParent(parent => parent.Width), 
                Constraint.RelativeToParent(parent => parent.Height));        
        }

        public void ReloadContentLayout()
        {
            var tmpLayout = Content;
            Content = null;
            Content = tmpLayout;
        }

        protected override bool OnBackButtonPressed()
        {
            var isBackPress = false;
            
            if (ViewModel != null)
                isBackPress = ViewModel.IsLoading;

            return isBackPress;
        }

        #region OpenningLocker

        private static readonly object OpenningLocker = new object();

        private bool _isOpenning;
        public bool IsOpenning
        {
            get
            {
                lock (OpenningLocker)
                    return _isOpenning;
            }
            set
            {
                lock (OpenningLocker)
                    _isOpenning = value;
            }
        }

        #endregion

        public async void OpenPage(Page page, bool animated = true)
        {
            if (IsOpenning)
                return;

            try
            {
                IsOpenning = true;
                await Navigation.PushAsync(page, animated);
            }
            finally
            {
                IsOpenning = false;
            }
        }
    }
}
