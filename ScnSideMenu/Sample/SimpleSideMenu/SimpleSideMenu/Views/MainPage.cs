using System;
using System.Collections.Generic;
using System.Text;

using ScnSideMenu.Forms;
using SimpleSideMenu.ViewModels;
using SimpleSideMenu.Views.ContentUI;
using Xamarin.Forms;

namespace SimpleSideMenu.Views
{
    public class MainPage : SideBarPage
    {
        private MainViewModel viewModel
        {
            get { return (MainViewModel)BindingContext; }
        }

        private MainContentUI contentUI
        {
            get { return (MainContentUI)ContentUI; }
        }

        public MainPage()
            : base(typeof(MainViewModel), typeof(MainContentUI))
        {
            var btnRightMenu = new Button
            {
                Text = "Right menu",
            };
            btnRightMenu.Clicked += (s, e) => { IsShowRightPanel = !IsShowRightPanel; };

            ContentLayout.Children.Add(btnRightMenu);

            var btnLeftMenu = new Button
            {
                Text = "Left menu",
            };
            btnLeftMenu.Clicked += (s, e) => { IsShowLeftPanel = !IsShowLeftPanel; };

            ContentLayout.Children.Add(btnLeftMenu);
        }
    }
}
