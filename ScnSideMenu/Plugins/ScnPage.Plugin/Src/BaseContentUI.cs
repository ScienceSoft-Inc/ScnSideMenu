using Xamarin.Forms;
using ScnPage.Plugin.Forms.Helpers;

namespace ScnPage.Plugin.Forms
{
    public class BaseContentUI
    {
        public BaseContentUI()
        {
            title = new BaseLanguageStrings("");
            txtLoading = new BaseLanguageStrings("loading...");
            txtAwait = new BaseLanguageStrings("wait...");
        }

        public BaseLanguageStrings title;
        public string Title
        {
            get { return title.Current; }
        }

        public BaseLanguageStrings txtLoading;
        public string TxtLoading
        {
            get { return txtLoading.Current; }
        }

        public BaseLanguageStrings txtAwait;
        public string TxtAwait
        {
            get { return txtAwait.Current; }
        }
    }
}
