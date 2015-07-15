using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ScnGesture.Plugin.Forms;
using ScnGesture.Plugin.Forms.iOS.Renderers;

[assembly: ExportRenderer(typeof(BoxViewGesture), typeof(BoxViewGestureRenderer))]

namespace ScnGesture.Plugin.Forms.iOS.Renderers
{
    public class BoxViewGestureRenderer : BoxRenderer
    {
        public static void Init() { }

        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);
            var boxViewGesture = (BoxViewGesture)Element;

            var tapGestureRecognizer = new TapGestureRecognizer(() => boxViewGesture.OnTap())
            {
                OnTouchesBegan = (() => boxViewGesture.OnTapBegan()),
                OnTouchesEnded = (() => boxViewGesture.OnTapEnded()),
                OnTouchesMoved = (() => boxViewGesture.OnTapMoved()),
            };

            var longPressGestureRecognizer = new LongPressGestureRecognizer(() => boxViewGesture.OnLongTap())
            {
                OnTouchesBegan = (() => boxViewGesture.OnLongTapBegan()),
                OnTouchesEnded = (() => boxViewGesture.OnLongTapEnded()),
                OnTouchesMoved = (() => boxViewGesture.OnLongTapMoved()),
            };

            var swipeGestureRecognizer = new SwipeGestureRecognizer(() => boxViewGesture.OnSwipe())
            {
                OnTouchesBegan = (() => boxViewGesture.OnSwipeBegan()),
                OnTouchesEnded = (() => boxViewGesture.OnSwipeEnded()),
                OnTouchesMoved = (() => boxViewGesture.OnSwipeMoved()),
            };

            if (e.NewElement == null)
            {
                if (tapGestureRecognizer != null)
                    this.RemoveGestureRecognizer(tapGestureRecognizer);

                if (longPressGestureRecognizer != null)
                    this.RemoveGestureRecognizer(longPressGestureRecognizer);

                if (swipeGestureRecognizer != null)
                    this.RemoveGestureRecognizer(swipeGestureRecognizer);
            }

            if (e.OldElement == null)
            {
                this.AddGestureRecognizer(tapGestureRecognizer);
                this.AddGestureRecognizer(longPressGestureRecognizer);
                this.AddGestureRecognizer(swipeGestureRecognizer);
            }
        }

        class TapGestureRecognizer: UITapGestureRecognizer
        {
            public TapGestureRecognizer(Action action)
                : base(action)
            { }

            public override void TouchesBegan(NSSet touches, UIEvent evt)
            {
                base.TouchesBegan(touches, evt);

                if (OnTouchesBegan != null) 
                    OnTouchesBegan();
            }

            public override void TouchesEnded(NSSet touches, UIEvent evt)
            {
                base.TouchesEnded(touches, evt);

                if (OnTouchesEnded != null) 
                    OnTouchesEnded();
            }

            public override void TouchesMoved(NSSet touches, UIEvent evt)
            {
                base.TouchesMoved(touches, evt);

                if (OnTouchesMoved != null) 
                    OnTouchesMoved();
            }

            public Action OnTouchesBegan;
            public Action OnTouchesEnded;
            public Action OnTouchesMoved;
        }

        class LongPressGestureRecognizer  : UILongPressGestureRecognizer 
        {
            public LongPressGestureRecognizer(Action action)
                : base(action)
            { }

            public override void TouchesBegan(NSSet touches, UIEvent evt)
            {
                base.TouchesBegan(touches, evt);

                if (OnTouchesBegan != null) 
                    OnTouchesBegan();
            }

            public override void TouchesEnded(NSSet touches, UIEvent evt)
            {
                base.TouchesEnded(touches, evt);

                if (OnTouchesEnded != null) 
                    OnTouchesEnded();
            }

            public override void TouchesMoved(NSSet touches, UIEvent evt)
            {
                base.TouchesMoved(touches, evt);

                if (OnTouchesMoved != null)
                    OnTouchesMoved();
            }

            public Action OnTouchesBegan;
            public Action OnTouchesEnded;
            public Action OnTouchesMoved;
        }

        class SwipeGestureRecognizer : UISwipeGestureRecognizer
        {
            public SwipeGestureRecognizer(Action action)
                : base(action)
            { }

            public override void TouchesBegan(NSSet touches, UIEvent evt)
            {
                base.TouchesBegan(touches, evt);

                if (OnTouchesBegan != null) 
                    OnTouchesBegan();
            }

            public override void TouchesEnded(NSSet touches, UIEvent evt)
            {
                base.TouchesEnded(touches, evt);

                if (OnTouchesEnded != null) 
                    OnTouchesEnded();
            }

            public override void TouchesMoved(NSSet touches, UIEvent evt)
            {
                base.TouchesMoved(touches, evt);

                if (OnTouchesMoved != null)
                    OnTouchesMoved();
            }

            public Action OnTouchesBegan;
            public Action OnTouchesEnded;
            public Action OnTouchesMoved;
        }

    }
}