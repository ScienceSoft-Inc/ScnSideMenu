using ScnSideMenu.Forms;
using Xamarin.Forms;

namespace SimpleSideMenu.Views
{
    public class MainPage : SideBarPage
    {
        public MainPage()
            : base(PanelSetEnum.psLeftRight)
        {
            #region left menu
            var btnLeftMenuShow = new Button
            {
                Text = "Left menu show"
            };
            btnLeftMenuShow.Clicked += (s, e) => IsShowLeftPanel = !IsShowLeftPanel;

            ContentLayout.Children.Add(btnLeftMenuShow);

            LeftPanel.BackgroundColor = Color.LawnGreen;
            LeftPanel.Content = new StackLayout
            {
                Padding = 32,
                Children =
                {
                    new Label
                    {
                        Text = "left menu",
                        TextColor = Color.Black
                    }
                }
            };
            #endregion

            #region right menu
            RightPanelWidthRequest = 250;

            var btnRightMenuShow = new Button
            {
                Text = "Right menu show"
            };
            btnRightMenuShow.Clicked += (s, e) => IsShowRightPanel = !IsShowRightPanel;

            ContentLayout.Children.Add(btnRightMenuShow);

            var rightPanelContent = new StackLayout
            {
                Padding = 32
            };

            for (var i = 0; i < 100; i++)
            {
                var view = CreateComplexView($"right menu {i}");
                rightPanelContent.Children.Add(view);
            }

            RightPanel.BackgroundColor = Color.Blue;
            RightPanel.Content = rightPanelContent;
            #endregion

            //set right swipe reaction panel to be wider
            RightSwipeSize = 20;

            //set transparent spaces (to skip nav bar, or bottom bar) when panel is swiped
            TransparentSize = new Thickness(50, 0, 50, 20);
        }

        private static View CreateComplexView(string text)
        {
            return new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Label
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Text = text
                    },
                    new Switch
                    {
                        HorizontalOptions = LayoutOptions.End
                    }
                }
            };
        }
    }
}
