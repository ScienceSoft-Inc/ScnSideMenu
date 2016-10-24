using ScnViewGestures.UWP.Renderers;

namespace SimpleSideMenu.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
			ViewGesturesRenderer.Init();
			LoadApplication(new SimpleSideMenu.App());
		}
    }
}