using System;
using Xamarin.Forms;
using ScnGesture.Plugin.Forms;

namespace ScnSideMenu.Forms
{
    public class SideBarPanel : AbsoluteLayout
    {
        public SideBarPanel()
        {
            BackgroundColor = Color.White;
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.EndAndExpand;

            panelLayout = new RelativeLayout();
            SetLayoutFlags(panelLayout, AbsoluteLayoutFlags.All);
            SetLayoutBounds(panelLayout, new Rectangle(0f, 0f, 1f, 1f));
            Children.Add(panelLayout);

            CloseContext();

            if (Device.OS == TargetPlatform.WinPhone)
            {
                var tapSideBar = new TapGestureRecognizer();
                tapSideBar.Tapped += (sender, e) => { OnClick(); };
                GestureRecognizers.Add(tapSideBar);
            }
        }

        public event EventHandler Click;
        public void OnClick()
        {
            if (Click != null) Click(this, EventArgs.Empty);
        }

        private RelativeLayout panelLayout;
        private View previousView = null;

        public void ClearContext()
        {
            previousView = null;
            panelLayout.Children.Clear();
            CloseContext();
        }

        public void AddToContext(View view, bool inputTransparent = true)
        {
            panelLayout.Children.RemoveAt(panelLayout.Children.Count - 1);

            if (previousView != null)
            {
                panelLayout.Children.Add(view,
                    Constraint.Constant(0),
                    Constraint.RelativeToView(previousView, (parent, sibling) =>
                    {
                        return sibling.Y + sibling.Height;
                    }),
                    Constraint.RelativeToParent(parent => { return parent.Width; }));
            }
            else
            {
                panelLayout.Children.Add(view,
                    Constraint.Constant(0),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent(parent => { return parent.Width; }));
            }

            previousView = view;

            if (inputTransparent)
            {
                var boxGesture = new BoxViewGesture(null);
                boxGesture.Tap += (s, e) => { OnClick(); };
                boxGesture.Swipe += (s, e) => { OnClick(); };
                panelLayout.Children.Add(boxGesture,
                    Constraint.Constant(0),
                    Constraint.RelativeToView(view, (parent, sibling) =>
                    {
                        return sibling.Y;
                    }),
                    Constraint.RelativeToParent(parent => { return parent.Width; }),
                    Constraint.RelativeToView(view, (parent, sibling) =>
                    {
                        return sibling.Y + sibling.Height;
                    }));
            }

            CloseContext();
        }

        private void CloseContext()
        {
            var boxGesture = new BoxViewGesture(null);
            boxGesture.Tap += (s, e) => { OnClick(); };
            boxGesture.Swipe += (s, e) => { OnClick(); };
            if (previousView != null)
            {
                panelLayout.Children.Add(boxGesture,
                    Constraint.Constant(0),
                    Constraint.RelativeToView(previousView, (parent, sibling) =>
                    {
                        return sibling.Y + sibling.Height;
                    }),
                    Constraint.RelativeToParent(parent => { return parent.Width; }),
                    Constraint.RelativeToParent(parent => { return parent.Height; }));
            }
            else
            {
                panelLayout.Children.Add(boxGesture,
                    Constraint.Constant(0),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent(parent => { return parent.Width; }),
                    Constraint.RelativeToParent(parent => { return parent.Height; }));
            }
        }
    }
}
