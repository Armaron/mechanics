using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.Ui;
using Newtonsoft.Json;
using cs_packages.client;
using System.Globalization;


namespace cs_packages.browsers
{
    class Shop : Events.Script

    {
        //int sex;               
        static public HtmlWindow shop = null;

        public Shop()
        {
            //From server
            Events.Add("open.shop", OpenShop);

            //From CEF
            Events.Add("shopExit", ExitShop);
            Events.Add("shopBuyButton", BuyShop);

        }

        public void OpenShop(object[] args)
        {
            KeyManager.block = 9;
            shop = new HtmlWindow("package://auth/assets/shop.html");
            shop.Active = true;
            if (args[0].ToString() != "ammo")
            { shop.ExecuteJs("pushShopList('" + args[1].ToString() + "','" + args[0].ToString() + "');"); }
            else
            {
                shop.ExecuteJs("pushShopList('" + args[1].ToString() + "','" + args[0].ToString() + "','" + args[2].ToString() + "');");
                Chat.Output("pushShopList('" + args[1].ToString() + "','" + args[0].ToString() + "','" + args[2].ToString() + "');");
            }
            Cursor.Visible = true;
            Chat.Show(false);
        }

        public void ExitShop(object[] args)
        {
            shop.Destroy();
            Cursor.Visible = false;
            Chat.Show(true);
            KeyManager.block = 0;
            RAGE.Elements.Player.LocalPlayer.FreezePosition(false);





            //Events.CallRemote("SetPlayerDecor");
            //Events.CallRemote("ExitSalon");
            //Events.CallRemote("SetClothesDefault");

        }

        public void BuyShop(object[] args)
        {
            string cashService = args[0].ToString();         //cashService 
            int price = Convert.ToInt32(args[1]);   //clothesPrice (мумма по всем покупкам)
            string output = args[2].ToString();         //output - приходит JSON массив объектов с полями - name, price, type, count

            switch (cashService)
            {
                case "card": //карточкой
                    {
                        Events.CallRemote("PayInShopCard", output, price);
                        break;
                    }
                case "cash": //наличкой
                    {
                        Events.CallRemote("PayInShopCash", output, price);
                        break;
                    }

            }



        }





    }
}
