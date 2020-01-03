using cs_packages.client;
using RAGE;
using RAGE.Elements;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using static RAGE.Events;
namespace cs_packages.browsers
{
    public class Phone : Events.Script
    {
        static uint globalDimension = 4294967295;

        static float volume = 1f;

        static bool callmarker = false;
        public static bool usePhoneMarker = false;


        public static  HtmlWindow phone = null;
        public static List<RAGE.Elements.Player> PlayerInRadio = new List<RAGE.Elements.Player>();
        public static List<Blip> blips = new List<Blip>();
        public static List<Colshape> colshapes = new List<Colshape>();



        static int n = 0; //номер темы на телефоне (от 0 до 13)

        public static int number;

        public Phone()
        {
            // События
            OnPlayerEnterColshape += DeliteBlipAndColshape;
            OnPlayerExitColshape += ExitColShape;


            //// Работа с ЦЕФкой и настройки телефона
            // Server
            Events.Add("openclose.phone", OpenClose);               //открыть/закрыть телефон
            Events.Add("ContactsToClient", ContactsForClient);      //Загузка контактов и списка автомобилей
            Events.Add("AddFriendInContacts", AddFriendInContacts); //добавление друга в контакты
            Events.Add("WallpaperToClient", WallpaperForClient);    //Загрузка темы
            Events.Add("PushPhoneBalance", PushPhoneBalance);       //изменение баланса
            Events.Add("GoHome", GoHome);                           //сброс на начальный экран            
            // CEF
            Events.Add("lockPhone", OpenClose);                     //закрыть телефон по нажатию кнопки
            Events.Add("refreshedContacts", RefreshedContacts);     //добавление/удаление контакта (перезапись контактов)
            Events.Add("phoneWallpaper", PhoneWallpaper);           //изменение темы
            ////////
            

            //// Сообщения и блипы
            // Server            
            Events.Add("MyOutcomingMessage", MyOutcomingMessage);   //исходящее сообщение (для отрисовки в случае удачной доставки)
            Events.Add("IncomingMessage", IncomingMessage);         //входящее сообщение
            Events.Add("GetGeo", GetGeo);                           //получить геопозицию

            // CEF
            Events.Add("sendMessage", SendMessage);                 //отправить сообщение //mp.trigger("sendMessage", contactsList[currentIndex].number, currentMessage);
            Events.Add("PhoneSendGeo", PhoneSendGeo);               //отправить геопозицию //mp.trigger('PhoneSendGeo', currentElem.number);
            Events.Add("PhoneSendGeoCar", PhoneGetGeoCar);          //запросить геопозицию тачки //mp.trigger('PhoneSendGeoCar', currentElem.number);
            Events.Add("PhoneSendParkingCar", PhoneSendParkingCar); //mp.trigger('PhoneSendGeoCar', currentElem.number);
            ////////


            //// Звонки
            // Server
            Events.Add("IncomingСall", IncomingСall);               //входящий вызов
            Events.Add("CallConfirmed", CallConfirmed);             //исходящий вызов подтвержден
            Events.Add("Volume", Volume);                           //Volume

            // CEF
            Events.Add("PhoneCheckCall", PhoneCheckCall);           //исходящий вызов //mp.trigger('PhoneCheckCall', number);
            Events.Add("cancelOutcomingCall", СancelOutcomingCall); //сбросить исходящийзвонок //mp.trigger("cancelOutcomingCall",getNumber, status); -отбой исходящего(в двух случаях, при дозвоне и при самом разговоре)
            Events.Add("allowIncomingCall", AllowIncomingCall);     //поднять трубку //mp.trigger("allowIncomingCall", getNumber); -приём входящего
            Events.Add("cancelIncomingCall", CancelIncomingCall);   //сбросить входящий звонок //mp.trigger("cancelIncomingCall", getNumber); -отбой входящего
            Events.Add("fastCall", FastCall);                       //mp.trigger('PhoneSendGeoCar', currentElem.number);
            ////////
        }


