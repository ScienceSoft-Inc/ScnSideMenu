using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScnPage.Plugin.Forms
{
    public class BaseLanguageStrings
    {
        public BaseLanguageStrings(string enUs = "")
        {
            EnUsValue = enUs;
        }
        
        public string EnUsValue { get; set; }

        public string Current
        {
            get { return GetCurrentLangString(); }
        }

        public virtual string GetCurrentLangString()
        {
            return EnUsValue;
        }
    }
}
