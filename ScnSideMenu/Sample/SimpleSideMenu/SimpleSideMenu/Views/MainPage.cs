using System;
using System.Collections.Generic;
using System.Text;

using ScnSideMenu.Forms;
using Xamarin.Forms;

namespace SimpleSideMenu.Views
{
    public class MainPage : SideBarPage
    {
        public MainPage() : base(PanelSetEnum.psLeftRight)
        {
            #region right menu
            var btnRightMenuShow = new Button
            {
                Text = "Right menu show",
            };
            btnRightMenuShow.Clicked += (s, e) => { IsShowRightPanel = !IsShowRightPanel; };
            
            //add button to main layout on page
            ContentLayout.Children.Add(btnRightMenuShow);

            //set width for right panel
            RightPanelWidth = 150;
            
            //add label to main layout on right panel
            RightPanel.AddToContext(
                new StackLayout
                {
                    Padding = new Thickness(32),
                    Children =
                    {
                        new Label
                        {
                            Text = "right menu",
                            TextColor = Color.Red,
                        }
                    }
                });
            RightPanel.BackgroundColor = Color.Blue;
            
            #endregion
            
            #region left menu
            var btnLeftMenuShow = new Button
            {
                Text = "Left menu show",
            };
            btnLeftMenuShow.Clicked += (s, e) => { IsShowLeftPanel = !IsShowLeftPanel; };

            ContentLayout.Children.Add(btnLeftMenuShow);

            LeftPanel.BackgroundColor = Color.Yellow;
            LeftPanel.AddToContext(
                new StackLayout
                {
                    Padding = new Thickness(32),
                    Children =
                    {
                        new Label
                        {
                            Text = "left menu",
                            TextColor = Color.Green,
                        }
                    }
                });
            #endregion
        }
    }
}
