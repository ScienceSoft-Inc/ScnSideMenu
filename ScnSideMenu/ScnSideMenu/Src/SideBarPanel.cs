using System;
using Xamarin.Forms;

namespace ScnSideMenu.Forms
{
    public class SideBarPanel : ContentView
    {
        private const int SwipeReactionValue = 40;

        public PanelAlignEnum PanelAlign { get; }
        
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

            var panGestureRecognizer = new PanGestureRecognizer();
            panGestureRecognizer.PanUpdated += (sender, args) =>
            {
                if (args.StatusType == GestureStatus.Running &&
                    (args.TotalX > SwipeReactionValue && Math.Abs(args.TotalX) > Math.Abs(args.TotalY) &&
                     panelAlign == PanelAlignEnum.paRight ||
                     args.TotalX < -SwipeReactionValue && Math.Abs(args.TotalX) > Math.Abs(args.TotalY) &&
                     panelAlign == PanelAlignEnum.paLeft))
                    OnSwipe();
            };

            var gestureContainer = Device.RuntimePlatform == Device.Android ? contentView : this;
            gestureContainer.GestureRecognizers.Add(panGestureRecognizer);
        }

        public void OnSwipe()
        {
            Swipe?.Invoke(this, EventArgs.Empty);
        }
    }
}
