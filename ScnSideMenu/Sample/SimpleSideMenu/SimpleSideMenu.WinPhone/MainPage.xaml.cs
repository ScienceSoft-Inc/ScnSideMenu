using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using ScnViewGestures.Plugin.Forms.WinPhone.Renderers;

namespace SimpleSideMenu.WinPhone
{
	public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
	{
		public MainPage ()
		{
			InitializeComponent ();
			SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

			global::Xamarin.Forms.Forms.Init ();
            ViewGesturesRenderer.Init();

			LoadApplication (new SimpleSideMenu.App ());
		}
	}
}
