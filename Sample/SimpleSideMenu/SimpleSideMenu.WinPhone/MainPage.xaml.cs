using ScnViewGestures.Plugin.Forms.WinRT.Renderers;

namespace SimpleSideMenu.WinPhone
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