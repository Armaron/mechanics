using cs_packages.client;
using cs_packages.player;
using RAGE;
using RAGE.Ui;
using System;

public class Inventory : Events.Script
{
    static public HtmlWindow inventoryCef = null;
   
   
    string loc1 = "";
    string loc2 = "";
    string loc3 = "";
    string items = "";
    static string give_name = "";
    static int give_count = 0;
    public static Int64 tempGive = 0;
    public static bool give = false;
    static RAGE.Elements.Player target;

    public Inventory()
    {

        //  inventoryCef.ExecuteJs("document.getElementById('inventory').style.display = 'none';");


        Events.Add("setinventary.client", OpenInventory);
        Events.Add("closeInventory", OpenInventory);
        Events.Add("inventory.dropItem", InventaryEvent);
        Events.Add("use", Use);// пользовать
        Events.Add("remove", Remove);//снять
        Events.Add("drop", Drop);//выкинуть
        Events.Add("drop.client", DropClient);//выкинуть
        Events.Add("give", Give);//передать
        Events.Add("give.true", GiveTrue);//передать
        Events.Add("action.currentInventory", СurrentInventory);//выкинуть
        Events.Add("want.give", WantGive);
        Events.Add("GiveFalse", GiveFalse);
        Events.Add("actionTimedOut", GiveFalseServer);


    }



    public void GiveFalseServer(object[] args)
    {
        Chat.Output("Соси писос");
        Events.CallRemote("GiveFalse.server");
    }


    public void WantGive(object[] args)
    {

        give_name = args[0].ToString();
        give_count = Convert.ToInt32(args[1]);
        give = true;
        target = (RAGE.Elements.Player)args[2];
        tempGive = TickEvent.tickcount;

    }

    public static void GiveFalse(object[] args)
    {
        Chat.Output("Соси еще писос");
        give_name = "";
        give_count = 0;
        target = null;
        give = false;

    }


    public static void GiveTrue()
    {


        if (inventoryCef != null)
        {

            inventoryCef.Destroy();
            inventoryCef = null;
            KeyManager.block = 0;
            //     Chat.Show(true);

            Cursor.Visible = false;

        }



        Events.CallRemote("GiveWantTrue", give_name, give_count, target);
        give_name = "";
        give_count = 0;
        target = null;
        give = false;

    }





    public void Drop(object[] args)
    {

        Events.CallRemote("dropelem", Convert.ToInt32(args[0]), Convert.ToInt32(args[2]));



        loc1 = args[0].ToString();
        loc2 = args[1].ToString();
        loc3 = args[2].ToString();

        //  Chat.Output(loc1 + loc2 + loc3);



    }
    public void DropClient(object[] args)
    {

        Chat.Output(loc1 + loc2 + loc3);
        inventoryCef.ExecuteJs("doneAction('drop'," + loc1 + ",'" + loc2 + "','" + loc3 + "');");

        loc1 = "";
        loc2 = "";
        loc3 = "";




    }



    public void Give(object[] args)
    {

        Chat.Output("уход");
        // inventoryCef.ExecuteJs("doneAction('drop'," + args[0].ToString() + ",'" + args[1].ToString() + "','" + args[2].ToString() + "');");
        Events.CallRemote("getelem", Convert.ToInt32(args[0]), Convert.ToInt32(args[2]));

        loc1 = args[0].ToString();
        loc2 = args[1].ToString();
        loc3 = args[2].ToString();


        Chat.Output(loc1 + loc2 + loc3);

    }
    public void GiveTrue(object[] args)
    {

        Chat.Output("приход");
        Chat.Output(loc1 + loc2 + loc3);
        inventoryCef.ExecuteJs("doneAction('drop'," + loc1 + ",'" + loc2 + "','" + loc3 + "');");

        loc1 = "";
        loc2 = "";
        loc3 = "";

        // Events.CallRemote("getelem", Convert.ToInt32(args[0]), Convert.ToInt32(args[2]));





    }
    public void Use(object[] args)
    {



        Events.CallRemote("use.server", Convert.ToInt32(args[0]));
        inventoryCef.ExecuteJs("doneAction('use'," + args[0].ToString() + ",'" + args[1].ToString() + "');");

        loc1 = "";
        loc2 = "";
        loc3 = "";




    }
    public void Remove(object[] args)
    {

        //  Chat.Output("dsadsdaa");
        Events.CallRemote("remove.server", Convert.ToInt32(args[0]));
        inventoryCef.ExecuteJs("doneAction('remove'," + args[0].ToString() + ",'" + args[1].ToString() + "');");



        loc1 = "";
        loc2 = "";
        loc3 = "";


    }


    public void InventaryEvent(object[] args)
    {

        //Events.CallRemote("inventory.dropItem.server");
        //action = "inventory.dropItem";
        //index = (int)args[0];




    }
    public void inv(object[] args)
    {
        // Chat.Output(args[0].ToString());


        //Events.CallRemote("inventory.dropItem.server");
        //action = "inventory.dropItem";
        //index = (int)args[0];




    }



    public void СurrentInventory(object[] args)
    {
        Events.CallRemote("getcurrentinventary.server", args[1]);


    }
    public void OpenInventory(object[] args)
    {

        //if (chatActive != true)
        //{
        //    switch (menuActive)
        //    {

        if (inventoryCef == null)
        {
            //   Chat.Show(false);

            Cursor.Visible = true;
            items = args[0].ToString();
            inventoryCef = new HtmlWindow("package://auth/assets/inventory.html");
            inventoryCef.ExecuteJs("pushInventory('" + items + "','man'," + args[1] + ");");
            //  inventoryCef.Active = true;



            //  Chat.Output(args[0].ToString());

            //menuActive = true;
            //bBind();
            //cefActive = true;
            KeyManager.block = 4;
            DrawInfo.LoadScreen = false;
        }
        else
        {

            Inventory.inventoryCef.Destroy();
            Inventory.inventoryCef = null;
            KeyManager.block = 0;
            //     Chat.Show(true);

            Cursor.Visible = false;


        }



    }
}
