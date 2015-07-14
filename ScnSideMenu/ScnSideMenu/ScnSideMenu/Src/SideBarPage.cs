using System;
using Xamarin.Forms;
using ScnPage.Plugin.Forms;

namespace ScnSideMenu.Forms
{
    public class SideBarPage : BaseContentPage
    {
        public SideBarPage(Type viewModelType, Type contentUIType)
            : base(viewModelType, contentUIType)
        {
            LeftPanel = new SideBarPanel();
            RightPanel = new SideBarPanel();
        }

        public enum PanelEnum { pLeft, pRight }
        static object locker = new object();

        #region Left panel
        private SideBarPanel leftPanel;
        public SideBarPanel LeftPanel 
        { 
            get { return leftPanel; }
            set
            {
                leftPanel = value;

                leftPanel.Click += (s, e) => { ClosePanel(); };
                baseLayout.Children.Add(leftPanel,
                    Constraint.RelativeToParent(parent =>
                    {
                        if (_leftPanelWidth == 0)
                            _leftPanelWidth = parent.Width - Device.OnPlatform(48, 48, 64);

                        return 0 - _leftPanelWidth;
                    }),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent(parent => { return _leftPanelWidth; }),
                    Constraint.RelativeToParent(parent => { return parent.Height; }));
            }
        }

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

                    if (value)
                        ShowLeftPanel();
                    else
                        HideLeftPanel();

                    OnPanelChanged(new SideBarEventArgs(value, PanelEnum.pLeft));
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
        private SideBarPanel rightPanel;
        public SideBarPanel RightPanel 
        { 
            get { return rightPanel; }
            set 
            {
                rightPanel = value;

                rightPanel.Click += (s, e) => { ClosePanel(); };
                baseLayout.Children.Add(rightPanel,
                    Constraint.RelativeToParent(parent => { return parent.Width; }),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent(parent =>
                    {
                        if (_rightPanelWidth == 0)
                            _rightPanelWidth = parent.Width - Device.OnPlatform(48, 48, 64);

                        return _rightPanelWidth;
                    }),
                    Constraint.RelativeToParent(parent => { return parent.Height; }));
            }
        }

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

                    if (value)
                        ShowRightPanel();
                    else
                        HideRightPanel();

                    OnPanelChanged(new SideBarEventArgs(value, PanelEnum.pLeft));
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

            await LeftPanel.TranslateTo(_leftPanelWidth, LeftPanel.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
        }

        async private void HideLeftPanel()
        {
            await LeftPanel.TranslateTo(0, LeftPanel.TranslationY, SpeedAnimatePanel, Easing.CubicOut);
        }

        async private void ShowRightPanel()
        {
            IsShowLeftPanel = false;

            await RightPanel.LayoutTo(new Rectangle((baseLayout.Width - _rightPanelWidth), RightPanel.TranslationY, RightPanel.Width, RightPanel.Height), SpeedAnimatePanel, Easing.CubicOut);
        }

        async private void HideRightPanel()
        {
            await RightPanel.LayoutTo(new Rectangle(baseLayout.Width, RightPanel.TranslationY, RightPanel.Width, RightPanel.Height), SpeedAnimatePanel, Easing.CubicOut);
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

            return IsBackPress && base.OnBackButtonPressed();
        }

        public class SideBarEventArgs : EventArgs
        {
            public readonly bool IsShow;
            public readonly PanelEnum Panel;

            public SideBarEventArgs(bool isShow, PanelEnum panel)
            {
                IsShow = isShow;
                Panel = panel;
            }
        }
    }
}