        public static void LoadCef()  
        {
            phone = new HtmlWindow("package://auth/assets/phone.html");
            phone.Active = false;
            Events.CallRemote("GetWallpaperAndContactsToClient"); //Загрузка темы и контактов из базы данных при включении телефона            
        }


        #region Работа с ЦЕФкой и настройки телефона

        public static void OpenClose(object[] args)
        {
            if (phone.Active == false)
            {
                //  RAGE.Elements.Player.LocalPlayer.TaskUseMobilePhoneTimed(100);                
                RAGE.Game.Mobile.CreateMobilePhone(0);
                RAGE.Game.Mobile.ScriptIsMovingMobilePhoneOffscreen(false);
                RAGE.Game.Mobile.SetMobilePhonePosition(0, 0, 0);
                //    RAGE.Game.Mobile.DisablePhoneThisFrame(true);
                //     RAGE.Game.Mobile.SetPhoneLean(true);//поворрот экрана
                ///  RAGE.Elements.Player.LocalPlayer.PlayAnim("static", "amb@code_human_wander_mobile@male@base", 1f, true, true, true, 1f, 8);
                // RAGE.Elements.Player.LocalPlayer.TaskPlayPhoneGestureAnimation("amb@code_human_wander_mobile@male@base", "static", "BONEMASK_HEAD_NECK_AND_R_ARM",
                //   0.5f,0.25f,true,true);
                KeyManager.block = 8;
              
                Chat.Show(false);
                phone.Active = true;
                phone.ExecuteJs("phoneFadeIn();");
                phone.ExecuteJs("pushPhoneBalance('" + args[0] + "');");
                Cursor.Visible = true;
                usePhoneMarker = true;
                //// phone.ExecuteJs("settingsInitialize(" + 7 + ");");
                //Chat.Output(args[0].ToString());
                //Chat.Output(args[1].ToString());

                //Events.CallRemote("Anim_OpenPhoneClient");


            }
            else
            {
                RAGE.Game.Mobile.DestroyMobilePhone();
                KeyManager.block = 0;
               Chat.Show(true);
                phone.ExecuteJs("phoneFadeOut();");
                phone.Active = false;
                Cursor.Visible = false;
                if (!callmarker)
                {
                    Events.CallRemote("Anim_ClosePhoneClient");

                }
                usePhoneMarker = false;

            }
        }
        public static void ContactsForClient(object[] args)
        {
            phone.ExecuteJs("pushContactList('" + args[0].ToString() + "', '" + args[1].ToString() + "');");
        }
        public static void AddFriendInContacts(object[] args)
        {
            //TODO добавление в контакты
            //Chat.Output("FFRRIIEENNDD");
            //string nicname = args[1].ToString();
            //int number = (int)args[2];

            //phone.ExecuteJs("addContact('" + nicname + "', '" + number.ToString() + "');");

        }
        public static void WallpaperForClient(object[] args)
        {
            phone.ExecuteJs("settingsInitialize(" + Convert.ToInt32(args[0]) + ");");
        }
        public static void PushPhoneBalance(object[] args) 
        {
            int count = (int)args[0];
            phone.ExecuteJs("pushPhoneBalance('" + count + "')");


        }
        public static void GoHome(object[] args)
        {
            phone.ExecuteJs("goHome();");
            callmarker = false;
            RAGE.Elements.Player.LocalPlayer.VoiceVolume = 0f;
            RAGE.Voice.Muted = true;
            RAGE.Elements.Player.LocalPlayer.PlayFacialAnim("mood_normal_1", "facials@gen_male@variations@normal");

            if (!usePhoneMarker) Events.CallRemote("Anim_ClosePhoneClient");
            else Events.CallRemote("Anim_OpenPhoneClient");
            //Events.CallRemote("PlayerStopTalk.server");////??????
        }

        public static void RefreshedContacts(object[] args)
        {            
            Events.CallRemote("Phone_RefreshedContact", args[0]);
        }
        public static void PhoneWallpaper(object[] args)
        {
            Events.CallRemote("Phone_SetNewWallpaper", args[0]);
            phone.ExecuteJs("settingsInitialize(" + Convert.ToInt32(args[0]) + ");");
        }

        #endregion


