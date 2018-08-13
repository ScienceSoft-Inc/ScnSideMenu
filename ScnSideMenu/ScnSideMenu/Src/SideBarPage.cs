using ScnPage.Plugin.Forms;
using ScnViewGestures.Plugin.Forms;
using System;
using Xamarin.Forms;

namespace ScnSideMenu.Forms
{
    [Flags]
    public enum PanelSetEnum 
    { 
        psNone = 0,
        psLeft = 1, 
        psRight = 2,
        psLeftRight = psLeft | psRight
    }

    public enum PanelAlignEnum { paLeft, paRight }

    public class SideBarPage : BaseContentPage
    {
        public SideBarPage(PanelSetEnum panelSet)
        {
            Init(panelSet);
        }

        public SideBarPage(Type viewModelType, Type contentUiType, PanelSetEnum panelSet)
            : base(viewModelType, contentUiType)
        {
            Init(panelSet);
        }

        private void Init(PanelSetEnum panelSet)
        {
            if (panelSet == PanelSetEnum.psLeft || panelSet == PanelSetEnum.psLeftRight)
                LeftPanel = new SideBarPanel(PanelAlignEnum.paLeft);

            if (panelSet == PanelSetEnum.psRight || panelSet == PanelSetEnum.psLeftRight)
                RightPanel = new SideBarPanel(PanelAlignEnum.paRight);

            Disappearing += (s, e) => ClosePanel();
        }

        public int TransparentSize { get; set; } = 48;

        private static readonly object Locker = new object();

        public enum SwipeMotionEnum { smNone, smLeft, smRight }

        protected SwipeMotionEnum SwipeGestureCatch = SwipeMotionEnum.smNone;

        public int SwipeReactionValue { get; set; } = 40;

        #region Left panel
        private SideBarPanel _leftPanel;
        public SideBarPanel LeftPanel 
        { 
            get { return _leftPanel; }
            private set
            {
                _leftPanel = value;

                _leftPanel.Swipe += (sender, args) => IsShowLeftPanel = false;

                LayoutChanged += (sender, args) => IsShowLeftPanel = false;

                BaseLayout.Children.Add(_leftPanel,
                    Constraint.RelativeToParent(parent =>
                    {
                        LeftPanelWidth = LeftPanelWidthRequest > 0
                            ? (LeftPanelWidthRequest > parent.Width - TransparentSize
                                ? parent.Width - TransparentSize
                                : LeftPanelWidthRequest)
                            : parent.Width - TransparentSize;

                        return -LeftPanelWidth;
                    }),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent(parent => LeftPanelWidth),
                    Constraint.RelativeToParent(parent => parent.Height));

                // space near border for showing panel of a finger
                #region Swipe panel
                var swipePanel = new BoxView
                {
                    BackgroundColor = Color.Transparent,
					InputTransparent = true
				};

                _leftSwipeGesture = new ViewGestures
                {
                    Content = swipePanel,
                    BackgroundColor = Color.Transparent
                };

                _leftSwipeGesture.Tap += (s, e) => IsShowRightPanel = false;

                _leftSwipeGesture.Drag += (s, e) =>
                {
                    IsShowRightPanel = false;

                    if (e.DistanceX > SwipeReactionValue)
                        SwipeGestureCatch = SwipeMotionEnum.smRight;
                    else if (e.DistanceX < -SwipeReactionValue)
                        SwipeGestureCatch = SwipeMotionEnum.smLeft;
                    else
                        MoveLeftPanel(e.DistanceX);
                };

                _leftSwipeGesture.TouchEnded += (s, e) =>
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
                            IsShowLeftPanel = LeftPanel.TranslationX >= LeftPanelWidth / 2;
                            break;
                    }

                    SwipeGestureCatch = SwipeMotionEnum.smNone;
                };

                BaseLayout.Children.Add(_leftSwipeGesture,
                    Constraint.Constant(0),
                    Constraint.Constant(TransparentSize),
                    Constraint.Constant(LeftSwipeSize),
                    Constraint.RelativeToView(_leftPanel, (parent, sibling) => sibling.Height - TransparentSize * 2));
                #endregion

                // space near panel under active page
                #region Transparent panel
                var leftTransparentPanel = new BoxView
                {
                    BackgroundColor = Color.Transparent,
					InputTransparent = true
				};

                _leftTransparentGestures = new ViewGestures
                {
                    IsVisible = false,
                    Content = leftTransparentPanel,
                    BackgroundColor = Color.Transparent
                };

