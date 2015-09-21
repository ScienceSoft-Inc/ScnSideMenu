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
            Init(panelSet);
        }

        public SideBarPage(Type viewModelType, Type contentUIType, PanelSetEnum panelSet)
            : base(viewModelType, contentUIType)
        {
            Init(panelSet);
        }

        private void Init(PanelSetEnum panelSet)
        {
            if ((panelSet == PanelSetEnum.psLeft) || (panelSet == PanelSetEnum.psLeftRight))
                LeftPanel = new SideBarPanel(PanelAlignEnum.paLeft);

            if ((panelSet == PanelSetEnum.psRight) || (panelSet == PanelSetEnum.psLeftRight))
                RightPanel = new SideBarPanel(PanelAlignEnum.paRight);

            Disappearing += (s, e) => { ClosePanel(); };
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
        public enum SwipeMotionEnum { smNone, smLeft, smRight }
        private SwipeMotionEnum SwipeGestureCatch = SwipeMotionEnum.smNone;
        private int swipeReactionValue = Device.OnPlatform(40, 40, 50);

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

                leftSwipeGesture = new ViewGestures
                {
                    Content = swipePanel,
                    BackgroundColor = Color.Transparent,
                };

                leftSwipeGesture.Tap += (s, e) =>
                {
                    IsShowRightPanel = false;
                };

                leftSwipeGesture.Drag += (s, e) =>
                {
                    IsShowRightPanel = false;

                    if (e.DistanceX > swipeReactionValue)
                        SwipeGestureCatch = SwipeMotionEnum.smRight;
                    else if (e.DistanceX < -swipeReactionValue)
                        SwipeGestureCatch = SwipeMotionEnum.smLeft;
                    else
                        MoveLeftPanel(e.DistanceX);
                };

                leftSwipeGesture.TouchEnded += (s, e) =>
                {
                    switch (SwipeGestureCatch)
                    {
                        case SwipeMotionEnum.smLeft:
                            IsShowLeftPanel = false;
                            break;

                        case SwipeMotionEnum.smRight:
                            IsShowLeftPanel = true;
                            break;

                        default:
                            if (LeftPanel.TranslationX >= _leftPanelWidth / 2)
                                IsShowLeftPanel = true;
                            else
                                IsShowLeftPanel = false;
                            break;
                    }

                    SwipeGestureCatch = SwipeMotionEnum.smNone;
                };

                baseLayout.Children.Add(leftSwipeGesture,
                    Constraint.Constant(0),
                    Constraint.Constant(_transparentSize),
                    Constraint.Constant(_leftSwipeSize),
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
                        return sibling.Width + _leftSwipeSize;
                    }),
                    Constraint.Constant(_transparentSize),
                    Constraint.RelativeToView(leftPanel, (parent, sibling) =>
                    {
                        var width = parent.Width - sibling.Width;
                        return (RightPanel == null) ? width : width - _rightSwipeSize;
                    }),
                    Constraint.RelativeToParent(parent => { return parent.Height - _transparentSize * 2; }));
                #endregion
            }
        }

        private ViewGestures leftSwipeGesture;
        private int _leftSwipeSize = Device.OnPlatform(10, 10, 15);
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
                    if (LeftPanel == null)
                        return;

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

                rightSwipeGesture = new ViewGestures
                {
                    Content = swipePanel,
                    BackgroundColor = Color.Transparent,
                };

                rightSwipeGesture.Tap += (s, e) =>
                {
                    IsShowLeftPanel = false;
                };

                rightSwipeGesture.Drag += (s, e) =>
                {
                    IsShowLeftPanel = false;

                    if (e.DistanceX > swipeReactionValue)
                        SwipeGestureCatch = SwipeMotionEnum.smRight;
                    else if (e.DistanceX < -swipeReactionValue)
                        SwipeGestureCatch = SwipeMotionEnum.smLeft;
                    else
                        MoveRightPanel(e.DistanceX);
                };

                rightSwipeGesture.TouchEnded += (s, e) =>
                {
                    switch (SwipeGestureCatch)
                    {
                        case SwipeMotionEnum.smLeft:
                            IsShowRightPanel = true;
                            break;

                        case SwipeMotionEnum.smRight:
                            IsShowRightPanel = false;
                            break;

                        default:
                            if (RightPanel.TranslationX <= -_rightPanelWidth / 2)
                                IsShowRightPanel = true;
                            else
                                IsShowRightPanel = false;
                            break;
                    }

                    SwipeGestureCatch = SwipeMotionEnum.smNone;
                };

                baseLayout.Children.Add(rightSwipeGesture,
                    Constraint.RelativeToView(rightPanel, (parent, sibling) =>
                    {
                        return sibling.X - _rightSwipeSize;
                    }),
                    Constraint.Constant(_transparentSize),
                    Constraint.Constant(_rightSwipeSize),
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
                    Constraint.Constant((LeftPanel == null) ? 0 : _leftSwipeSize),
                    Constraint.Constant(_transparentSize),
                    Constraint.RelativeToView(rightPanel, (parent, sibling) =>
                    {
                        var width = parent.Width - sibling.Width- _rightSwipeSize;
                        return (RightPanel == null) ? width : width - _leftSwipeSize;
                    }),
                    Constraint.RelativeToParent(parent => { return parent.Height - _transparentSize * 2; }));
                #endregion
            }
        }

        private ViewGestures rightSwipeGesture;
        private int _rightSwipeSize = Device.OnPlatform(10, 10, 15);
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
                    if (RightPanel == null)
                        return;

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
            {
                leftSwipeGesture.TranslateTo(_leftPanelWidth, leftSwipeGesture.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
                await LeftPanel.TranslateTo(_leftPanelWidth, LeftPanel.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
            }
        }

        async private void HideLeftPanel()
        {
            if (LeftPanel != null)
            {
                leftSwipeGesture.TranslateTo(0, leftSwipeGesture.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
                await LeftPanel.TranslateTo(0, LeftPanel.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
            }
        }

        async private void ShowRightPanel()
        {
            IsShowLeftPanel = false;

            if (RightPanel != null)
            {
                rightSwipeGesture.TranslateTo(0 - _rightPanelWidth, rightSwipeGesture.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
                //ContentLayout.FadeTo(0.5, SpeedAnimatePanel, Easing.CubicOut);
                await RightPanel.TranslateTo(0 - _rightPanelWidth, RightPanel.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
            }
        }

        async private void HideRightPanel()
        {
            if (RightPanel != null)
            {
                rightSwipeGesture.TranslateTo(0, rightSwipeGesture.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
                //ContentLayout.FadeTo(1, SpeedAnimatePanel, Easing.CubicOut);
                await RightPanel.TranslateTo(0, RightPanel.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
            }
        }
        #endregion

        #region Panel move
        private void MoveLeftPanel(double deltaTranslate)
        {
            if ((LeftPanel.TranslationX + deltaTranslate) <= 0)
                LeftPanel.TranslationX = 0;
            else if ((LeftPanel.TranslationX + deltaTranslate) >= _leftPanelWidth)
                LeftPanel.TranslationX = _leftPanelWidth;
            else
                LeftPanel.TranslationX += deltaTranslate;

            leftSwipeGesture.TranslationX = LeftPanel.TranslationX;
        }

        private void MoveRightPanel(double deltaTranslate)
        {
            if ((RightPanel.TranslationX + deltaTranslate) >= 0)
                RightPanel.TranslationX = 0;
            else if ((RightPanel.TranslationX + deltaTranslate) <= (0 - _rightPanelWidth))
                RightPanel.TranslationX = 0 - _rightPanelWidth;
            else
                RightPanel.TranslationX += deltaTranslate;

            rightSwipeGesture.TranslationX = RightPanel.TranslationX;
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