        #region Сообщения и блипы

        public static void MyOutcomingMessage(object[] args) 
        {            
            phone.ExecuteJs("incomingMessage('" + args[0] + "', 'outcoming', '" + args[1].ToString() + "', '" + args[2].ToString() + "');");
        }
        public static void IncomingMessage(object[] args) 
        {           
            phone.ExecuteJs("incomingMessage('" + args[0] + "', 'incoming', '" + args[1].ToString() + "', '" + args[2].ToString() + "');");
        }
        public static void GetGeo(object[] args) 
        {

            float x = (float)args[0];
            float y = (float)args[1];
            float z = (float)args[2];

            int senderNumber = (int)args[3];

            uint dim = (uint)args[4];

            
            Blip blip = new Blip(280, new Vector3(x, y, z), color: 84, shortRange: false, dimension: dim);
            blip.SetData("SenderPhoneNumber", senderNumber);

            Colshape colshape = new SphereColshape(new Vector3(x, y, z), 4.0f, dim);
            colshape.SetData("SenderPhoneNumber", senderNumber);

            blips.Add(blip);
            colshapes.Add(colshape);

        }

        public static void SendMessage(object[] args) 
        {
            Events.CallRemote("Phone_SendMessage", args);
        }
        public static void PhoneSendGeo(object[] args)
        {            
            Events.CallRemote("SendGeo", args[0]);
        }
        public static void PhoneGetGeoCar(object[] args) 
        {            
            string veh_number = args[0].ToString();
            List<RAGE.Elements.Vehicle> vehicles = RAGE.Elements.Entities.Vehicles.All;

            int i = 0;
            foreach (Vehicle veh in vehicles)
            {
                string plate = veh.GetNumberPlateText().Replace(" ", string.Empty);
                if (plate == veh_number)
                {
                    Vector3 veh_pos = veh.Position;

                    Blip blip = new Blip(225, veh_pos, color: 84, shortRange: false, dimension: globalDimension);
                    blip.SetData("PlateNumber", veh_number);

                    Colshape colshape = new SphereColshape(veh_pos, 4.0f, globalDimension);
                    colshape.SetData("PlateNumber", 69);

                    blips.Add(blip);
                    colshapes.Add(colshape); 
                }

            }


        }
        public static void PhoneSendParkingCar(object[] args) 
        {            
            Events.CallRemote("GetCarFromGarage", args[0]);
        }

        #endregion



        #region Звонки

        public static void IncomingСall(object[] args) 
        {
            //if (callmarker)
            //{
            //    Events.CallRemote("Engaged", number); //вызываемый абонент занят
            //    return;
            //}
            //Chat.Output("Входящий звонок с номера:" + args[0]);   //////DEBUG
            phone.ExecuteJs("getCall('" + args[0].ToString() + "');");
            //phone.ExecuteJs("pushHistoryList('"+ args[0].ToString() + "', 'in');");
            number = (int)args[0];
            callmarker = true;
        }
        public static void CallConfirmed(object[] args) 
        {

            phone.ExecuteJs("toCall(" + (int)args[0] + ", 'out');");

            callmarker = true;
            RAGE.Elements.Player.LocalPlayer.VoiceVolume = 1f;
            RAGE.Voice.Muted = false;
            RAGE.Elements.Player.LocalPlayer.PlayAnim("static", "amb@code_human_wander_mobile@male@base", 1f, true, true, true, 1f, 8);
            RAGE.Elements.Player.LocalPlayer.PlayFacialAnim("mic_chatter", "mp_facial");
            RAGE.Elements.Player.LocalPlayer.TaskUseMobilePhoneTimed(100000);
            Volume(args);

            //Events.CallRemote("PlayerTalk.server");

        }
        public static void Volume(object[] args) // Volume
        {
            Player target = (Player)args[0];
            target.VoiceVolume = 1f;
            // Chat.Output("Volume пришел на клиент");//////DEBUG

        }

