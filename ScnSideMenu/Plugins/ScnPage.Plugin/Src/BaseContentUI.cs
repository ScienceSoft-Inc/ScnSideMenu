namespace ScnPage.Plugin.Forms
{
    public class BaseContentUI
    {
        public BaseContentUI()
        {
            title = new BaseLanguageStrings();
            txtLoading = new BaseLanguageStrings("loading...");
            txtAwait = new BaseLanguageStrings("wait...");
        }

        public BaseLanguageStrings title;
        public string Title => title.Current;

        public BaseLanguageStrings txtLoading;
        public string TxtLoading => txtLoading.Current;

        public BaseLanguageStrings txtAwait;
        public string TxtAwait => txtAwait.Current;
    }
}
