using RAGE;
using RAGE.Ui;
using System.Linq;
using cs_packages.browsers;
using System;

namespace cs_packages.client
{
    class Menu : Events.Script
    {
        static public HtmlWindow hudCef = null;
        static public HtmlWindow menuCef = null;
        static public HtmlWindow inventoryCef = null;
        static public HtmlWindow animCircleCef = null;
        static public bool ready;
        bool chatActive = false;

        public Menu()
        {
            Events.Add("message.send", MessageSend);
            Events.Add("skills.menu", SkillsMenu);

            Events.Add("menuData.client", MenuData);
            Events.Add("animCircleData.client", AnimCircleData);





            Events.Add("settingsSave.client", SettingsSave);
            Events.Add("animate.client", Animate);
            Events.Add("animCircleSave.client", AnimateSave);
            Events.Add("animCircleReset.client", AnimateReset);

            Events.Add("waterStatus.client", WaterStatus);
            Events.Add("eatStatus.client", EatStatus);
            Events.Add("cardStatus.client", CardStatus);
            Events.Add("cashStatus.client", CashStatus);

        }
        public void SkillsMenu(object[] args)
        {

            if (menuCef != null)
                menuCef.ExecuteJs("skillsInitialize('" + args[0].ToString() + "');");

        }

        public static void CreateAllMenu()
        {

            Phone.LoadCef();
            hudCef = new HtmlWindow("package://auth/assets/hud.html");
            menuCef = new HtmlWindow("package://auth/assets/menu.html");
            animCircleCef = new HtmlWindow("package://auth/assets/animCircle.html");
            menuCef.ExecuteJs("document.getElementById('buttons').style.display = 'none';");
            animCircleCef.ExecuteJs("document.getElementById('animCircle').style.display = 'none';");
            menuCef.Active = false;
            animCircleCef.Active = false;
            ready = true;
            hudCef.ExecuteJs("eventInit(100);");

        }
        public static void WaterStatus(object[] args)
        {

            hudCef.ExecuteJs("waterStatus(" + Convert.ToInt32(args[0]) + ");");


        }
        public static void EatStatus(object[] args)
        {

            hudCef.ExecuteJs("eatStatus(" + Convert.ToInt32(args[0]) + ");");


        }
        public static void CardStatus(object[] args)
        {

            hudCef.ExecuteJs("cardStatus(" + Convert.ToInt32(args[0]) + ");");


        }
        public static void CashStatus(object[] args)
        {

            hudCef.ExecuteJs("cashStatus(" + Convert.ToInt32(args[0]) + ");");


        }
        public static void OpenMenu()
        {
            Chat.Output("menu open");
            //if (chatActive != true)
            //{
            //    switch (menuActive)
            //    {
            if (!menuCef.Active)

            {
                Chat.Show(false);
                Cursor.Visible = true;
                menuCef.Active = true;

                menuCef.ExecuteJs("document.getElementById('buttons').style.display = 'block';");

                //menuActive = true;
                //bBind();
                //cefActive = true;
                KeyManager.block = 10;
            }
            else
            {
                menuCef.ExecuteJs("document.getElementById('buttons').style.display = 'none';");
                menuCef.ExecuteJs("animCircle.settingsAnimShow = false;");
                menuCef.ExecuteJs("menu.animateSettingsShow = false;");
                Chat.Show(true);
                Cursor.Visible = false;
                menuCef.Active = false;
                //menuActive = false;
                //cefActive = false;
                KeyManager.block = 0;
            }
        }
        public static void OpenAnimList()
        {

            if (!animCircleCef.Active)
            {
                animCircleCef.ExecuteJs("document.getElementById('animCircle').style.display = 'block';");
                Chat.Show(false);
                Cursor.Visible = true;

                animCircleCef.Active = true;
                KeyManager.block = 4;

            }
            else
            {
                animCircleCef.ExecuteJs("document.getElementById('animCircle').style.display = 'none';");
                Chat.Show(true);
                Cursor.Visible = false;
                animCircleCef.Active = false;
                KeyManager.block = 0;
            }

        }





