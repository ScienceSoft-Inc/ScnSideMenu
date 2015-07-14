using Xamarin.Forms;
using ScnPage.Plugin.Forms.Helpers;

namespace ScnPage.Plugin.Forms
{
    public class BaseContentUI
    {
        public BaseContentUI()
        {
            txtLoading = new PropertyLang("loading...", "загрузка...", "пампаванне...");
            txtAwait = new PropertyLang("wait...", "подождите...", "чакайце...");
        }

        protected PropertyLang _title;
        public string Title
        {
            get { return _title.ActualValue(); }
        }

        private PropertyLang txtLoading;
        public string TxtLoading
        {
            get { return txtLoading.ActualValue(); }
        }

        private PropertyLang txtAwait;
        public string TxtAwait
        {
            get { return txtAwait.ActualValue(); }
        }

        #region Style
        public Color AppBarColor = Color.FromRgb(8, 132, 192);
        public string ImgAppBarBack = Device.OnPlatform("Icon/bar_back.png", "ic_bar_back.png", "");
        public double AppBarBtnOpacity = 0.7;

        public Style AppBarTitleStyle = new Style(typeof(Label))
        {
            Setters = 
            {
                new Setter { Property = Label.FontFamilyProperty, Value =  Device.OnPlatform("Arial", "Arial", "Arial") },
                new Setter { Property = Label.FontSizeProperty, Value = Device.OnPlatform(16, 16, 12) },
                new Setter { Property = Label.TextColorProperty, Value = Device.OnPlatform(Color.White, Color.White, Color.Black) }
            }
        };

        public Color MainBackGroundColor = Color.FromHex("fff");
        #endregion
    }
}
