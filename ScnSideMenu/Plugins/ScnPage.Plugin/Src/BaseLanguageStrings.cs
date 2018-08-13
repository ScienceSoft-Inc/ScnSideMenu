namespace ScnPage.Plugin.Forms
{
    public class BaseLanguageStrings
    {
        public BaseLanguageStrings(string enUs = "")
        {
            EnUsValue = enUs;
        }
        
        public string EnUsValue { get; set; }

        public string Current => GetCurrentLangString();

        public virtual string GetCurrentLangString()
        {
            return EnUsValue;
        }
    }
}
