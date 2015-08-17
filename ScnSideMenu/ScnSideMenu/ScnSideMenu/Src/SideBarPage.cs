using System;
using Xamarin.Forms;
using ScnPage.Plugin.Forms;
using ScnViewGestures.Plugin.Forms;

namespace ScnSideMenu.Forms
{
    [Flags]
    public enum PanelSetEnum 
    { 
        psNone = 0,
        psLeft = 1, 
        psRight = 2,
        psLeftRight = psLeft | psRight,
    }

    public enum PanelAlignEnum { paLeft, paRight }

    public class SideBarPage : BaseContentPage
    {
        public SideBarPage(PanelSetEnum panelSet)
        {
            InitPanel(panelSet);
        }

        public SideBarPage(Type viewModelType, Type contentUIType, PanelSetEnum panelSet)
            : base(viewModelType, contentUIType)
        {
            InitPanel(panelSet);
        }

        private void InitPanel(PanelSetEnum panelSet)
        {
            if ((panelSet == PanelSetEnum.psLeft) || (panelSet == PanelSetEnum.psLeftRight))
                LeftPanel = new SideBarPanel(PanelAlignEnum.paLeft);

            if ((panelSet == PanelSetEnum.psRight) || (panelSet == PanelSetEnum.psLeftRight))
                RightPanel = new SideBarPanel(PanelAlignEnum.paRight);
        }
        
        //Parameter which used for calculation service spaces for gestures. 
        //Usual sidebar menu is shown through button on app bar so leave empty space near panel.
        private int _transparentSize = Device.OnPlatform(48, 48, 64);
        public int TransparentSize
        {
            get { return _transparentSize; }
            set { _transparentSize = value; }
        }

        static object locker = new object();

        #region Left panel
        private SideBarPanel leftPanel = null;
        public SideBarPanel LeftPanel 
        { 
            get { return leftPanel; }
            private set
            {
                leftPanel = value;

                leftPanel.Click += (s, e) => { ClosePanel(); };
                baseLayout.Children.Add(leftPanel,
                    Constraint.RelativeToParent(parent =>
                    {
                        if (_leftPanelWidth == 0)
                            _leftPanelWidth = parent.Width - _transparentSize;

                        return 0 - _leftPanelWidth;
                    }),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent(parent => { return _leftPanelWidth; }),
                    Constraint.RelativeToParent(parent => { return parent.Height; }));

                // space near border for showing panel of a finger
                #region Swipe panel
                var swipePanel = new BoxView
                {
                    BackgroundColor = Color.Transparent,
                };

                var viewGesture = new ViewGestures
                {
                    Content = swipePanel,
                    BackgroundColor = Color.Transparent,
                };

                viewGesture.SwipeRight += (s, e) => 
                {
                    if (!IsShowLeftPanel)
                        IsShowLeftPanel = true;
                };

                viewGesture.SwipeLeft += (s, e) =>
                {
                    IsShowLeftPanel = false;
                };

                viewGesture.Tap += (s, e) =>
                {
                    IsShowLeftPanel = false;
                };

                baseLayout.Children.Add(viewGesture,
                    Constraint.Constant(0),
                    Constraint.Constant(_transparentSize),
                    Constraint.Constant(Device.OnPlatform(10, 10, 15)),
                    Constraint.RelativeToView(leftPanel, (parent, sibling) =>
                    {
                        return sibling.Height - _transparentSize * 2;
                    }));
                #endregion

                // space near panel under active page
                #region Transparent panel
                var leftTransparentPanel = new BoxView
                {
                    BackgroundColor = Color.Transparent,
                };

                leftTransparentGestures = new ViewGestures
                {
                    IsVisible = false,
                    Content = leftTransparentPanel,
                    BackgroundColor = Color.Transparent,
                };

                leftTransparentGestures.SwipeRight += (s, e) =>
                {
                    IsShowLeftPanel = false;
                };

                leftTransparentGestures.SwipeLeft += (s, e) =>
                {
                    IsShowLeftPanel = false;
                };

                leftTransparentGestures.SwipeUp += (s, e) =>
                {
                    IsShowLeftPanel = false;
                };

                leftTransparentGestures.SwipeDown += (s, e) =>
                {
                    IsShowLeftPanel = false;
                };

                leftTransparentGestures.Tap += (s, e) =>
                {
                    IsShowLeftPanel = false;
                };

                baseLayout.Children.Add(leftTransparentGestures,
                    Constraint.RelativeToView(leftPanel, (parent, sibling) =>
                    {
                        return sibling.Width;
                    }),
                    Constraint.Constant(_transparentSize),
                    Constraint.RelativeToView(leftPanel, (parent, sibling) =>
                    {
                        return parent.Width - sibling.Width;
                    }),
                    Constraint.RelativeToParent(parent => { return parent.Height - _transparentSize * 2; }));
                #endregion
            }
        }

        private ViewGestures leftTransparentGestures;

        private bool _isShowLeftPanel;
        public bool IsShowLeftPanel
        {
            get
            {
                lock (locker)
                    return _isShowLeftPanel;
            }
            set
            {
                lock (locker)
                {
                    _isShowLeftPanel = value;
                    leftTransparentGestures.IsVisible = _isShowLeftPanel;
                    if (value)
                        ShowLeftPanel();
                    else
                        HideLeftPanel();

                    OnPanelChanged(new SideBarEventArgs(value, PanelAlignEnum.paLeft));
                }
            }
        }

        private double _leftPanelWidth;
        public double LeftPanelWidth
        {
            get { return _leftPanelWidth; }
            set { _leftPanelWidth = (value > 0) ? value : 0; }
        }
        #endregion

