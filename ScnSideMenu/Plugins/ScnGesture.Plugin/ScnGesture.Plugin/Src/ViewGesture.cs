using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ScnGesture.Plugin.Forms
{
    public class ViewGesture: RelativeLayout 
    {
        public ViewGesture()
        {
            boxViewGesture = new BoxViewGesture(this);

            boxViewGesture.TapBegan += boxGesture_PressBegan;

            boxViewGesture.TapEnded += boxGesture_PressEnded;
            boxViewGesture.LongTapEnded += boxGesture_PressEnded;
            boxViewGesture.SwipeEnded += boxGesture_PressEnded;
        }

        private BoxViewGesture boxViewGesture;
        public BoxViewGesture Gesture
        {
            get { return boxViewGesture; }
        }

        private double deformationValue = -10;
        public double DeformationValue
        {
            get { return deformationValue; }
            set { deformationValue = value; }
        }

        private View content = null;
        public View Content 
        { 
            get { return content; }
            set 
            {
                content = value;
                SetContent(); 
            }
        }

        private void SetContent()
        {
            this.Children.Clear();

            this.Children.Add(content,
                Constraint.Constant(0),
                Constraint.Constant(0));

            this.Children.Add(boxViewGesture,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToView(content, (parent, sibling) => { return sibling.Width; }),
                Constraint.RelativeToView(content, (parent, sibling) => { return sibling.Height; }));
        }

        async void boxGesture_PressBegan(object sender, EventArgs e)
        {
            await this.ScaleTo(1 + (DeformationValue / 100), 100, Easing.CubicOut);
        }

        async void boxGesture_PressEnded(object sender, EventArgs e)
        {
            await this.ScaleTo(1, 100, Easing.CubicOut);
        }
    }
}
