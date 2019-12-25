using RAGE;
using RAGE.Ui;
using cs_packages.player;
using cs_packages.client;
using System.IO;

namespace cs_packages.vehicle
{



    class Vehicle_Inventory : Events.Script
    {
        static public HtmlWindow InventoryCar = null;


        public Vehicle_Inventory()
        {
            Events.Add("OpenCarInventory", OpenInventory);
            Events.Add("action.currentCarInv", СurrentCarInv);
            Events.Add("closeCarInventory", CloseCarInventory);
        }


        public static void OpenInventory(object[] args)
        {
            if (InventoryCar == null)
            {
                InventoryCar = new HtmlWindow("package://auth/assets/carInventory.html");

                KeyManager.block = 13;

                InventoryCar.Active = true;
                // //  Chat.Output(args[0].ToString());

                InventoryCar.ExecuteJs("pushInventory('" + args[0].ToString() + "','" + args[1].ToString() + "'," + (float)args[2] + "," + (float)args[3] + ",'" + (bool)args[4] + "');");
                //Chat.Output("pushInventory('" + args[0].ToString() + "','" + args[1].ToString() + "'," + (float)args[2] + "," + (float)args[3] + "," + (bool)args[4] + ");");
                Cursor.Visible = true;







                Chat.Show(false);
                DrawInfo.LoadScreen = false;
            }

        }
        public static void СurrentCarInv(object[] args)
        {
            Events.CallRemote("CurrentCarInventory",args[1],args[2]);

        }

       




        public static void CloseCarInventory(object[] args)
        {

           // CloseVehicleInv

            Events.CallRemote("CloseCarInventory");
          

            KeyManager.block = 0;
            InventoryCar.Active = false;
            InventoryCar.Destroy();
         
            InventoryCar = null;
         
            Cursor.Visible = false;







            Chat.Show(true);
        }

    }
}
