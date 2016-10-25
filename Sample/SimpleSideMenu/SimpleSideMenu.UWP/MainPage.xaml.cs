using ScnViewGestures.UWP.Renderers;

namespace SimpleSideMenu.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
			ViewGesturesRenderer.Init();
			LoadApplication(new SimpleSideMenu.App());
		}
    }
}