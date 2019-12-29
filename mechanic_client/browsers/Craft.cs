using RAGE;
using RAGE.Ui;
using System.Linq;
using cs_packages.client;


namespace cs_packages.browsers
{
    class Craft : Events.Script
    {
        public static HtmlWindow craft = null;

        public Craft()
        {
            Events.Add("createCraft", CreateBrowserEvent);
            Events.Add("doneCraft", DoneCraft);
            Events.Add("craftExit", CreateBrowserEvent); 
            //   Events.OnBrowserCreated += OnBrowserCreatedEvent;
        }




        public static void CreateBrowserEvent(object[] args)
        {

            if (craft == null)
            {
                //   Chat.Show(false);

                Cursor.Visible = true;
               string items = args[0].ToString();
                string crafter = args[1].ToString();
              //  Chat.Output(items);
              //  Chat.Output(crafter);
                craft = new HtmlWindow("package://auth/assets/craft.html");
               // craft.ExecuteJs("pushCraft('" + items + "','" + crafter + "','" + args[2].ToString() + "');");
                craft.ExecuteJs("pushCraft('" + items + "','"+crafter+"','" + args[2].ToString() + "',"+ (int)args[3] +");");
                //  inventoryCef.Active = true;



              ////  Chat.Output(args[0].ToString());

                //menuActive = true;
                //bBind();
                //cefActive = true;
                KeyManager.block = 2;
            }
            else
            {
                craft.Destroy();
                craft = null;
                KeyManager.block = 0;
                //     Chat.Show(true);

                Cursor.Visible = false;

            }
        }

        public void DoneCraft(object[] args)
        {
            craft.Destroy();
            craft = null;
            KeyManager.block = 0;
            //     Chat.Show(true);

            Cursor.Visible = false;

            Events.CallRemote("craft.server", args[0], args[1]);

            

        }



    }
}
