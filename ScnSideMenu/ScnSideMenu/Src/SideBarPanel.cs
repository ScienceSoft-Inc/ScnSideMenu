using System;
using Xamarin.Forms;

namespace ScnSideMenu.Forms
{
    public class SideBarPanel : ContentView
    {
        public PanelAlignEnum PanelAlign { get; }

        public event EventHandler Tap;
        public event EventHandler Swipe;

        public new View Content
        {
            get => ((ContentView) ((ScrollView) base.Content).Content).Content;
            set => ((ContentView) ((ScrollView) base.Content).Content).Content = value;
        }

        public SideBarPanel(PanelAlignEnum panelAlign)
        {
            var contentView = new ContentView();

            base.Content = new ScrollView
            {
                Content = contentView
            };

            PanelAlign = panelAlign;

            BackgroundColor = Color.White;
            VerticalOptions = LayoutOptions.FillAndExpand;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, args) => OnTap();

            var panGestureRecognizer = new PanGestureRecognizer();
            panGestureRecognizer.PanUpdated += (sender, args) =>
            {
                if (args.StatusType == GestureStatus.Running &&
                    (args.TotalX > 40 && Math.Abs(args.TotalX) > Math.Abs(args.TotalY) &&
                     panelAlign == PanelAlignEnum.paRight ||
                     args.TotalX < -40 && Math.Abs(args.TotalX) > Math.Abs(args.TotalY) &&
                     panelAlign == PanelAlignEnum.paLeft))
                    OnSwipe();
            };

            var gestureContainer = Device.RuntimePlatform == Device.Android ? contentView : this;
            gestureContainer.GestureRecognizers.Add(tapGestureRecognizer);
            gestureContainer.GestureRecognizers.Add(panGestureRecognizer);
        }

        public void OnTap()
        {
            Tap?.Invoke(this, EventArgs.Empty);
        }

        public void OnSwipe()
        {
            Swipe?.Invoke(this, EventArgs.Empty);
        }
    }
}
