using System;
using System.Collections.Generic;
using System.Text;
using cs_packages.client;
using RAGE;
using RAGE.Ui;

namespace cs_packages.browsers
{
   public  class DeathScreen : Events.Script
    {
        public static HtmlWindow Death;

        public DeathScreen()
        {
            Events.Add("PlayerDeath", PlayerDeath);
            Events.Add("CloseDeath", CloseDeath);
            
        }
        public void PlayerDeath(object[] args)
        {
            Death = new HtmlWindow("package://auth/assets/coma.html");
            Death.ExecuteJs("pushNotebook('"+" Допрыгался блеать" + "');");
            // phone.ExecuteJs("pushNotebook('" + args[0].ToString() + "',filer, archive," +  "'"+args[3].ToString() + "'," + true + ");");
            KeyManager.block = 99;
            Chat.Show(false);

            Death.Active = true;


            Cursor.Visible = true;


        }
        public void CloseDeath(object[] args)
        {
            if (Death != null)
            {
                Death.Active = false;
                Death.Destroy();
                // phone.ExecuteJs("pushNotebook('" + args[0].ToString() + "',filer, archive," +  "'"+args[3].ToString() + "'," + true + ");");

                Death = null;
            }

                KeyManager.block = 0;
                Chat.Show(true);

                Cursor.Visible = false;


        }
    }
}
