using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ScnGesture.Plugin.Forms
{
    public class BoxViewGesture : BoxView
    {
        public BoxViewGesture(View owner)
        {
            Owner = owner;
        }

        public View Owner { get; private set; }

        public object GestureSender()
        {
            if (Owner == null)
                return this;

            if ((Owner is ViewGesture) && ((Owner as ViewGesture).Content != null))
                return (Owner as ViewGesture).Content;
            else
                return this;
        }

        public enum GestureType { gtTap, gtLongType, gtSwipe, gtDrag };

        #region Main gesture
        public event EventHandler TouchBegan;
        public void OnTouchBegan(GestureType gestureType)
        {
            if (TouchBegan != null) TouchBegan(GestureSender(), EventArgs.Empty);
        }

        public event EventHandler Touch;
        public void OnTouch(GestureType gestureType)
        {
            if (Touch != null) Touch(GestureSender(), EventArgs.Empty);
        }

        public event EventHandler TouchMoved;
        public void OnTouchMoved(GestureType gestureType)
        {
            if (TouchMoved != null) TouchMoved(GestureSender(), EventArgs.Empty);
        }

        public event EventHandler TouchEnded;
        public void OnTouchEnded(GestureType gestureType)
        {
            if (TouchEnded != null) TouchEnded(GestureSender(), EventArgs.Empty);
        }
        #endregion

        #region Tap gesture
        public event EventHandler TapBegan;
        public void OnTapBegan()
        {
            if (TapBegan != null) 
                TapBegan(GestureSender(), EventArgs.Empty);
            OnTouchBegan(GestureType.gtTap);
        }

        public event EventHandler Tap;
        public void OnTap()
        {
            if (Tap != null) 
                Tap(GestureSender(), EventArgs.Empty);
            OnTouch(GestureType.gtTap);
        }

        public event EventHandler TapEnded;
        public void OnTapEnded()
        {
            if (TapEnded != null) 
                TapEnded(GestureSender(), EventArgs.Empty);
            OnTouchEnded(GestureType.gtTap);
        }

        public event EventHandler TapMoved;
        public void OnTapMoved()
        {
            if (TapMoved != null)
                TapMoved(GestureSender(), EventArgs.Empty);
            else
                OnTapEnded();
        }
        #endregion

        #region LongTap gesture
        public event EventHandler LongTapBegan;
        public void OnLongTapBegan()
        {
            if (LongTapBegan != null)
                LongTapBegan(GestureSender(), EventArgs.Empty);
            OnTouchBegan(GestureType.gtLongType);
        }

        public event EventHandler LongTap;
        public void OnLongTap()
        {
            if (LongTap != null) 
                LongTap(GestureSender(), EventArgs.Empty);
            OnTouch(GestureType.gtLongType);
        }

        public event EventHandler LongTapMoved;
        public void OnLongTapMoved()
        {
            if (LongTapMoved != null) 
                LongTapMoved(GestureSender(), EventArgs.Empty);
            OnTouchMoved(GestureType.gtLongType);
        }

        public event EventHandler LongTapEnded;
        public void OnLongTapEnded()
        {
            if (LongTapEnded != null) 
                LongTapEnded(GestureSender(), EventArgs.Empty);
            OnTouchEnded(GestureType.gtLongType);
        }
        #endregion

        #region Swipe gesture
        public event EventHandler SwipeBegan;
        public void OnSwipeBegan()
        {
            if (SwipeBegan != null) 
                SwipeBegan(GestureSender(), EventArgs.Empty);
            OnTouchBegan(GestureType.gtSwipe);
        }

        public event EventHandler Swipe;
        public void OnSwipe()
        {
            if (Swipe != null) 
                Swipe(GestureSender(), EventArgs.Empty);
            OnTouch(GestureType.gtSwipe);
        }

        public event EventHandler SwipeMoved;
        public void OnSwipeMoved()
        {
            if (SwipeMoved != null)
                SwipeMoved(GestureSender(), EventArgs.Empty);
            OnTouchMoved(GestureType.gtSwipe);
        }

        public event EventHandler SwipeEnded;
        public void OnSwipeEnded()
        {
            if (SwipeEnded != null) 
                SwipeEnded(GestureSender(), EventArgs.Empty);
            OnTouchEnded(GestureType.gtSwipe);
        }
        #endregion
    }
}
