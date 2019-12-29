using cs_packages.client;
using RAGE;
using RAGE.Ui;
using System;

namespace cs_packages.browsers
{
    public class House : Events.Script
    {
        static public HtmlWindow Home = null;
        int housenum = 0;
        public House()
        {
            Events.Add("open.home", Open);
            Events.Add("open.room", OpenRoom);
            Events.Add("apartamentExit", Exit);
            Events.Add("apartamentBuy", Buy);
            Events.Add("comeAppartament", Come);

            Events.Add("closeSellApartament", Exit);
            Events.Add("quitAppartament", Quit);
            Events.Add("sellApartament", SellApartament);
            Events.Add("changeLockAppartament", СhangeLockAppartament);
            Events.Add("closeSellAppartament", CloseSellAppartament);
            Events.Add("openStorageAppartament", OpenStorageAppartament);
            Events.Add("lookInterier", LookInterier);
            Events.Add("giveKeyAppartament", GiveKeyAppartament);
            Events.Add("storageAppartament", StorageAppartament);
            Events.Add("OpenAppInventory", OpenAppInventory);
            Events.Add("action.currentAppInv", СurrentAppInv);
            Events.Add("closeAppInventory", CloseAppInventory);

        }

        public void Open(object[] args)
        {
            if (Home == null)
            {
                int countRooms = (int)args[0];
                int price = Convert.ToInt32(args[1]);
                int countgarage = (int)args[2];
                float invweight = (float)args[3];
                housenum = (int)args[4];
                KeyManager.block = 2;
                Home = new HtmlWindow("package://auth/assets/buyApartament.html");
                Home.Active = true;
                //   Chat.Output(args[0].ToString());
                Home.ExecuteJs("pushApartamentBuy(" + countRooms + "," + price + "," + countgarage + ", " + invweight + ");");

                Cursor.Visible = true;
            }

        }
        public void СhangeLockAppartament(object[] args)
        {
            Events.CallRemote("ChangeKeyApp", housenum);

        }



        public void OpenAppInventory(object[] args)
        {


                CloseAppInventory(null);

           
                Home = new HtmlWindow("package://auth/assets/appartamentInventory.html");



                Home.Active = true;
                //   Chat.Output(args[0].ToString());

                Home.ExecuteJs("pushInventory('" + args[0].ToString() + "','" + args[1].ToString() + "'," + (float)args[2] + "," + (float)args[3] + ",'true');");
                //Chat.Output("pushInventory('" + args[0].ToString() + "','" + args[1].ToString() + "'," + (float)args[2] + "," + (float)args[3] + "," + (bool)args[4] + ");");
                Cursor.Visible = true;







                Chat.Show(false);
       

        }


        public  void СurrentAppInv(object[] args)
        {
            Events.CallRemote("CurrentAppInventory",housenum, args[1], args[2]);

        }
        public  void CloseAppInventory(object[] args)
        {
          

            KeyManager.block = 0;
            if (Home != null)
            {
                Home.Active = false;
                Home.Destroy();

                Home = null;
            }
            Cursor.Visible = false;







            Chat.Show(true);
        }






        public void StorageAppartament(object[] args)
        {
            Events.CallRemote("StorageApp", housenum); //Инвентарь

        }
        public void GiveKeyAppartament(object[] args)
        {
            Events.CallRemote("GiveKeyApp", housenum);

        }
        public void LookInterier(object[] args)
        {
            Events.CallRemote("LookInterierRoom", housenum);

        }

        public void OpenStorageAppartament(object[] args)
        {
            Events.CallRemote("CloseInvAppartament", housenum);

        }
        public void CloseSellAppartament(object[] args)
        {
            Events.CallRemote("CloseAppartament", housenum);

        }

        public void SellApartament(object[] args)
        {
            //mp.trigger('sellApartament', number, price)
            int number = Convert.ToInt32(args[0]);
            int price = Convert.ToInt32(args[1]);

            Events.CallRemote("SellRoom", number, housenum, price);

        }



        public void Quit(object[] args)
        {
            Events.CallRemote("QuitRoom", housenum);

        }
        public void OpenRoom(object[] args)
        {
            //            pushSellApartament(status, numberOf, priceOf)
            //- status можно отправлять пустую строку, если хозяин, чтобы меню продажи было на месте
            //Если посетитель гость, то в status передавать строку 'visitor'


            string status = args[0].ToString();
            int number = Convert.ToInt32(args[1]);
            int priceOf = Convert.ToInt32(args[2]);
            housenum = (int)args[3];

            KeyManager.block = 2;
            Home = new HtmlWindow("package://auth/assets/sellApartament.html");
            Home.Active = true;
            //   Chat.Output(args[0].ToString());
            Home.ExecuteJs("pushSellApartament('" + status + "'," + number + "," + priceOf + ");");
            if ((bool)args[4])
            {
                Home.ExecuteJs("toogleAction(1, 'closeSellAppartament')");
            }
            else
            {
                Home.ExecuteJs("toogleAction(0, 'closeSellAppartament')");
            }
            if ((bool)args[5])
            {
                Home.ExecuteJs("toogleAction(1, 'openStorageAppartament')");
            }
            else
            {
                Home.ExecuteJs("toogleAction(0, 'openStorageAppartament')");
            }
            Cursor.Visible = true;

        }
        public void Exit(object[] args)
        {

            KeyManager.block = 0;
            if (Home != null)
            {
                Home.Active = false;
                //   Chat.Output(args[0].ToString());
                Home.Destroy();
                Home = null;
            }
                Cursor.Visible = false;

        }
        public void Come(object[] args)
        {

            RAGE.Events.CallRemote("ComeHouse", args[0], housenum);


        }

        public void Buy(object[] args)
        {
            //mp.trigger("apartamentBuy", numberOf, cashService, currentPrice);
            //-Номер квартиры
            //- cash / card
            //- цена
            //   Chat.Output(args[0].ToString());
            int roomNum = Convert.ToInt32(args[0]);
            string serv = args[1].ToString();
            int price = Convert.ToInt32(args[2]);
            if (serv == "cash")
            {
                RAGE.Events.CallRemote("PayHouseCash", price, roomNum, housenum);

            }
            else if (serv == "card")
            {

                RAGE.Events.CallRemote("PayHouseCard", price, roomNum, housenum);
            }


        }


    }
}