        public void MessageSend(object[] args)
        {
            if (chatActive)
            {
                chatActive = false;
            }
        }
        public void MenuData(object[] args)
        {
            menuCef.ExecuteJs("menu.admin = " + args[0].ToString() + "");
            menuCef.ExecuteJs("menu.autoLogin = " + args[1].ToString() + "");

            menuCef.ExecuteJs("menu.pass.name = '" + args[2].ToString() + "'");
            menuCef.ExecuteJs("menu.pass.age = '" + args[3].ToString() + "'");
            menuCef.ExecuteJs("menu.pass.number = '" + args[4].ToString() + "'");
            menuCef.ExecuteJs("menu.pass.id = '" + args[5].ToString() + "'");
            menuCef.ExecuteJs("menu.pass.signature = '" + args[6].ToString() + "'");
            menuCef.ExecuteJs("menu.pass.carlic = '" + args[7].ToString() + "'");
            menuCef.ExecuteJs("menu.pass.lic = '" + args[8].ToString() + "'");
            menuCef.ExecuteJs("menu.pass.carnumb = '" + args[9].ToString() + "'");
            menuCef.ExecuteJs("menu.pass.housenumb = '" + args[10].ToString() + "'");
            menuCef.ExecuteJs("menu.pass.rank = '" + args[11].ToString() + "'");



            //menuCef.ExecuteJs("menu.pass.name = '" + args[2].ToString() + "'");
            //menuCef.ExecuteJs("menu.pass.id = '" + args[0].ToString() + "'");
            //menuCef.ExecuteJs("menu.pass.age = '" + args[3].ToString() + "'");
            //menuCef.ExecuteJs("menu.pass.signature = '" + args[4].ToString() + "'");    

        }
        public void AnimCircleData(object[] args)
        {
            void setSlot(int slotId, int slotIndex)
            {
                if (slotIndex != -1)
                {
                    animCircleCef.ExecuteJs("animCircle.fastAnimList[" + slotId + "].title = menu.animations[" + slotIndex + "].title;");
                    animCircleCef.ExecuteJs("animCircle.fastAnimList[" + slotId + "].dict = menu.animations[" + slotIndex + "].dict;");
                    animCircleCef.ExecuteJs("animCircle.fastAnimList[" + slotId + "].name = menu.animations[" + slotIndex + "].name;");
                    animCircleCef.ExecuteJs("animCircle.fastAnimList[" + slotId + "].id = " + slotIndex + "");

                    menuCef.ExecuteJs("animCircle.fastAnimList[" + slotId + "].title = menu.animations[" + slotIndex + "].title;");
                    menuCef.ExecuteJs("animCircle.fastAnimList[" + slotId + "].dict = menu.animations[" + slotIndex + "].dict;");
                    menuCef.ExecuteJs("animCircle.fastAnimList[" + slotId + "].name = menu.animations[" + slotIndex + "].name;");
                    menuCef.ExecuteJs("animCircle.fastAnimList[" + slotId + "].id = " + slotIndex + "");
                }

            }
            setSlot(0, (int)args[0]);
            setSlot(1, (int)args[1]);
            setSlot(2, (int)args[2]);
            setSlot(3, (int)args[3]);
            setSlot(4, (int)args[4]);
            setSlot(5, (int)args[5]);
            setSlot(6, (int)args[6]);
            setSlot(7, (int)args[7]);
        }
        public void SettingsSave(object[] args)
        {
            Events.CallRemote("settingsSave.server", args[0]);
        }
        public void Animate(object[] args)
        {
            animCircleCef.ExecuteJs("document.getElementById('animCircle').style.display = 'none';");
            Cursor.Visible = false;
            Chat.Show(true);

            //if (args[1].ToString().Contains("seat"))
            //{
              //  var objects =RAGE.Elements.;
               
               
               
               
               

               

               
               


               
               
               
            //}

            Events.CallRemote("animate.server", args[0], args[1]);
        }
        public void AnimateSave(object[] args)
        {
            animCircleCef.ExecuteJs("animCircle.fastAnimList[" + args[1] + "].title = menu.animations[" + args[0].ToString() + "].title;");
            animCircleCef.ExecuteJs("animCircle.fastAnimList[" + args[1] + "].dict = menu.animations[" + args[0].ToString() + "].dict;");
            animCircleCef.ExecuteJs("animCircle.fastAnimList[" + args[1] + "].name = menu.animations[" + args[0].ToString() + "].name;");
            animCircleCef.ExecuteJs("animCircle.fastAnimList[" + args[1] + "].id = " + args[0].ToString() + "");

            Events.CallRemote("animCircleSave.server", args[0], args[1]);
        }

        public void AnimateReset(object[] args)
        {
            Events.CallRemote("animCircleReset.server", args[0]);
        }


    }
}
