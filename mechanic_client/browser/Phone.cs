using cs_packages.client;
using cs_packages.player;
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
        static bool usePhoneMarker = false;


        static public HtmlWindow phone = null;
        public static List<RAGE.Elements.Player> PlayerInRadio = new List<RAGE.Elements.Player>();
        public static List<Blip> blips = new List<Blip>();
        public static List<Colshape> colshapes = new List<Colshape>();



        static int n = 0; //номер темы на телефоне (от 0 до 13)

        public static int number;

        public Phone()
        {
            //EVENTS
            OnPlayerEnterColshape += DeliteBlipAndColshape;
            OnPlayerExitColshape += ExitColShape;
           // RAGE.Game.Invoker.Invoke
            //Events.Add("caracara", CaraCara);
            //////From server
            ///
            Events.Add("openclose.phone", OpenClose);
            Events.Add("lockPhone", OpenClose);
            Events.Add("WallpaperToClient", WallpaperForClient); //Загрузка темы
            Events.Add("ContactsToClient", ContactsForClient); //Загузка контактов
            Events.Add("GetGeo", GetGeo); //получить геопозицию
            //Events.Add("GetPoint", GetPoint); //получить точку маршрута
            //Events.Add("DeliteBlip", DeliteBlip); //удалить блип
            Events.Add("PushPhoneBalance", PushPhoneBalance); //изменение баланса
            //Events.Add("GetPhoneWallpaper", GetPhoneWallpaper);
            //Events.Add("ContactsFromDB", ContactsFromDB);
            Events.Add("MyOutcomingMessage", MyOutcomingMessage);//исходящее сообщение (для отрисовки)
            Events.Add("IncomingMessage", IncomingMessage);//входящее сообщение
            Events.Add("GoHome", GoHome);//сброс на начальный экран
            Events.Add("AddFriendInContacts", AddFriendInContacts);//добавление друга в контакты
            ///ЗВОНОК
            Events.Add("IncomingСall", IncomingСall);//входящий вызов
            Events.Add("CallConfirmed", CallConfirmed);//исходящий вызов подтвержден
            Events.Add("Volume", Volume);//Volume
            ///Сел/вышел из машины
            Events.Add("EnterVehicle", EnterVehicle);//сел в машину (цефку вверх)
            Events.Add("ExitVehicle", ExitVehicle);//вышел из машины (цефку вниз)
            ///Приложение ТАЧКИ



            //////From CEF
            ///
            Events.Add("phoneWallpaper", PhoneWallpaper); //изменение темы
            Events.Add("refreshedContacts", RefreshedContacts);//добавление/удаление контакта (перезапись контактов)
            Events.Add("sendMessage", SendMessage);//исходящее сообщение //mp.trigger("sendMessage", contactsList[currentIndex].number, currentMessage);
            Events.Add("PhoneSendGeo", PhoneSendGeo);////mp.trigger('PhoneSendGeo', currentElem.number);
                                                     //Events.Add("PhoneSendPoint", PhoneSendPoint);////mp.trigger('PhoneSendPoint', currentElem.number);

            ///ЗВОНОК
            Events.Add("PhoneCheckCall", PhoneCheckCall);//исходящий вызов //mp.trigger('PhoneCheckCall', number);
            Events.Add("cancelOutcomingCall", СancelOutcomingCall); //сбросить исходящийзвонок //mp.trigger("cancelOutcomingCall",getNumber, status); -отбой исходящего(в двух случаях, при дозвоне и при самом разговоре)
            Events.Add("allowIncomingCall", AllowIncomingCall); //поднять трубку //mp.trigger("allowIncomingCall", getNumber); -приём входящего
            Events.Add("cancelIncomingCall", CancelIncomingCall); //сбросить входящийзвонок //mp.trigger("cancelIncomingCall", getNumber); -отбой входящего

            ///Приложение ТАЧКИ
            Events.Add("PhoneSendGeoCar", PhoneGetGeoCar); //mp.trigger('PhoneSendGeoCar', currentElem.number);
            Events.Add("PhoneSendParkingCar", PhoneSendParkingCar); //mp.trigger('PhoneSendGeoCar', currentElem.number);



            Events.Add("fastCall", FastCall); //mp.trigger('PhoneSendGeoCar', currentElem.number);

        }


        public static void LoadCef()  //Включение телефона
        {
            phone = new HtmlWindow("package://auth/assets/phone.html");
            phone.Active = false;
            Events.CallRemote("GetWallpaperToClient"); //Загрузка темы и контактов из базы данных при включении телефона
            //Events.CallRemote("GetContactsToClient");  //Загрузка котакт ов из БД
            //Events.CallRemote("SetPhoneNumber_ID");//////DEBUG (принудительное добавление номера теелефона в PlayerData) //////DEBUG 


        }

        public void FastCall(object[] args)
        {
            string dept = args[0].ToString();
            string text = args[1].ToString();
            if(dept == "police")
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

                KeyManager.block = 10;
                Chat.Show(false);
                phone.Active = true;
                phone.ExecuteJs("phoneFadeIn();");
                phone.ExecuteJs("pushPhoneBalance('" + args[0] + "');");
                Cursor.Visible = true;
                usePhoneMarker = true;
                //// phone.ExecuteJs("settingsInitialize(" + 7 + ");");
                //Chat.Output(args[0].ToString());
                //Chat.Output(args[1].ToString());

                Events.CallRemote("Anim_OpenPhoneClient");
                DrawInfo.LoadScreen =false;

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
                    usePhoneMarker = false;
                }


            }
        }

        public static void WallpaperForClient(object[] args)
        {
            phone.ExecuteJs("settingsInitialize(" + Convert.ToInt32(args[0]) + ");");
            //Chat.Output("Текущие обои (из БД): " + args[0].ToString());
        }

        public static void PhoneWallpaper(object[] args)
        {
            //int n = (int)args[0];
            //phone.ExecuteJs($"settingsInitialize(int {n});");
            //Events.CallRemote("Phone_SetNewWallpaper", n);
            //n = (int)args[0];


            Events.CallRemote("Phone_SetNewWallpaper", args[0]);

            //Chat.Output("Новые обои: " + args[0].ToString());
            phone.ExecuteJs("settingsInitialize(" + Convert.ToInt32(args[0]) + ");");


        }

        public static void ContactsForClient(object[] args)
        {
            phone.ExecuteJs("pushContactList('" + args[0].ToString() + "', '" + args[1].ToString() + "');");
            //  phone.ExecuteJs("incomingMessage('" + args[0] + "', 'incoming', '" + args[1].ToString() + "', '" + args[2].ToString() + "');");

            //Chat.Output("Контакты (из БД): " + args[0].ToString());       //////DEBUG

        }

        public static void RefreshedContacts(object[] args)
        {
            //    Chat.Output("Создан новый контакт: " + args[0].ToString());

            Events.CallRemote("Phone_RefreshedContact", args[0]);




        }

        public static void AddFriendInContacts(object[] args)
        {
            string nicname = args[0].ToString();
            int number = (int)args[1];

            phone.ExecuteJs("addContact('" + nicname + "', '" + number.ToString() + "');");


        }

        public static void SendMessage(object[] args) //исходящее сообщение
        {
            //Chat.Output("Отправка сообщения: " + args[0].ToString());//////DEBUG
            //Chat.Output("Отправка сообщения: " + args[0].ToString());//////DEBUG
            //Chat.Output("Отправка сообщения: ");//////DEBUG

            Events.CallRemote("Phone_SendMessage", args);




        }

        public static void IncomingMessage(object[] args) //входящее сообщение
        {
            //Chat.Output("Входящее сообщение от:"+ args[1].ToString() + "Приходит: " + args[0].ToString());//////DEBUG
            //Chat.Output("Входящее сообщение: " +args[0].ToString()); //////DEBUG
            //Chat.Output("Время: " +args[1].ToString());              //////DEBUG
            //Chat.Output("Сообщение: " +args[2].ToString());          //////DEBUG

            ////incomingMessage(number, time, message) - Входящее сообщение 
            //phone.ExecuteJs("incomingMessage('" + args[0] + "', '" + args[1].ToString() + "', '" + args[2].ToString() + "');");
            phone.ExecuteJs("incomingMessage('" + args[0] + "', 'incoming', '" + args[1].ToString() + "', '" + args[2].ToString() + "');");

        }

        public static void MyOutcomingMessage(object[] args) //исходящее сообщение (отрисовка)
        {
            //Chat.Output("Входящее сообщение от:"+ args[1].ToString() + "Приходит: " + args[0].ToString());//////DEBUG
            //Chat.Output("Входящее сообщение: " +args[0].ToString()); //////DEBUG
            //Chat.Output("Время: " +args[1].ToString());              //////DEBUG
            //Chat.Output("Сообщение: " +args[2].ToString());          //////DEBUG

            ////incomingMessage(number, time, message) - Входящее сообщение 
            phone.ExecuteJs("incomingMessage('" + args[0] + "', 'outcoming', '" + args[1].ToString() + "', '" + args[2].ToString() + "');");

        }

        public static void PhoneSendGeo(object[] args) //отправкаа геопозиции
        {
            Chat.Output("ОТПРАВКА ГЕОПОЗИЦИИ");//////DEBUG
            Events.CallRemote("SendGeo", args[0]);
        }


        public static void GetGeo(object[] args) //получение геопозиции
        {

            float x = (float)args[0];
            float y = (float)args[1];
            float z = (float)args[2];

            int senderNumber = (int)args[3];

            //Chat.Output("ПРИЕМ ГЕОПОЗИЦИИ: phone number - "+ args[3] + " x - " + args[0] + " y - " + args[1] + " z - " + args[2]);//////DEBUG            

            Blip blip = new Blip(280, new Vector3(x, y, z), color: 84, shortRange: false, dimension: globalDimension);
            blip.SetData("SenderPhoneNumber", senderNumber);

            Colshape colshape = new SphereColshape(new Vector3(x, y, z), 4.0f, globalDimension);
            colshape.SetData("SenderPhoneNumber", senderNumber);

            blips.Add(blip);
            colshapes.Add(colshape);

        }


        public static void PhoneCheckCall(object[] args) //исходящий вызов //mp.trigger('PhoneCheckCall', number);
        {
            Chat.Output("Исходящий звонок на номер:" + args[0].ToString());   //////DEBUG
            Events.CallRemote("CallPlayer", args[0]);

            number = (int)args[0];
            //phone.ExecuteJs("pushHistoryList('" + number.ToString() + "', 'out');");

            callmarker = true;



            RAGE.Elements.Player.LocalPlayer.VoiceVolume = 1f;
            RAGE.Voice.Muted = false;
            //   NAPI.Player.PlayPlayerAnimation(client, (int)(server_state.Constants.AnimationFlags.Loop | server_state.Constants.AnimationFlags.AllowPlayerControl), "amb@code_human_wander_mobile@male@base", "static");

            RAGE.Elements.Player.LocalPlayer.PlayAnim("static", "amb@code_human_wander_mobile@male@base", 1f, true, true, true, 1f, 8);


            Events.CallRemote("PlayerTalk.server");
            Events.CallRemote("Anim_PhoneTalk");
        }

        public static void CallConfirmed(object[] args) //исходящий вызов подтвержден
        {

            phone.ExecuteJs("toCall(" + (int)args[0] + ", 'out');");

            callmarker = true;
            RAGE.Elements.Player.LocalPlayer.VoiceVolume = 1f;
            RAGE.Voice.Muted = false;
            RAGE.Elements.Player.LocalPlayer.PlayAnim("static", "amb@code_human_wander_mobile@male@base", 1f, true, true, true, 1f, 8);
            RAGE.Elements.Player.LocalPlayer.PlayFacialAnim("mic_chatter", "mp_facial");
            RAGE.Elements.Player.LocalPlayer.TaskUseMobilePhoneTimed(100000);
            Events.CallRemote("PlayerTalk.server");

        }

        public static void IncomingСall(object[] args) //входящий вызов
        {
            if (callmarker)
            {
                Events.CallRemote("Engaged", number); //вызываемый абонент занят
                return;
            }


            Chat.Output("Входящий звонок с номера:" + args[0]);   //////DEBUG


            phone.ExecuteJs("getCall('" + args[0] + "');");
            //phone.ExecuteJs("pushHistoryList('"+ args[0].ToString() + "', 'in');");
            number = (int)args[0];
            callmarker = true;


        }


        public static void СancelOutcomingCall(object[] args) //Сбросить исходящий
        {

            //Chat.Output("Исходящий звонок на номер: " + args[0].ToString() + " СБРОЩЕН");   //////DEBUG
            Events.CallRemote("CancelCall", Convert.ToInt32(args[0]));
            phone.ExecuteJs("goHome();");
            callmarker = false;
            Player.LocalPlayer.VoiceVolume = 0f;
            Voice.Muted = true;

            Player.LocalPlayer.PlayFacialAnim("mood_normal_1", "facials@gen_male@variations@normal");
            Events.CallRemote("PlayerStopTalk.server");
        }


        public static void AllowIncomingCall(object[] args) //Поднять трубку
        {
            //Chat.Output("Поднять трубку: " + number);   //////DEBUG
            phone.ExecuteJs("toCall('" + number + "', 'in');");
            Events.CallRemote("AgreeCall", number);
            callmarker = true;
            RAGE.Elements.Player.LocalPlayer.VoiceVolume = 1f;
            RAGE.Voice.Muted = false;

            RAGE.Elements.Player.LocalPlayer.PlayFacialAnim("mic_chatter", "mp_facial");
            RAGE.Elements.Player.LocalPlayer.TaskUseMobilePhoneTimed(100000);
            Events.CallRemote("PlayerTalk.server");
            Events.CallRemote("Anim_PhoneTalk");

        }

        public static void CancelIncomingCall(object[] args) //Сбросить входящий
        {
            //Chat.Output("Сбросить входящий с номера: " + number);   //////DEBUG
            Events.CallRemote("CancelCall", number);

            phone.ExecuteJs("goHome();");
            callmarker = false;

            RAGE.Elements.Player.LocalPlayer.VoiceVolume = 0f;
            RAGE.Voice.Muted = true;
            RAGE.Elements.Player.LocalPlayer.PlayFacialAnim("mood_normal_1", "facials@gen_male@variations@normal");
            Events.CallRemote("PlayerStopTalk.server");

        }

        public static void GoHome(object[] args) //GO HOME()
        {
            phone.ExecuteJs("goHome();");
            callmarker = false;
            RAGE.Elements.Player.LocalPlayer.VoiceVolume = 0f;
            RAGE.Voice.Muted = true;
            RAGE.Elements.Player.LocalPlayer.PlayFacialAnim("mood_normal_1", "facials@gen_male@variations@normal");
            Events.CallRemote("PlayerStopTalk.server");

            if (!usePhoneMarker) Events.CallRemote("Anim_ClosePhoneClient");


        }

        public static void Volume(object[] args) // Volume
        {
            Player target = (Player)args[0];
            target.VoiceVolume = 1f;
            Chat.Output("Volume пришел на клиент");//////DEBUG

        }

        public static void PushPhoneBalance(object[] args) //Баланс телефона
        {
            int count = (int)args[0];
            phone.ExecuteJs("pushPhoneBalance('" + count + "')");


        }

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








        public static void PhoneGetGeoCar(object[] args) //отправкаа геопозиции тачки
        {

            //Chat.Output("ОТПРАВКА ГЕОПОЗИЦИИ ТАЧКИ");//////DEBUG

            string veh_number = args[0].ToString();
            List<RAGE.Elements.Vehicle> vehicles = RAGE.Elements.Entities.Vehicles.All;


            //   Chat.Output("На сервере " + vehicles.Count.ToString() + " автомобилей");//////DEBUG
            int i = 0;//////DEBUG
            foreach (Vehicle veh in vehicles)
            {
                string plate = veh.GetNumberPlateText().Replace(" ", string.Empty);
                if (plate == veh_number)
                {

                    Vector3 veh_pos = veh.Position;

                    //  Chat.Output("Авто №" + i + " NumberPlate: " + veh_number); //////DEBUG

                    Blip blip = new Blip(225, veh_pos, color: 84, shortRange: false, dimension: globalDimension);
                    blip.SetData("PlateNumber", veh_number);

                    Colshape colshape = new SphereColshape(veh_pos, 4.0f, globalDimension);
                    colshape.SetData("PlateNumber", 69);

                    blips.Add(blip);
                    colshapes.Add(colshape);

                    i++;//////DEBUG



                }

                //    //RAGE.Elements.Vehicle vehicle = vehicles.Find(veh => veh.Position.DistanceTo2D(pos) <= 5f);
                //    //vehicle = vehicles.Find(veh => veh.Position.DistanceTo2D(RAGE.Elements.Player.LocalPlayer.Position) <= 5f);


            }


        }
        public static void PhoneSendParkingCar(object[] args) //отправкаа парковки тачки
        {
            //  Chat.Output("ОТПРАВКА ПАРКИНГА ТАЧКИ");//////DEBUG

            Events.CallRemote("GetCarFromGarage", args[0]);

        }
        public static void EnterVehicle(object[] args) //цефку вверх
        {
            // Chat.Output("Цефка телефона вверху");//////DEBUG
            if (phone != null)
                phone.ExecuteJs("phoneToTop();");

        }
        public static void ExitVehicle(object[] args) //цефку вниз
        {
            //  Chat.Output("Цефка телефона внизу");//////DEBUG

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
                if (colshape.GetData<int>("garage") != null)
                    if (colshape.GetData<int>("garage") == 999)
                    {

                        Events.CallRemote("EnterHomeGarage");
                        return;
                    }
            }
            catch(Exception e)
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
