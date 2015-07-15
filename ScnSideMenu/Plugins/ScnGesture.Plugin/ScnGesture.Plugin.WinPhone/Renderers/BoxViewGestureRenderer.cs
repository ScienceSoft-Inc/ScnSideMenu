using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

using ScnGesture.Plugin.Forms;
using ScnGesture.Plugin.Forms.WinPhone.Renderers;

[assembly: ExportRenderer(typeof(BoxViewGesture), typeof(BoxViewGestureRenderer))]

namespace ScnGesture.Plugin.Forms.WinPhone.Renderers
{
    public class BoxViewGestureRenderer : BoxViewRenderer
    {
        public static void Init() { }
        
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);
        }
    }
}