                _leftTransparentGestures.SwipeRight += (s, e) => IsShowLeftPanel = false;
                _leftTransparentGestures.SwipeLeft += (s, e) => IsShowLeftPanel = false;
                _leftTransparentGestures.SwipeUp += (s, e) => IsShowLeftPanel = false;
                _leftTransparentGestures.SwipeDown += (s, e) => IsShowLeftPanel = false;
                _leftTransparentGestures.Tap += (s, e) => IsShowLeftPanel = false;

                BaseLayout.Children.Add(_leftTransparentGestures,
                    Constraint.RelativeToView(_leftPanel, (parent, sibling) => sibling.Width + LeftSwipeSize),
                    Constraint.Constant(TransparentSize),
                    Constraint.RelativeToView(_leftPanel, (parent, sibling) =>
                    {
                        var width = parent.Width - sibling.Width;
                        return RightPanel == null ? width : width - RightSwipeSize;
                    }),
                    Constraint.RelativeToParent(parent => parent.Height - TransparentSize * 2));
                #endregion
            }
        }

        public int LeftSwipeSize { get; set; } = 10;

        private ViewGestures _leftSwipeGesture;
        private ViewGestures _leftTransparentGestures;

        private bool _isShowLeftPanel;
        public bool IsShowLeftPanel
        {
            get
            {
                lock (Locker)
                    return _isShowLeftPanel;
            }
            set
            {
                lock (Locker)
                {
                    if (LeftPanel == null)
                        return;

                    _isShowLeftPanel = value;
                    _leftTransparentGestures.IsVisible = _isShowLeftPanel;
                    if (value)
                        ShowLeftPanel();
                    else
                        HideLeftPanel();

                    OnPanelChanged(new SideBarEventArgs(value, PanelAlignEnum.paLeft));
                }
            }
        }

        public double LeftPanelWidth { get; protected set; }

        public double LeftPanelWidthRequest { get; set; }
        #endregion

        #region Right panel
        private SideBarPanel _rightPanel;
        public SideBarPanel RightPanel 
        { 
            get { return _rightPanel; }
            private set 
            {
                _rightPanel = value;

                _rightPanel.Swipe += (sender, args) => IsShowRightPanel = false;

                LayoutChanged += (sender, args) => IsShowRightPanel = false;

                BaseLayout.Children.Add(_rightPanel,
                    Constraint.RelativeToParent(parent => parent.Width),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent(parent =>
                    {
                        RightPanelWidth = RightPanelWidthRequest > 0
                            ? (RightPanelWidthRequest > parent.Width - TransparentSize
                                ? parent.Width - TransparentSize
                                : RightPanelWidthRequest)
                            : parent.Width - TransparentSize;

                        return RightPanelWidth;
                    }),
                    Constraint.RelativeToParent(parent => parent.Height));

                // space near border for showing panel of a finger 
                #region Swipe panel
                var swipePanel = new BoxView
                {
                    BackgroundColor = Color.Transparent,
					InputTransparent = true
				};

                _rightSwipeGesture = new ViewGestures
                {
                    Content = swipePanel,
                    BackgroundColor = Color.Transparent
                };

                _rightSwipeGesture.Tap += (s, e) => IsShowLeftPanel = false;

                _rightSwipeGesture.Drag += (s, e) =>
                {
                    IsShowLeftPanel = false;

                    if (e.DistanceX > SwipeReactionValue)
                        SwipeGestureCatch = SwipeMotionEnum.smRight;
                    else if (e.DistanceX < -SwipeReactionValue)
                        SwipeGestureCatch = SwipeMotionEnum.smLeft;
                    else
                        MoveRightPanel(e.DistanceX);
                };

                _rightSwipeGesture.TouchEnded += (s, e) =>
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
                            IsShowRightPanel = RightPanel.TranslationX <= -RightPanelWidth / 2;
                            break;
                    }

                    SwipeGestureCatch = SwipeMotionEnum.smNone;
                };

                BaseLayout.Children.Add(_rightSwipeGesture,
                    Constraint.RelativeToView(_rightPanel, (parent, sibling) => sibling.X - RightSwipeSize),
                    Constraint.Constant(TransparentSize),
                    Constraint.Constant(RightSwipeSize),
                    Constraint.RelativeToView(_rightPanel, (parent, sibling) => sibling.Height - TransparentSize * 2));
                #endregion

                // space near panel under active page
                #region Transparent panel
                var rightTransparentPanel = new BoxView
                {
                    BackgroundColor = Color.Transparent,
					InputTransparent = true
				};

                _rightTransparentGestures = new ViewGestures
                {
                    IsVisible = false,
                    Content = rightTransparentPanel,
                    BackgroundColor = Color.Transparent
                };

                _rightTransparentGestures.SwipeRight += (s, e) => IsShowRightPanel = false;
                _rightTransparentGestures.SwipeLeft += (s, e) => IsShowRightPanel = false;
                _rightTransparentGestures.SwipeUp += (s, e) => IsShowRightPanel = false;
                _rightTransparentGestures.SwipeDown += (s, e) => IsShowRightPanel = false;
                _rightTransparentGestures.Tap += (s, e) => IsShowRightPanel = false;

                BaseLayout.Children.Add(_rightTransparentGestures,
                    Constraint.Constant(LeftPanel == null ? 0 : LeftSwipeSize),
                    Constraint.Constant(TransparentSize),
                    Constraint.RelativeToView(_rightPanel, (parent, sibling) =>
                    {
                        var width = parent.Width - sibling.Width- RightSwipeSize;
                        return RightPanel == null ? width : width - LeftSwipeSize;
                    }),
                    Constraint.RelativeToParent(parent => parent.Height - TransparentSize * 2));
                #endregion
            }
        }

        public int RightSwipeSize { get; set; } = 10;

        private ViewGestures _rightSwipeGesture;
        private ViewGestures _rightTransparentGestures;

        private bool _isShowRightPanel;
        public bool IsShowRightPanel
        {
            get
            {
                lock (Locker)
                    return _isShowRightPanel;
            }
            set
            {
                lock (Locker)
                {
                    if (RightPanel == null)
                        return;

                    _isShowRightPanel = value;
                    _rightTransparentGestures.IsVisible = _isShowRightPanel;

                    if (value)
                        ShowRightPanel();
                    else
                        HideRightPanel();

                    OnPanelChanged(new SideBarEventArgs(value, PanelAlignEnum.paRight));
                }
            }
        }

        public double RightPanelWidth { get; protected set; }

        public double RightPanelWidthRequest { get; set; }
        #endregion

        public event EventHandler<SideBarEventArgs> PanelChanged;
        public void OnPanelChanged(SideBarEventArgs e)
        {
            PanelChanged?.Invoke(this, e);
        }

        private uint _speedAnimatePanel = 300;
        public uint SpeedAnimatePanel
        {
            get => _speedAnimatePanel;
            set => _speedAnimatePanel = value > 0 ? value : 0;
        }

        public void ClosePanel()
        {
            IsShowLeftPanel = false;
            IsShowRightPanel = false;
        }

        #region Panel animation
        private async void ShowLeftPanel()
        {
            IsShowRightPanel = false;

            if (LeftPanel != null)
            {
                _leftSwipeGesture.TranslationX = LeftPanelWidth;
                await LeftPanel.TranslateTo(LeftPanelWidth, LeftPanel.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
            }
        }

        private async void HideLeftPanel()
        {
            if (LeftPanel != null)
            {
                _leftSwipeGesture.TranslationX = 0;
                await LeftPanel.TranslateTo(0, LeftPanel.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
            }
        }

        private async void ShowRightPanel()
        {
            IsShowLeftPanel = false;

            if (RightPanel != null)
            {
                _rightSwipeGesture.TranslationX = -RightPanelWidth;
                await RightPanel.TranslateTo(-RightPanelWidth, RightPanel.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
            }
        }

        private async void HideRightPanel()
        {
            if (RightPanel != null)
            {
                _rightSwipeGesture.TranslationX = 0;
                await RightPanel.TranslateTo(0, RightPanel.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
            }
        }
        #endregion

        #region Panel move
        private void MoveLeftPanel(double deltaTranslate)
        {
            if (LeftPanel.TranslationX + deltaTranslate <= 0)
                LeftPanel.TranslationX = 0;
            else if (LeftPanel.TranslationX + deltaTranslate >= LeftPanelWidth)
                LeftPanel.TranslationX = LeftPanelWidth;
            else
                LeftPanel.TranslationX += deltaTranslate;

            _leftSwipeGesture.TranslationX = LeftPanel.TranslationX;
        }

        private void MoveRightPanel(double deltaTranslate)
        {
            if (RightPanel.TranslationX + deltaTranslate >= 0)
                RightPanel.TranslationX = 0;
            else if (RightPanel.TranslationX + deltaTranslate <= -RightPanelWidth)
                RightPanel.TranslationX = -RightPanelWidth;
            else
                RightPanel.TranslationX += deltaTranslate;

            _rightSwipeGesture.TranslationX = RightPanel.TranslationX;
        }
        #endregion

        protected override bool OnBackButtonPressed()
        {
            var isBackPress = IsShowLeftPanel || IsShowRightPanel;

            if (isBackPress)
                ClosePanel();

            return isBackPress || base.OnBackButtonPressed();
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