        #region Right panel
        private SideBarPanel rightPanel = null;
        public SideBarPanel RightPanel 
        { 
            get { return rightPanel; }
            private set 
            {
                rightPanel = value;

                rightPanel.Click += (s, e) => { ClosePanel(); };
                baseLayout.Children.Add(rightPanel,
                    Constraint.RelativeToParent(parent => { return parent.Width; }),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent(parent =>
                    {
                        if (_rightPanelWidth == 0)
                            _rightPanelWidth = parent.Width - _transparentSize;

                        return _rightPanelWidth;
                    }),
                    Constraint.RelativeToParent(parent => { return parent.Height; }));

                // space near border for showing panel of a finger 
                #region Swipe panel
                var swipePanel = new BoxView
                {
                    BackgroundColor = Color.Transparent,
                };

                var viewGesture = new ViewGestures
                {
                    Content = swipePanel,
                    BackgroundColor = Color.Transparent,
                };

                viewGesture.SwipeLeft += (s, e) =>
                {
                    if (!IsShowRightPanel)
                        IsShowRightPanel = true;
                };

                viewGesture.SwipeRight += (s, e) =>
                {
                    IsShowRightPanel = false;
                };

                viewGesture.Tap += (s, e) =>
                {
                    IsShowRightPanel = false;
                };

                baseLayout.Children.Add(viewGesture,
                    Constraint.RelativeToView(rightPanel, (parent, sibling) =>
                    {
                        return sibling.X - Device.OnPlatform(10, 10, 15);
                    }),
                    Constraint.Constant(_transparentSize),
                    Constraint.Constant(Device.OnPlatform(10, 10, 15)),
                    Constraint.RelativeToView(rightPanel, (parent, sibling) =>
                    {
                        return sibling.Height - _transparentSize * 2;
                    }));
                #endregion

                // space near panel under active page
                #region Transparent panel
                var rightTransparentPanel = new BoxView
                {
                    BackgroundColor = Color.Transparent,
                };

                rightTransparentGestures = new ViewGestures
                {
                    IsVisible = false,
                    Content = rightTransparentPanel,
                    BackgroundColor = Color.Transparent,
                };

                rightTransparentGestures.SwipeRight += (s, e) =>
                {
                    IsShowRightPanel = false;
                };

                rightTransparentGestures.SwipeLeft += (s, e) =>
                {
                    IsShowRightPanel = false;
                };

                rightTransparentGestures.SwipeUp += (s, e) =>
                {
                    IsShowRightPanel = false;
                };

                rightTransparentGestures.SwipeDown += (s, e) =>
                {
                    IsShowRightPanel = false;
                };

                rightTransparentGestures.Tap += (s, e) =>
                {
                    IsShowRightPanel = false;
                };

                baseLayout.Children.Add(rightTransparentGestures,
                    Constraint.Constant(0),
                    Constraint.Constant(_transparentSize),
                    Constraint.RelativeToView(rightPanel, (parent, sibling) =>
                    {
                        return parent.Width - sibling.Width;
                    }),
                    Constraint.RelativeToParent(parent => { return parent.Height - _transparentSize * 2; }));
                #endregion

            }
        }

        private ViewGestures rightTransparentGestures;

        private bool _isShowRightPanel;
        public bool IsShowRightPanel
        {
            get
            {
                lock (locker)
                    return _isShowRightPanel;
            }
            set
            {
                lock (locker)
                {
                    _isShowRightPanel = value;
                    rightTransparentGestures.IsVisible = _isShowRightPanel;

                    if (value)
                        ShowRightPanel();
                    else
                        HideRightPanel();

                    OnPanelChanged(new SideBarEventArgs(value, PanelAlignEnum.paLeft));
                }
            }
        }
        
        private double _rightPanelWidth;
        public double RightPanelWidth
        {
            get { return _rightPanelWidth; }
            set { _rightPanelWidth = (value > 0) ? value : 0; }
        }
        #endregion

        public event EventHandler<SideBarEventArgs> PanelChanged;
        public void OnPanelChanged(SideBarEventArgs e)
        {
            if (PanelChanged != null) PanelChanged(this, e);
        }

        private uint _speedAnimatePanel = 300;
        public uint SpeedAnimatePanel
        {
            get { return _speedAnimatePanel; }
            set { _speedAnimatePanel = (value > 0) ? value : 0; }
        }

        public void ClosePanel()
        {
            IsShowLeftPanel = false;
            IsShowRightPanel = false;
        }

        #region Panel animation
        async private void ShowLeftPanel()
        {
            IsShowRightPanel = false;

            if (LeftPanel != null)
                await LeftPanel.TranslateTo(_leftPanelWidth, LeftPanel.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
        }

        async private void HideLeftPanel()
        {
            if (LeftPanel != null)
                await LeftPanel.TranslateTo(0, LeftPanel.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
        }

        async private void ShowRightPanel()
        {
            IsShowLeftPanel = false;

            if (RightPanel != null)
                await RightPanel.TranslateTo(0 - _rightPanelWidth, RightPanel.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
        }

        async private void HideRightPanel()
        {
            if (RightPanel != null)
                await RightPanel.TranslateTo( _rightPanelWidth, RightPanel.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
        }
        #endregion

        protected override bool OnBackButtonPressed()
        {
            bool IsBackPress = IsShowLeftPanel || IsShowRightPanel;

            if (IsBackPress)
            {
                IsShowLeftPanel = false;
                IsShowRightPanel = false;
            }

            return IsBackPress || base.OnBackButtonPressed();
        }

        public class SideBarEventArgs : EventArgs
        {
            public readonly bool IsShow;
            public readonly PanelAlignEnum Panel;

            public SideBarEventArgs(bool isShow, PanelAlignEnum panel)
            {
                IsShow = isShow;
                Panel = panel;
            }
        }
    }
}
