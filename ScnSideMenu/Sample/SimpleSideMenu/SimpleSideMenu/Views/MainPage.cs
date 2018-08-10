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
