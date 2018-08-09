using ScnSideMenu.Forms;
using Xamarin.Forms;

namespace SimpleSideMenu.Views
{
    public class MainPage : SideBarPage
    {
        public MainPage()
            : base(PanelSetEnum.psLeftRight)
        {
            #region right menu
            var btnRightMenuShow = new Button
            {
                Text = "Right menu show"
            };
            btnRightMenuShow.Clicked += (s, e) => IsShowRightPanel = !IsShowRightPanel;
            
            //add button to main layout on page
            ContentLayout.Children.Add(btnRightMenuShow);

            //set width for right panel
            RightPanelWidth = 250;
            
            RightPanel.BackgroundColor = Color.Blue;
            RightPanel.Content = new StackLayout
            {
                Padding = 32,
                Children =
                {
                    new Label
                    {
                        Text = "right menu 1",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 2",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 3",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 4",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 5",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 6",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 7",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 8",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 1",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 2",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 3",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 4",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 5",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 6",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 7",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 8",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 1",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 2",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 3",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 4",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 5",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 6",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 7",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 8",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 1",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 2",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 3",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 4",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 5",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 6",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 7",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 8",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 1",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 2",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 3",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 4",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 5",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 6",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 7",
                        TextColor = Color.Red
                    },
                    new Switch(),
                    new Label
                    {
                        Text = "right menu 8",
                        TextColor = Color.Red
                    },
                    new Switch()
                }
            };

            #endregion

            #region left menu
            var btnLeftMenuShow = new Button
            {
                Text = "Left menu show"
            };
            btnLeftMenuShow.Clicked += (s, e) => IsShowLeftPanel = !IsShowLeftPanel;

            ContentLayout.Children.Add(btnLeftMenuShow);

            LeftPanel.BackgroundColor = Color.Yellow;
            LeftPanel.Content = new StackLayout
            {
                Padding = 32,
                Children =
                {
                    new Label
                    {
                        Text = "left menu",
                        TextColor = Color.Green
                    }
                }
            };
            #endregion
        }
    }
}