        public static void PhoneCheckCall(object[] args) // !Ned to correct!
        {
            callmarker = true;
            number = (int)args[0];
            //Chat.Output("Исходящий звонок на номер:" + args[0].ToString());   //////DEBUG
            //phone.ExecuteJs("pushHistoryList('" + number.ToString() + "', 'out');");
            //NAPI.Player.PlayPlayerAnimation(client, (int)(server_state.Constants.AnimationFlags.Loop | server_state.Constants.AnimationFlags.AllowPlayerControl), "amb@code_human_wander_mobile@male@base", "static");

            RAGE.Elements.Player.LocalPlayer.VoiceVolume = 1f;
            RAGE.Voice.Muted = false;
            RAGE.Elements.Player.LocalPlayer.PlayAnim("static", "amb@code_human_wander_mobile@male@base", 1f, true, true, true, 1f, 8);

            Events.CallRemote("CallPlayer", args[0]);
            //Events.CallRemote("PlayerTalk.server"); //И З Б А В И Т Ь С Я !!!
            //Events.CallRemote("Anim_PhoneTalk");
        }
        public static void СancelOutcomingCall(object[] args) // !Ned to correct!
        {

            //Chat.Output("Исходящий звонок на номер: " + args[0].ToString() + " СБРОЩЕН");   //////DEBUG
            GoHome(null);
            //phone.ExecuteJs("goHome();");
            //callmarker = false;
            //Player.LocalPlayer.VoiceVolume = 0f;
            //Voice.Muted = true;
            
            //Player.LocalPlayer.PlayFacialAnim("mood_normal_1", "facials@gen_male@variations@normal");

            Events.CallRemote("CancelCall", Convert.ToInt32(args[0]));
            //Events.CallRemote("PlayerStopTalk.server"); //И З Б А В И Т Ь С Я !!!
        }
        public static void AllowIncomingCall(object[] args) // !Ned to correct!
        {
            //Chat.Output("Поднять трубку: " + number);   //////DEBUG
            phone.ExecuteJs("toCall('" + number + "', 'in');");
            callmarker = true;
            RAGE.Elements.Player.LocalPlayer.VoiceVolume = 1f;
            RAGE.Voice.Muted = false;

            RAGE.Elements.Player.LocalPlayer.PlayFacialAnim("mic_chatter", "mp_facial");
            RAGE.Elements.Player.LocalPlayer.TaskUseMobilePhoneTimed(100000);

            Events.CallRemote("AgreeCall", number);
            //Events.CallRemote("PlayerTalk.server");//И З Б А В И Т Ь С Я !!!
            //Events.CallRemote("Anim_PhoneTalk");

        }
        public static void CancelIncomingCall(object[] args) 
        {
            //Chat.Output("Сбросить входящий с номера: " + number);   //////DEBUG
            Events.CallRemote("CancelCall", number);
            GoHome(null);
            //phone.ExecuteJs("goHome();");
            //callmarker = false;
            //RAGE.Elements.Player.LocalPlayer.VoiceVolume = 0f;
            //RAGE.Voice.Muted = true;
            //RAGE.Elements.Player.LocalPlayer.PlayFacialAnim("mood_normal_1", "facials@gen_male@variations@normal");
            //Events.CallRemote("PlayerStopTalk.server");

        }
        public void FastCall(object[] args)
        {
            string dept = args[0].ToString();
            string text = args[1].ToString();
            if (dept == "police")
            {
                Chat.Output("CallConfirmed");
                Events.CallRemote("PoliceCall", text);
            }
            if (dept == "ambulance")
            {
                Chat.Output("CallConfirmed");
                Events.CallRemote("MedicCall", text);
            }
        }

        #endregion


        
        public static void ClosePhone()
        {
            RAGE.Game.Mobile.DestroyMobilePhone();
            KeyManager.block = 0;
            Chat.Show(true);
            phone.ExecuteJs("phoneFadeOut();");
            phone.Active = false;
            Cursor.Visible = false;
            if (!callmarker)
            {
                Events.CallRemote("Anim_ClosePhoneClient");
                usePhoneMarker = false;
            }

        }

