using System;
using Xamarin.Forms;
using ScnViewGestures.Plugin.Forms;

namespace ScnSideMenu.Forms
{
    public class SideBarPanel : AbsoluteLayout
    {
        public SideBarPanel(PanelAlignEnum panelAlign)
        {
            panelAlignEnum = panelAlign;

            BackgroundColor = Color.White;
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.EndAndExpand;

            panelLayout = new RelativeLayout();
            SetLayoutFlags(panelLayout, AbsoluteLayoutFlags.All);
            SetLayoutBounds(panelLayout, new Rectangle(0f, 0f, 1f, 1f));
            Children.Add(panelLayout);

            CloseContext();
        }

        private PanelAlignEnum panelAlignEnum;

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

            if (inputTransparent)
            {
                var viewGestures = new ViewGestures();
                viewGestures.Content = view;
                viewGestures.BackgroundColor = BackgroundColor;

                viewGestures.Tap += (s, e) => { OnClick(); };
                if (panelAlignEnum == PanelAlignEnum.paLeft)
                    viewGestures.SwipeLeft += (s, e) => { OnClick(); };
                else if (panelAlignEnum == PanelAlignEnum.paRight)
                    viewGestures.SwipeRight += (s, e) => { OnClick(); };

                AddView(viewGestures);
                previousView = viewGestures;
            }
            else
            {
                AddView(view);
                previousView = view;
            }

            CloseContext();
        }

        private void AddView(View view)
        {
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
        }

        private void CloseContext()
        {
            var viewGestures = new ViewGestures();
            viewGestures.BackgroundColor = BackgroundColor;

            viewGestures.Tap += (s, e) => { OnClick(); };
            if (panelAlignEnum == PanelAlignEnum.paLeft)
                viewGestures.SwipeLeft += (s, e) => { OnClick(); };
            else if (panelAlignEnum == PanelAlignEnum.paRight)
                viewGestures.SwipeRight += (s, e) => { OnClick(); };

            if (previousView != null)
            {
                panelLayout.Children.Add(viewGestures,
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
                panelLayout.Children.Add(viewGestures,
                    Constraint.Constant(0),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent(parent => { return parent.Width; }),
                    Constraint.RelativeToParent(parent => { return parent.Height; }));
            }
        }
    }
}
