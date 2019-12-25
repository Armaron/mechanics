using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.Ui;
using cs_packages.client;

namespace cs_packages.browsers
{
    class Busines : Events.Script
    {
        public static HtmlWindow BusinesBrowser = null;
        public Busines()
        {
            Events.Add("OpenSellBusinessPanel", OpenSellBusinessPanel);
			Events.Add("OpenBusinessPanel", OpenBusinessPanel);
			Events.Add("buyBusinessButton", BuyBusinessButton);
			Events.Add("sellItems", SellPoiintItems);
            Events.Add("buyBusinessExit", Exit);
            Events.Add("closeBusiness", Exit);
            Events.Add("sellBusiness", SellBusiness);
            //     
        }
        public void BuyBusinessButton(object[] args)
        {
			string service = args[0].ToString();
			if(service == "cash")
			{
			RAGE.Events.CallRemote("PayBusinessCash", args[1], args[2]);
			
			}
			else if(service == "card")
			{
			
			RAGE.Events.CallRemote("PayBusinessCard", args[1], args[2]);
			}
			
		//            
//mp.trigger("buyBusinessButton", cashService, busName, busPrice);
//            cashService - как и раньше -cash или card
//busName - имя бизнеса
//busPrice - цена бизнеса.
        }
		public void SellPoiintItems(object[] args)
        {
           
            RAGE.Events.CallRemote("sellpoint.client", args[0], args[1]);
        }

        public void OpenSellBusinessPanel(object[] args)
        {
          //  Chat.Output("BizLoad...");
			KeyManager.block = 5;
            BusinesBrowser = new HtmlWindow("package://auth/assets/buyBusiness.html");
            BusinesBrowser.Active = true;
         // //  Chat.Output(args[0].ToString());
            BusinesBrowser.ExecuteJs("pushBuyBusiness('" + args[0].ToString() + "','" +  args[1].ToString() +"',"+ (int)args[2]+");");
          

            Cursor.Visible = true;
          //  Chat.Output("BizLoadSuccess!");

        }
		 public void OpenBusinessPanel(object[] args)
        {
			KeyManager.block = 5;
            BusinesBrowser = new HtmlWindow("package://auth/assets/sellBusiness.html");
            BusinesBrowser.Active = true;
         // //  Chat.Output(args[0].ToString());
            BusinesBrowser.ExecuteJs("pushBusiness('" + args[0].ToString() + "','" +  args[1].ToString() +"',"+ (int)args[2]+");");
          

            Cursor.Visible = true;
		

        }
            
		public void Exit(object[] args)
        {
            KeyManager.block = 0;
            BusinesBrowser.Destroy();
            BusinesBrowser.Active = false;
            BusinesBrowser = null;
            Cursor.Visible = false;
        }
        public void SellBusiness(object[] args)
        {
            RAGE.Events.CallRemote("SellBusinessCash", args[0]);

            KeyManager.block = 0;
            BusinesBrowser.Destroy();
            BusinesBrowser.Active = false;
            BusinesBrowser = null;
            Cursor.Visible = false;
        }


    }
}
