//using System;
//using System.Collections.Generic;
//using System.Text;
//using cs_packages.client;
//using RAGE;
//using RAGE.Ui;
//using cs_packages.vehicle;

//namespace cs_packages.browsers
//{
//    public  class GasStation : Events.Script
//    {
//        static public HtmlWindow Station = null;
//        public GasStation()
//        {
//            Events.Add("OpenGasStation", OpenGasStation);
//            Events.Add("exitAzs", ExitAzs);
//            Events.Add("azsBuy", AzsBuy);
//        }

//        public void OpenGasStation(object[] args)
//        {
//            if (TickEvent.SpeedClass != null)
//            {

//                KeyManager.block = 13;
//                Station = new HtmlWindow("package://auth/assets/azs.html");
//                Station.Active = true;
//                int maxfule = TickEvent.SpeedClass.maxfuelincar - (int)TickEvent.SpeedClass.fuelLevel;
//                Station.ExecuteJs("pushAzs('" + args[0].ToString() + "',1," + maxfule + ");");

//                Cursor.Visible = true;
//                Chat.Show(false);
//            }
//        }

//        public void ExitAzs(object[] args)
//        {
//            KeyManager.block = 0;
//            Station.Active = false;
//            Station.Destroy();
//            Station = null;
        
           

//            Cursor.Visible = false;
//            Chat.Show(true);
//        }
//        public void AzsBuy(object[] args)
//        {
//            string service = args[0].ToString();
//            int fuelcount =  Convert.ToInt32(args[1]);
//            string type = args[2].ToString();
//            int price = Convert.ToInt32(args[3]);

//            switch (service)
//            {
//                case "card": //карточкой
//                    {
                        
//                        Events.CallRemote("BuyFuelCard", fuelcount,type,price);

//                        break;
//                    }
//                case "cash": //наличкой
//                    {

//                        Events.CallRemote("BuyFuelCash", fuelcount, type, price);
//                        break;
//                    }

//            }

         
//            ExitAzs(null);
//        }
//    }
//}
