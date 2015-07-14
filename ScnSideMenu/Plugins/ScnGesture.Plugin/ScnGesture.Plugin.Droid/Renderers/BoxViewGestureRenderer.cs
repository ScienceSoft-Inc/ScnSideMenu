using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ScnGesture.Plugin.Forms;
using ScnGesture.Plugin.Forms.Droid.Renderers;

[assembly: ExportRenderer(typeof(BoxViewGesture), typeof(BoxViewGestureRenderer))]

namespace ScnGesture.Plugin.Forms.Droid.Renderers
{
    public class BoxViewGestureRenderer : BoxRenderer
    {
        private readonly GestureListener _listener;
        private readonly GestureDetector _detector;

        public BoxViewGestureRenderer()
        {
            _listener = new GestureListener();
            _detector = new GestureDetector(_listener);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            var boxViewGesture = (BoxViewGesture)Element;

            _listener.OnTapBegan = (() => boxViewGesture.OnTapBegan());
            _listener.OnTap = (() => boxViewGesture.OnTap());
            _listener.OnTapEnded = (() => boxViewGesture.OnTapEnded());

            _listener.OnSwipe = (() => boxViewGesture.OnSwipe());

            if (e.NewElement == null)
            {
                /*if (this.GenericMotion != null)
                {
                    this.GenericMotion -= HandleGenericMotion;
                }
                if (this.Touch != null)
                {
                    this.Touch -= HandleTouch;
                }*/
            }

            if (e.OldElement == null)
            {
                this.GenericMotion += HandleGenericMotion;
                this.Touch += HandleTouch;
            }
        }
        
        void HandleTouch(object sender, TouchEventArgs e)
        {
            _detector.OnTouchEvent(e.Event);
        }

        void HandleGenericMotion(object sender, GenericMotionEventArgs e)
        {
            _detector.OnTouchEvent(e.Event);
        }

        public class GestureListener : GestureDetector.SimpleOnGestureListener
        {
            public Action OnTapBegan;
            public Action OnTap;
            public Action OnTapEnded;

            public Action OnSwipe;

            public override void OnLongPress(MotionEvent e)
            {
                #if DEBUG
                Console.WriteLine("OnLongPress");
                #endif

                base.OnLongPress(e);
            }

            public override bool OnDoubleTap(MotionEvent e)
            {
                #if DEBUG
                Console.WriteLine("OnDoubleTap");
                #endif

                return base.OnDoubleTap(e);
            }

            public override bool OnDoubleTapEvent(MotionEvent e)
            {
                #if DEBUG
                Console.WriteLine("OnDoubleTapEvent");
                #endif

                return base.OnDoubleTapEvent(e);
            }

            public override bool OnSingleTapUp(MotionEvent e)
            {
                #if DEBUG
                Console.WriteLine("OnSingleTapUp");
                #endif

                if (OnTap != null)
                    OnTap();

                return base.OnSingleTapUp(e);
            }

            public override bool OnDown(MotionEvent e)
            {
                #if DEBUG
                Console.WriteLine("OnDown");
                #endif

                if (OnTapBegan != null)
                    OnTapBegan();

                return base.OnDown(e);
            }

            public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
            {
                #if DEBUG
                Console.WriteLine("OnFling");
                #endif

                if (OnTap != null)
                    OnTap();

                if (OnTapEnded != null)
                    OnTapEnded();

                return base.OnFling(e1, e2, velocityX, velocityY);
            }

            public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
            {
                #if DEBUG
                Console.WriteLine("OnScroll");
                #endif

                if (OnSwipe != null)
                    OnSwipe();

                if (OnTapEnded != null)
                    OnTapEnded();

                return base.OnScroll(e1, e2, distanceX, distanceY);
            }

            public override void OnShowPress(MotionEvent e)
            {
                #if DEBUG
                Console.WriteLine("OnShowPress");
                #endif

                base.OnShowPress(e);
            }

            public override bool OnSingleTapConfirmed(MotionEvent e)
            {
                #if DEBUG
                Console.WriteLine("OnSingleTapConfirmed");
                #endif

                if (OnTapEnded != null)
                    OnTapEnded();

                return base.OnSingleTapConfirmed(e);
            }
        }
    }
}