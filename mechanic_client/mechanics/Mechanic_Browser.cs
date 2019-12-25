using Newtonsoft.Json;
using RAGE;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;


namespace mechanic_client
{
    class Mechanic_Browser
    {
        private static string url = "package://auth/assets/mechanical.html";
        private static RAGE.Ui.HtmlWindow cef;

        public static void Open(string page)
        {
            Prepair(true);
        }
        public static void Create ()
        {
            cef = new RAGE.Ui.HtmlWindow(url);
            cef.Active = true;
           
        }

        public static void Close()
        {
           Prepair(false);     
        }

        public static void Call(string json) {
            cef.ExecuteJs($"pushMechanicalShop('{json}')");
        }

        private static void Prepair(bool isOpened)
        {
            
            RAGE.Ui.Cursor.Visible = isOpened;
            if (!isOpened) {
                cef.Destroy();
            }
        }
    }
}
