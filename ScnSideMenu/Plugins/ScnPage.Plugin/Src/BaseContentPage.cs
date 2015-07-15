using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ScnPage.Plugin.Forms
{
    public class BaseContentPage : ContentPage
    {
		private BaseContentUI contentUI;
        public BaseContentUI ContentUI
        {
            get { return contentUI; }
        }

		private BaseViewModel viewModel;
        public BaseViewModel ViewModel
        {
            get { return viewModel; }
        }

        //base layout
        protected RelativeLayout baseLayout;

        //layout with loading progressbar
		private StackLayout loadingLayout;

        //layout for custom content
		private StackLayout contentLayout;
        public StackLayout ContentLayout
        {
            get { return contentLayout; } 
        }

        //toolbar
		private IList<ToolbarItem> toolbar;
        public IList<ToolbarItem> Toolbar
        {
            get
            {
                return toolbar ??
                    (toolbar = new List<ToolbarItem>());
            }
        }

        public BaseContentPage()
        {
            InitContentLayout();
        }

        public BaseContentPage(Type viewModelType, Type contentUIType)
        {
            viewModel = (BaseViewModel)Activator.CreateInstance(viewModelType);
            contentUI = (BaseContentUI)Activator.CreateInstance(contentUIType);

            //Binding ContentUI with ViewModel
            viewModel.SetPage(this, contentUI);

            //Set binding model.
            BindingContext = viewModel;

            //Binding property for screen title 
            this.SetBinding(TitleProperty, "Title");

            this.SetBinding(IsBusyProperty, "IsLoadBusy");

            InitContentLayout();
            InitLoadingLayout();
        }

        private void InitContentLayout()
        {
            baseLayout = new RelativeLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            Content = baseLayout;

            contentLayout = new StackLayout
            {
                Padding = new Thickness(0),
                Spacing = 0
            };

            baseLayout.Children.Add(contentLayout, 
                Constraint.Constant(0), 
                Constraint.Constant(0), 
                Constraint.RelativeToParent(parent => { return parent.Width; }), 
                Constraint.RelativeToParent(parent => { return parent.Height; }));
        }
        
        private void InitLoadingLayout()
        {
            loadingLayout = new StackLayout
            {
                BackgroundColor = new Color(0, 0, 0, 0.8)
            };
            loadingLayout.SetBinding(IsVisibleProperty, "IsLoadActivity");

            var activityIndicator = new ActivityIndicator
            {
                Color = Device.OnPlatform(Color.White, Color.Default, Color.Default),
                VerticalOptions = LayoutOptions.EndAndExpand
            };
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoadActivity");
            if (Device.OS == TargetPlatform.Android)
                activityIndicator.HorizontalOptions = LayoutOptions.CenterAndExpand;

            var activityText = new Label
            {
                TextColor = Device.OnPlatform(Color.White, Color.Default, Color.Default),
                Text = contentUI.TxtLoading,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            loadingLayout.Children.Add(activityIndicator);
            loadingLayout.Children.Add(activityText);

            baseLayout.Children.Add(loadingLayout, Constraint.Constant(0), Constraint.Constant(0), Constraint.RelativeToParent(parent => { return parent.Width; }), Constraint.RelativeToParent(parent => { return parent.Height; }));        
        }

        public void ReloadContentLayout()
        {
            var tmpLayout = Content;
            Content = null;
            Content = tmpLayout;
        }

        public void ShowToolBar()
        {
            ToolbarItems.Clear();

            foreach (var item in Toolbar)
                ToolbarItems.Add(item);
        }

        public void HideToolBar()
        {
            ToolbarItems.Clear();
        }

        public void LoadingProcessSwitchGUI()
        {
            NavigationPage.SetHasBackButton(this, !ViewModel.IsLoading);

            if (ViewModel.IsLoading)
                HideToolBar();
            else
                ShowToolBar();
        }

        protected override bool OnBackButtonPressed()
        {
            bool IsBackPress = viewModel.IsLoading;

            return IsBackPress;
        }
    }
}
