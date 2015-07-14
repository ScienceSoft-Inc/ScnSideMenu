using System;
using System.Collections.Generic;
using System.Text;

using ScnPage.Plugin.Forms;
using ScnPage.Plugin.Forms.Helpers;

namespace SimpleSideMenu.Views.ContentUI
{
    public class MainContentUI : BaseContentUI
    {
        public MainContentUI()
        {
            _title = new PropertyLang("Map", "Карта", "Мапа");
        }
    }
}
