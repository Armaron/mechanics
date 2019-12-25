using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.Ui;

namespace cs_packages.browsers
{
    /// <summary>
    /// Интерфейс для реализации классов основанных на ЦЕФ интерфейсе
    /// </summary>
    interface IBrowser
    {
        /// <summary>
        /// открываемая страничка браузера
        /// </summary>
        HtmlWindow browser { get;  set; }
        /// <summary>
        /// Открыть закрыть браузер
        /// </summary>
        void ShowHideBrowser();
        /// <summary>
        /// Создать браузер
        /// </summary>
        void LoadBrowser();







    }
}
