using System;
using System.Collections.Generic;
using System.Text;
using cs_packages.client;
using Newtonsoft.Json;
using RAGE;
using RAGE.Ui;

namespace cs_packages.browsers
{
   public class Document :Events.Script
    {

        static public HtmlWindow Documents = null;

        public Document()
        {
            Events.Add("OpenDocument", OpenDocument);
        }

        public static void OpenDocument(object[] args)
        {
            KeyManager.block = 7;
            string[] tr = JsonConvert.DeserializeObject<string[]>(args[3].ToString());

            if (args[1].ToString() == null || args[1].ToString() == "")
            {
                args[1] = "Заголовок";
            }
            if (args[2].ToString() == null)
            {
                args[2] = "";
            }
           
            var Elem = new { Fraction = args[0].ToString(), Title = args[1].ToString(), Text = args[2].ToString(), SignsList = tr };
           
            Documents = new HtmlWindow("package://auth/assets/fractionDocument.html");
            Documents.ExecuteJs("pushFractionDocument('" + JsonConvert.SerializeObject(Elem) + "');");
            Documents.Active = true;
           
            //cashpointInit(card,cash,terminal,maxwithdrawal,phoneNumber)
        
            Cursor.Visible = true;
            Chat.Show(false);
        }


    }
}
