using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.Ui;
using cs_packages.client;


namespace cs_packages.browsers
{
    class SellPoint : Events.Script
    {
      
        public static HtmlWindow SellPointBrowser = null;

        public SellPoint()
        {
            Events.Add("open.sell", Open);
            Events.Add("sellPointItems", SellPoiintItems);
            Events.Add("closeSellPoint", CloseSellPoint);
            //Events.Add("executeFunction", ExecuteFunctionEvent);
            //Events.Add("destroyBrowser", DestroyBrowserEvent);
        }

        public void Open(object[] args)
        {

            KeyManager.block = 2;
            SellPointBrowser = new HtmlWindow("package://auth/assets/sellPoint.html");
            SellPointBrowser.Active = true;
         // //  Chat.Output(args[0].ToString());
            SellPointBrowser.ExecuteJs("pushSellPoint('" + args[0].ToString() + "','" +  "names" +"');");
          

            Cursor.Visible = true;
        

        }

        public void SellPoiintItems(object[] args)
        {
           
            RAGE.Events.CallRemote("sellpoint.client", args[0], args[1]);
        }


        public void CloseSellPoint(object[] args)
        {

            KeyManager.block = 0;
            SellPointBrowser.Destroy();
            SellPointBrowser.Active = false;
            SellPointBrowser = null;
            Cursor.Visible = false;
        }

    }
}