        public static void Phone_cef_up(object[] args) //цефку вверх
        {
            //Chat.Output("Цефка телефона вверху");//////DEBUG
            if (phone != null)
                phone.ExecuteJs("phoneToTop();");

        }
        public static void Phone_cef_down(object[] args) //цефку вниз
        {
            //Chat.Output("Цефка телефона внизу");//////DEBUG
            if (phone != null)
                phone.ExecuteJs("phoneToBottom();");

        }


        public static void ExitColShape(Colshape colshape, CancelEventArgs cancel)
        {
            if (colshape.GetData<int>("garage") == 999)
            {

                Events.CallRemote("ExitHomeGarage");
                return;
            }

        }

        /// <summary>
        /// Удаление маркеров и блипов геопозиций на клиенте
        /// </summary>
        /// <param name="colshape"></param>
        /// <param name="cancel"></param>
        public static void DeliteBlipAndColshape(Colshape colshape, CancelEventArgs cancel)
        {

            //Chat.Output("ENTER COLSHAPE");//////DEBUG       
            try
            {
                if (colshape.GetSharedData("repapairCords") != null)
                {
                    mechanic_client.Ticks_Mechs.onCoods = true;
                }

                    //Chat.Output(colshape.GetSharedData("nameBuis").ToString());
                    
                // mechanic_client.Mechanic_Client.onMarker = true;
            }
            catch
            {

            }

            try
            {
                if (colshape.GetData<int>("garage") != null)
                    if (colshape.GetData<int>("garage") == 999)
                    {

                        Events.CallRemote("EnterHomeGarage");
                        return;
                    }
            }
            catch
            {

            }

            for (int i = 0; i < colshapes.Count; i++)
            {
                Colshape shape = colshapes[i];
                try
                {
                    if (colshape.GetData<int>("SenderPhoneNumber") == shape.GetData<int>("SenderPhoneNumber"))
                    {
                        shape.Destroy();
                        colshapes.RemoveAt(i);
                        i--;
                        for (int ii = 0; ii < blips.Count; ii++)
                        {
                            Blip blip = blips[ii];
                            if (colshape.GetData<int>("SenderPhoneNumber") == blip.GetData<int>("SenderPhoneNumber"))
                            {
                                blip.Destroy();
                                blips.RemoveAt(ii);
                                ii--;
                            }
                        }
                    }
                }
                catch
                {

                }

                try
                {
                    if (colshape.GetData<int>("SenderPhoneNumber") == shape.GetData<int>("SenderPhoneNumber"))
                    {
                        shape.Destroy();
                        colshapes.RemoveAt(i);
                        i--;

                        for (int ii = 0; ii < blips.Count; ii++)
                        {
                            Blip blip = blips[ii];
                            if (colshape.GetData<int>("SenderPhoneNumber") == blip.GetData<int>("SenderPhoneNumber"))
                            {
                                blip.Destroy();
                                blips.RemoveAt(ii);
                                ii--;
                            }
                        }
                    }
                }
                catch
                {

                }


                try
                {
                    if (colshape.GetData<string>("PlateNumber") == shape.GetData<string>("PlateNumber"))
                    {
                        shape.Destroy();
                        colshapes.RemoveAt(i);
                        i--;

                        for (int ii = 0; ii < blips.Count; ii++)
                        {
                            Blip blip = blips[ii];
                            if (colshape.GetData<string>("PlateNumber") == blip.GetData<string>("PlateNumber"))
                            {
                                blip.Destroy();
                                blips.RemoveAt(ii);
                                ii--;
                            }
                        }
                    }
                }
                catch
                {

                }

                try
                {
                    if (colshape.GetData<string>("PlateNumber") == shape.GetData<string>("PlateNumber"))
                    {
                        shape.Destroy();
                        colshapes.RemoveAt(i);
                        i--;

                        for (int ii = 0; ii < blips.Count; ii++)
                        {
                            Blip blip = blips[ii];
                            if (colshape.GetData<string>("PlateNumber") == blip.GetData<string>("PlateNumber"))
                            {
                                blip.Destroy();
                                blips.RemoveAt(ii);
                                ii--;
                            }
                        }
                    }
                }
                catch
                {

                }
            }


        }











































    }
}
