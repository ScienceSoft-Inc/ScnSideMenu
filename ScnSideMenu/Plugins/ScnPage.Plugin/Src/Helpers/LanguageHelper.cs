using System;
using System.Collections.Generic;

namespace ScnPage.Plugin.Forms.Helpers
{
    public static class LanguageHelper
    {
        public enum LangTypeEnum { ltEn, ltRu, ltBe };
        static public Dictionary<LangTypeEnum, string> LanguageList = new Dictionary<LangTypeEnum, string>
        {
            {LangTypeEnum.ltEn, "English" },
            {LangTypeEnum.ltRu, "Русский" }
            //{LangTypeEnum.ltBe, "Беларуская" }
        };

        static public LangTypeEnum LangNameToEnum(string lang)
        {
            LangTypeEnum result = LangTypeEnum.ltEn;
            foreach (var item in LanguageList)
            {
                if (String.Compare(item.Value, lang, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    result = item.Key;
                    break;
                }
            }

            return result;
        }

        static public LangTypeEnum LangCodeToEnum(string lang)
        {
            if (String.Compare("EN", lang, StringComparison.CurrentCultureIgnoreCase) == 0)
                return LangTypeEnum.ltEn;
            if (String.Compare("RU", lang, StringComparison.CurrentCultureIgnoreCase) == 0)
                return LangTypeEnum.ltRu;
            if (String.Compare("BY", lang, StringComparison.CurrentCultureIgnoreCase) == 0)
                return LangTypeEnum.ltBe;
            return LangTypeEnum.ltEn;
        }
    }
    
    public struct PropertyLang
    {
        public PropertyLang(string enUs = "", string ruRu = "", string beBe = "")
        {
            enUsValue = enUs;
            ruRuValue = ruRu;
            beBeValue = beBe;
        }

        private string enUsValue;
        private string ruRuValue;
        private string beBeValue;

        public string ActualValue(LanguageHelper.LangTypeEnum langType = LanguageHelper.LangTypeEnum.ltEn)
        {
            string stringDescription = enUsValue;

            switch (langType)
            {
                case (LanguageHelper.LangTypeEnum.ltRu):
                    stringDescription = ruRuValue;
                    break;

                case (LanguageHelper.LangTypeEnum.ltBe):
                    stringDescription = beBeValue;
                    break;

                case (LanguageHelper.LangTypeEnum.ltEn):
                default:
                    break;
            };

            return stringDescription;
        }
    }

}
