﻿using RAGE;
using RAGE.Ui;
using System.Linq;
using cs_packages.client;
using cs_packages.browsers;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using cs_packages.vehicle;
using RAGE.Elements;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using RAGE.Game;

namespace cs_packages.player
{
    class PlayerState : Events.Script
    {
        bool Player_In_Car = false;
        static public bool block;
        static public RAGE.Elements.Vehicle vehicle;
        static public Vector3 coord;
        public bool DEBUG = true;
        public static bool IDEnabled = false;
        public static List<Friends> Friends = new List<Friends>();
        static public RAGE.Elements.Ped ped_Driver;
        public static string[] Zones = new string[]
        {
           "AIRP",
           "ALAMO",
           "ALTA",
           "ARMYB",
           "BANHAMC",
           "BANNING",
           "BEACH",
           "BHAMCA",
           "BRADP",
           "BRADT",
           "BURTON",
           "CALAFB",
           "CANNY",
           "CCREAK",
           "CHAMH",
           "CHIL",
           "CHU",
           "CMSW",
           "CYPRE",
           "DAVIS",
           "DELBE",
           "DELPE",
           "DELSOL",
           "DESRT",
           "DOWNT",
           "DTVINE",
           "EAST_V",
           "EBURO",
           "ELGORL",
           "ELYSIAN",
           "GALFISH",
           "GOLF",
           "GRAPES",
           "GREATC",
           "HARMO",
           "HAWICK",
           "HORS",
           "HUMLAB",
           "JAIL",
           "KOREAT",
           "LACT",
           "LAGO",
           "LDAM",
           "LEGSQU",
           "LMESA",
           "LOSPUER",
           "MIRR",
           "MORN",
           "MOVIE",
           "MTCHIL",
           "MTGORDO",
           "MTJOSE",
           "MURRI",
           "NCHU",
           "NOOSE",
           "OCEANA",
           "PALCOV",
           "PALETO",
           "PALFOR",
           "PALHIGH",
           "PALMPOW",
           "PBLUFF",
           "PBOX",
           "PROCOB",
           "RANCHO",
           "RGLEN",
           "RICHM",
           "ROCKF",
           "RTRAK",
           "SANAND",
           "SANCHIA",
           "SANDY",
           "SKID",
           "SLAB",
           "STAD",
           "STRAW",
           "TATAMO",
           "TERMINA",
           "TEXTI",
           "TONGVAH",
           "TONGVAV",
           "VCANA",
           "VESP",
           "VINE",
           "WINDF",
           "WVINE",
           "ZANCUDO",
           "ZP_ORT",
           "ZQ_UAR",
        };
        public static string[] ZonesRes = new string[]
      {
           "Национальный аэропорт Лос-Сантоса",
           "Море Аламо",
           "Альта",
           "Форт-Занкудо",
           "Шоссе Бингем-Каньон",
           "Пляж Беннинг",
           "Пляж Веспуччи",
           "Бингем-Каньон",
           "Проход Брэддок",
           "Тунель Бреддок",
           "Бёртон",
           "Калафия",
           "Ратон-Каньон",
           "Залив Кэссиди",
           "Чемберлен-Хиллз",
           "Вайнвуд-Хиллз",
           "Чумаш",
           "Заповедник горы Чилиад",
           "Кипарисовые Квартиры",
           "Дэвис",
           "Пляж Дель-Перро",
           "Дель-Перро",
           "Ля Пуэрта",
           "Пустыня Гранд-Сенора",
           "Центр",
           "Центр Вайнвуд",
           "Восточный Вайнвуд",
           "Высота Эль-Бурро",
           "Маяк Эль-Гордо",
           "Острова Элизиум",
           "Галилео-Парк",
           "Гольф-клуб",
           "Грейпсид",
           "Грейт-Чапаррал",
           "Хармони",
           "Хавик",
           "Ипподром Вайнвуд",
           "Научно-исследовательская лаборатория",
           "Тюрьма Болингброук",
           "Малый Сеул",
           "Заповедник",
           "Лаго-Занкудо",
           "Плотина",
           "Площадь Легиона",
           "Ла-Меса",
           "Ля-Пуэрта",
           "Миррор Парк",
           "Морнингвуд",
           "Ричардс Маджестик",
           "Гора Чилиад",
           "Гора Гордо",
           "Гора Джосайя",
           "Высота Мурриета",
           "Северный Чумаш",
           "N.O.O.S.E",
           "Тихий Океан",
           "Бухта Палето",
           "Палето-Бэй",
           "Палето-Форест",
           "Нагорье Паломино",
           "Электростанция Палмер-Тайлер",
           "Тихоокеанский Блеф",
           "Пилбокс-Хилл",
           "Пляж Прокопио",
           "Ранчо",
           "Ричман-Глен",
           "Ричман",
           "Рокфорд-Хиллз",
           "Редвуд Лайт-Трак",
           "Сан Андреас",
           "Сан-Шаньский горный хребет",
           "Сэнди-Шорс",
           "Мишн-Роу",
           "Стэб-Сити",
           "Maze Bank Arena",
           "Строберри",
           "Татавиамские горы",
           "Терминал",
           "Текстайл-Сити",
           "Тонгва-Хиллз",
           "Тонгва",
           "Каналы Веспуччи",
           "Веспуччи",
           "Вайнвуд",
           "Ветряная ферма Ron Alternates",
           "Западный Вайнвуд",
           "Река Занкудо",
           "Южный Порт",
           "Дэвис-Кварц",
      };

        public PlayerState()
        {

            
            //Chat.Output("Sansara RP");
            //Chat.Output("Закрытый альфа тест");
            //Chat.Output("Никому не передавайте данные для входа");

            Chat.Activate(false);


            RAGE.Nametags.Enabled = false;
            RAGE.Voice.Muted = true;
            RAGE.Elements.Player.LocalPlayer.VoiceVolume = 1;
            //player.VoiceVolume
            RAGE.Game.Misc.SetFadeOutAfterDeath(false);
            RAGE.Game.Player.SetPlayerHealthRechargeMultiplier(0.0f);
         
            RAGE.Game.Misc.DisableAutomaticRespawn(true);
            Events.Add("LoginCefCreate.client", LoginCefCreate);
            Events.Add("registerLogin.client", RegisterLogin);
            Events.Add("login.destroy", LoginDestroy);
            Events.Add("register.destroy", RegisterDestroy);
          //  RAGE.Elements.Entities.Peds.GetAtRemote
           // Events.Add("freeze", Freeze);
            Events.Add("fly", Fly);
            Events.Add("Time", Time);
            Events.Add("stopfly", StopFly);
            Events.Add("freeze", Freeze);
            Events.Add("freezestop", FreezeStop);
            Events.Add("cufffreeze", cuffFreeze);
            Events.Add("cufffreezestop", cuffFreezeStop);
            Events.Add("getweaponammo", GetWeapon);
            Events.Add("removeweapon", RemoveAmmo);
            Events.Add("loaded", Loaded);
            Events.Add("SetFriends", SetFriends);
            Events.Add("BigData", BigData);
            Events.Add("InJail", InJail);
            Events.Add("InJailFalse", InJailFalse);
            Events.Add("getweaponammoinv", GetWeaponInv);
            Events.Add("blip_test", onTestEvent);
            Events.Add("gameInactive", GameInactive);
            Events.Add("gameActive", GameActive);
            Events.Add("soundnotification", SoundNotification);
            Events.Add("GetObject", GetObject);
            Events.Add("Drive", CarDrive);
            Events.Add("Drive2", CarsDrives);
            ///player.TriggerEvent("GetClothesDLC", slot, id);
            Events.Add("GetClothesDLC", GetClothesDLC);
            Events.OnPlayerStartEnterVehicle += OnPlayerStartEnterVehicle;
           
            Events.OnPlayerEnterVehicle += OnPlayerEnterVehicle;
            Events.OnPlayerLeaveVehicle += OnPlayerLeaveVehicle;
            Events.OnPlayerWeaponShot += OnPlayerWeaponShot;

            RAGE.Game.Ui.SetHudComponentPosition(6, -0.5f,0f );
            RAGE.Game.Ui.SetHudComponentPosition(7, 20f, 0);
            RAGE.Game.Ui.SetHudComponentPosition(8, -0.5f, 0);
            RAGE.Game.Ui.SetHudComponentPosition(9, 20f, -0.1f);
            RAGE.Game.Ui.SetHudComponentPosition(2, -0.5f, -0.2f);
            RAGE.Game.Ui.SetHudComponentPosition(20, 20f, -0.2f);

            RAGE.Game.Pad.DisableControlAction(0, 75, true);
            RAGE.Game.Pad.DisableControlAction(2, 75, true);
            RAGE.Game.Pad.DisableControlAction(0, 58, true);
         
            //ped = new RAGE.Elements.Ped(1413662315, new Vector3(1863.297f, 3745.99f, 33.03188f));
            //ped.ActivatePhysics();
            //ped.SetVisible(true, true);
            //ped.Dimension = RAGE.Elements.Player.LocalPlayer.Dimension;
            //RAGE.Game.Ped.CreatePed(4, 1413662315, 1863.297f, 3745.99f, 33.03188f,0.0f, false, true);
            //  Ped myPed = NAPI.Ped.CreatePed(PedHash.Acult01AMM, Tattoe_SandyShores_1_NPC, 0, NAPI.GlobalDimension);

            uint dimension = 4294967295;

            //string str = "ХУИТА";
            //Encoding srcEncodingFormat = Encoding.UTF8;
            //Encoding dstEncodingFormat = Encoding.GetEncoding("windows-1251");
            //byte[] originalByteString = srcEncodingFormat.GetBytes(str);
            //byte[] convertedByteString = Encoding.Convert(srcEncodingFormat,
            //dstEncodingFormat, originalByteString);
            //string finalString = dstEncodingFormat.GetString(convertedByteString);


            //    Blip blip = new Blip(225, new Vector3(1f, 1f, 1f), name: "0KDQo9Ch0KHQmtCY0Jk=", color: 84, shortRange: false, dimension: 4294967295);
            //     string names = "русский текст";
            //const int maxStringLength = 50;
            // blip.
            // for (int i = 0; i < names.Length; i += maxStringLength)
            //{
            //  blip.SetNameFromTextFile("name.txt");   //(names.Substring(i, System.Math.Min(maxStringLength, names.Length - i)));
            // }
            // Ui.EndTextCommandPrint(duration, true);
          

            #region NPC в банках
            RAGE.Elements.Ped ped_Bank_Alta_ticket_1 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(313.8392f, -280.7066f, 54.16471f), 340f, dimension);
            RAGE.Elements.Ped ped_Bank_Alta_ticket_2 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(312.4359f, -280.1905f, 54.16467f), 340f, dimension);
            RAGE.Elements.Ped ped_Bank_PilboxHill_ticket_1 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(149.4745f, -1042.337f, 29.368f), 335f, dimension);
            RAGE.Elements.Ped ped_Bank_PilboxHill_ticket_2 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(147.9897f, -1041.791f, 29.36794f), 335f, dimension);
            RAGE.Elements.Ped ped_Bank_PalletBay_ticket_1 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(-112.2959f, 6471.099f, 31.62671f), 133.7f, dimension);
            RAGE.Elements.Ped ped_Bank_PalletBay_ticket_2 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(-111.1828f, 6470.082f, 31.62671f), 133.7f, dimension);
            RAGE.Elements.Ped ped_Bank_PalletBay_ticket_3 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(-110.1342f, 6468.936f, 31.62671f), 133.7f, dimension);
            RAGE.Elements.Ped ped_Bank_СanyonBanham_ticket_1 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(-2961.078f, 483.0013F, 15.697f), 86f, dimension);
            RAGE.Elements.Ped ped_Bank_СanyonBanham_ticket_2 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(-2961.212f, 481.4662f, 15.69695f), 86f, dimension);
            RAGE.Elements.Ped ped_Mainbank_WinewoodCenter_ticket_1 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(241.7276f, 226.9445f, 106.2871f), 156f, dimension);
            RAGE.Elements.Ped ped_Mainbank_WinewoodCenter_ticket_2 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(243.324f, 226.32f, 106.2875f), 156f, dimension);
            RAGE.Elements.Ped ped_Mainbank_WinewoodCenter_ticket_3 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(247.0025f, 225.0255f, 106.2876f), 156f, dimension);
            RAGE.Elements.Ped ped_Mainbank_WinewoodCenter_ticket_4 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(248.8379f, 224.353f, 106.2871f), 156f, dimension);
            RAGE.Elements.Ped ped_Mainbank_WinewoodCenter_ticket_5 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(252.1458f, 223.1474f, 106.2868f), 156f, dimension);
            RAGE.Elements.Ped ped_Mainbank_WinewoodCenter_ticket_6 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(254.0283f, 222.491f, 106.2868f), 156f, dimension);
            RAGE.Elements.Ped ped_Bank_GrandSenoraDesert_ticket_1 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(1174.955f, 2708.255f, 38.08796f), 175f, dimension);
            RAGE.Elements.Ped ped_Bank_GrandSenoraDesert_ticket_2 = new RAGE.Elements.Ped(0xB7C61032, new Vector3(1176.428f, 2708.217f, 38.08788f), 175f, dimension);

            //ped_Mainbank_WinewoodCenter_ticket_3.PlayAnim("veh@boat@speed@fps@", "sit_slow", 10f, true, true, true, 0, 0);
            #endregion

            #region NPC в магазинах 24/7

            RAGE.Elements.Ped ped_shop_1_ChiliadMountain = new RAGE.Elements.Ped(0x820B33BD, new Vector3(1728.633f, 6416.761f, 35.03723f), 249f, dimension);
            RAGE.Elements.Ped ped_shop_2_Grapeseed = new RAGE.Elements.Ped(0x820B33BD, new Vector3(1697.24f, 4923.381f, 42.06367f), 325f, dimension);
            RAGE.Elements.Ped ped_shop_3_SandyShores = new RAGE.Elements.Ped(0x820B33BD, new Vector3(1391.549f, 3605.958f, 34.98092f), 201f, dimension);
            RAGE.Elements.Ped ped_shop_4_SandyShores = new RAGE.Elements.Ped(0x820B33BD, new Vector3(1959.22f, 3741.485f, 32.34375f), 303f, dimension);
            RAGE.Elements.Ped ped_shop_5_GrandSenoraDesert = new RAGE.Elements.Ped(0x820B33BD, new Vector3(2676.453f, 3280.261f, 55.24114f), 327f, dimension);
            RAGE.Elements.Ped ped_shop_6_GrandSenoraDesert = new RAGE.Elements.Ped(0x820B33BD, new Vector3(549.3411f, 2669.652f, 42.15652f), 95f, dimension);
            RAGE.Elements.Ped ped_shop_7_GrandSenoraDesert = new RAGE.Elements.Ped(0x820B33BD, new Vector3(1165.293f, 2710.876f, 38.15771f), 185f, dimension);
            RAGE.Elements.Ped ped_shop_8_Сhumash = new RAGE.Elements.Ped(0x820B33BD, new Vector3(-3243.956f, 1000.128f, 12.83071f), 357f, dimension);
            RAGE.Elements.Ped ped_shop_9_CanyonBenham = new RAGE.Elements.Ped(0x820B33BD, new Vector3(-3040.556f, 583.9944f, 7.908929f), 14f, dimension);
            RAGE.Elements.Ped ped_shop_10_CanyonBenham = new RAGE.Elements.Ped(0x820B33BD, new Vector3(-2966.312f, 391.5856f, 15.04331f), 91f, dimension);
            RAGE.Elements.Ped ped_shop_11_RichmanGlen = new RAGE.Elements.Ped(0x820B33BD, new Vector3(-1819.508f, 793.5573f, 138.0857f), 139f, dimension);
            RAGE.Elements.Ped ped_shop_12_WinewoodCenter = new RAGE.Elements.Ped(0x820B33BD, new Vector3(372.9146f, 328.1199f, 103.5664f), 255f, dimension);
            RAGE.Elements.Ped ped_shop_13_TataviamMountains = new RAGE.Elements.Ped(0x820B33BD, new Vector3(2555.539f, 380.8298f, 108.623f), 8f, dimension);
            RAGE.Elements.Ped ped_shop_14_Morningwood = new RAGE.Elements.Ped(0x820B33BD, new Vector3(-1486.813f, -377.4867f, 40.16341f), 130f, dimension);
            RAGE.Elements.Ped ped_shop_15_Mirrorpark = new RAGE.Elements.Ped(0x820B33BD, new Vector3(1164.893f, -323.6125f, 69.20506f), 97f, dimension);
            RAGE.Elements.Ped ped_shop_16_VespucciChannels = new RAGE.Elements.Ped(0x820B33BD, new Vector3(-1221.407f, -907.9698f, 12.32635f), 33f, dimension);
            RAGE.Elements.Ped ped_shop_17_LittleSeoul = new RAGE.Elements.Ped(0x820B33BD, new Vector3(-706.0983f, -914.5689f, 19.21559f), 94f, dimension);
            RAGE.Elements.Ped ped_shop_18_Muretetaheights = new RAGE.Elements.Ped(0x820B33BD, new Vector3(1134.281f, -983.0289f, 46.41584f), 279f, dimension);
            RAGE.Elements.Ped ped_shop_19_Strawberry = new RAGE.Elements.Ped(0x820B33BD, new Vector3(24.4579f, -1345.584f, 29.49703f), 269f, dimension);
            RAGE.Elements.Ped ped_shop_20_Davis = new RAGE.Elements.Ped(0x820B33BD, new Vector3(-47.33448f, -1758.736f, 29.421f), 53f, dimension);

            #endregion

            #region NPC в магазинах оружия

            RAGE.Elements.Ped ped_shop_AmmuNation_1_PalletBay = new RAGE.Elements.Ped(0x9E08633D, new Vector3(-331.3176f, 6085.444f, 31.45479f), 226.7012f, dimension);
            RAGE.Elements.Ped ped_shop_shop_AmmuNation_2_sandyshores = new RAGE.Elements.Ped(0x9E08633D, new Vector3(1692.702f, 3761.395f, 34.70535f), 229f, dimension);
            RAGE.Elements.Ped ped_shop_AmmuNation_3_ZancudoRiver = new RAGE.Elements.Ped(0x9E08633D, new Vector3(-1118.575f, 2700.184f, 18.55415f), 224.1692f, dimension);
            RAGE.Elements.Ped ped_shop_AmmuNation_4_Сhumash = new RAGE.Elements.Ped(0x9E08633D, new Vector3(-3173.435f, 1088.853f, 20.83874f), 246.4287f, dimension);
            RAGE.Elements.Ped ped_shop_AmmuNation_5_Morningvood = new RAGE.Elements.Ped(0x9E08633D, new Vector3(-1304.233f, -395.2069f, 36.69581f), 76.56145f, dimension);
            RAGE.Elements.Ped ped_shop_AmmuNation_6_Havik = new RAGE.Elements.Ped(0x9E08633D, new Vector3(253.6347f, -51.08869f, 69.94106f), 67.61586f, dimension);
            RAGE.Elements.Ped ped_shop_AmmuNation_7_TataviamMountains = new RAGE.Elements.Ped(0x9E08633D, new Vector3(2567.381f, 292.4883f, 108.7349f), 7.356893f, dimension);
            RAGE.Elements.Ped ped_shop_AmmuNation_8_LittleSeoul = new RAGE.Elements.Ped(0x9E08633D, new Vector3(-661.7179f, -933.4256f, 21.82922f), 178.141f, dimension);
            RAGE.Elements.Ped ped_shop_AmmuNation_9_PilboxHill = new RAGE.Elements.Ped(0x9E08633D, new Vector3(23.11238f, -1105.592f, 29.79702f), 159.9406f, dimension);
            RAGE.Elements.Ped ped_shop_AmmuNation_10_LaMesa = new RAGE.Elements.Ped(0x9E08633D, new Vector3(841.9356f, -1035.309f, 28.19486f), 1.065942f, dimension);
            RAGE.Elements.Ped ped_shop_AmmuNation_11_CypresFlats = new RAGE.Elements.Ped(0x9E08633D, new Vector3(809.6691f, -2159.093f, 29.619f), 357.5533f, dimension);



            #endregion

            #region NPC в магазинах одежды

            RAGE.Elements.Ped ped_shop_ClothesMarket_1_PalletBay = new RAGE.Elements.Ped(0x20C8012F, new Vector3(5.866394f, 6511.474f, 31.87785f), 46.0793f, dimension);
            RAGE.Elements.Ped ped_shop_ClothesMarket_2_Grapeseed = new RAGE.Elements.Ped(0x20C8012F, new Vector3(1695.3f, 4823.086f, 42.06312f), 99.93824f, dimension);
            RAGE.Elements.Ped ped_shop_ClothesMarket_3_ZancudoRiver = new RAGE.Elements.Ped(0x20C8012F, new Vector3(-1102.441f, 2711.602f, 19.10787f), 220.2168f, dimension);
            RAGE.Elements.Ped ped_shop_ClothesMarket_4_Harmony = new RAGE.Elements.Ped(0x20C8012F, new Vector3(612.7493f, 2762.711f, 42.08813f), 270.5039f, dimension);
            RAGE.Elements.Ped ped_shop_ClothesMarket_5_GrandSenoraDesert = new RAGE.Elements.Ped(0x20C8012F, new Vector3(1196.506f, 2711.691f, 38.22263f), 178.1272f, dimension);
            RAGE.Elements.Ped ped_shop_ClothesMarket_6_Сhumash = new RAGE.Elements.Ped(0x20C8012F, new Vector3(-3169.336f, 1043.139f, 20.86322f), 66.13968f, dimension);
            RAGE.Elements.Ped ped_shop_ClothesMarket_7_Morningwood = new RAGE.Elements.Ped(0x20C8012F, new Vector3(-1449.064f, -238.2064f, 49.8134f), 48.22571f, dimension);
            RAGE.Elements.Ped ped_shop_ClothesMarket_8_RockfordHills = new RAGE.Elements.Ped(0x20C8012F, new Vector3(-708.8703f, -151.9174f, 37.41514f), 124.5f, dimension);
            RAGE.Elements.Ped ped_shop_ClothesMarket_9_Berton = new RAGE.Elements.Ped(0x20C8012F, new Vector3(-165.0581f, -303.0531f, 39.73328f), 258.1259F, dimension);
            RAGE.Elements.Ped ped_shop_ClothesMarket_10_Alta = new RAGE.Elements.Ped(0x20C8012F, new Vector3(127.0167f, -224.1678f, 54.55783f), 69.65506f, dimension);
            RAGE.Elements.Ped ped_shop_ClothesMarket_11_DelPerro = new RAGE.Elements.Ped(0x20C8012F, new Vector3(-1193.891f, -766.9327f, 17.3162f), 217.9726f, dimension);
            RAGE.Elements.Ped ped_shop_ClothesMarket_12_ЕextileСity = new RAGE.Elements.Ped(0x20C8012F, new Vector3(427.0428f, -806.171f, 29.49114f), 92.61037f, dimension);
            //RAGE.Elements.Ped ped_shop_ClothesMarket_13_ = new RAGE.Elements.Ped(0x20C8012F, new Vector3(), , 4294967295);
            RAGE.Elements.Ped ped_shop_ClothesMarket_14_VecucciChannels = new RAGE.Elements.Ped(0x20C8012F, new Vector3(-823.1691f, -1072.34f, 11.32811f), 207.6083f, dimension);
            RAGE.Elements.Ped ped_shop_ClothesMarket_15_Strobebury = new RAGE.Elements.Ped(0x20C8012F, new Vector3(73.94381f, -1392.883f, 29.37615f), 264.19f, dimension);

            #endregion
            #region  NPC LSPD
            RAGE.Elements.Ped ped_LSPD_Central = new RAGE.Elements.Ped(0x5E3DA4A4, new Vector3(454.1459f, - 980.019f, 30.6896f), 98f, dimension);
            #endregion

            #region  NPC BUS Driver
             ped_Driver = new RAGE.Elements.Ped(0x5E3DA4A4, new Vector3(163.3035f, -1777.8119f, 29.26619f), 98f, dimension);
            //vehicle = new RAGE.Elements.Vehicle(0xD577C962,new Vector3(398.8798f, -997.656f, 29.45933f),heading: 180f);
            #endregion


        }

        public void GetClothesDLC(object[] args)
        {
            Events.CallRemote("GetClothesFromDLC",(int)args[0],(int)args[1]);
        }

        public void CarsDrives(object[] args)
        {
            vehicle = (RAGE.Elements.Vehicle)args[0];
            int blipIterator = RAGE.Game.Invoker.Invoke<int>(0x186E5D252FA50E7D);
            int FirstInfoId = RAGE.Game.Invoker.Invoke<int>(0x1BEDE233E6CD2A1F, blipIterator);
            int NextInfoId = RAGE.Game.Invoker.Invoke<int>(0x14F96AA50D6FBEA7, blipIterator);
            RAGE.Game.Invoker.Invoke<int>(0xBE9B0959FFD0779B, FirstInfoId);

            for (int i = FirstInfoId; RAGE.Game.Invoker.Invoke<int>(0xA6DB27D19ECBB7DA, i) != 0; i = NextInfoId)
            {
                if (RAGE.Game.Invoker.Invoke<int>(0xBE9B0959FFD0779B, i) == 4)
                {
                    Vector3 coord = RAGE.Game.Ui.GetBlipInfoIdCoord(i);
                    float a = 1000.0f;
                    RAGE.Game.Misc.GetGroundZFor3dCoord(coord.X, coord.Y, a, ref a, false);

                    coord.Z = a + 2;

                    ped_Driver.TaskVehicleDriveToCoord(vehicle.Handle, coord.X, coord.Y, coord.Z, 10f, 1, 0xD577C962, 16777216, 3f, 1f);

                    return;
                }
            }

           

        }


        public void CarDrive(object[] args)
        {
            vehicle = (RAGE.Elements.Vehicle)args[0];
           
            ped_Driver.FreezePosition(false);
            ped_Driver.ActivatePhysics();
            ped_Driver.TaskEnterVehicle(vehicle.Handle,0,-1,1f,0,0);
            int blipIterator = RAGE.Game.Invoker.Invoke<int>(0x186E5D252FA50E7D);
            int FirstInfoId = RAGE.Game.Invoker.Invoke<int>(0x1BEDE233E6CD2A1F, blipIterator);
            int NextInfoId = RAGE.Game.Invoker.Invoke<int>(0x14F96AA50D6FBEA7, blipIterator);
            RAGE.Game.Invoker.Invoke<int>(0xBE9B0959FFD0779B, FirstInfoId);
           

            for (int i = FirstInfoId; RAGE.Game.Invoker.Invoke<int>(0xA6DB27D19ECBB7DA, i) != 0; i = NextInfoId)
            {
                if (RAGE.Game.Invoker.Invoke<int>(0xBE9B0959FFD0779B, i) == 4)
                {
                    coord = RAGE.Game.Ui.GetBlipInfoIdCoord(i);
                    float a = 1000.0f;
                    RAGE.Game.Misc.GetGroundZFor3dCoord(coord.X, coord.Y, a, ref a, false);

                    coord.Z = a + 2;
                    //ped_Driver.TaskVehicleFollowWaypointRecording

                    //   ped_Driver.TaskVehicleDriveToCoord(vehicle.Handle, coord.X, coord.Y,coord.Z, 10f, 1, 0xD577C962, 16777216, 3f, 1f);
                    //DrawInfo.Drive = true;
                    
                    RAGE.Game.Vehicle.CreateMissionTrain(15,coord.X, coord.Y, coord.Z, true);
                    return;
                }
            }
        }

        public void GetObject(object[] args)
        {
         int re = RAGE.Game.Object.GetClosestObjectOfType(RAGE.Elements.Player.LocalPlayer.Position.X, RAGE.Elements.Player.LocalPlayer.Position.Y, RAGE.Elements.Player.LocalPlayer.Position.Z, 2f, 1805980844, false, false, false);
            if(re != 0 )
            {
             
            }
        }



        public void BigData(object[] args)
        {
            string a = args[0].ToString();
            Chat.Output(RAGE.Game.Clock.GetClockMinutes().ToString() +" " + a.Length.ToString());
            
        }

        public void InJail(object[] args)
        {
            KeyManager.block = 99;
        }
        public void InJailFalse(object[] args)
        {
            KeyManager.block = 0;
        }

        private void onTestEvent(object[] args)
        {
            int blipIterator = RAGE.Game.Invoker.Invoke<int>(0x186E5D252FA50E7D);
            int FirstInfoId = RAGE.Game.Invoker.Invoke<int>(0x1BEDE233E6CD2A1F, blipIterator);
            int NextInfoId = RAGE.Game.Invoker.Invoke<int>(0x14F96AA50D6FBEA7, blipIterator);
            RAGE.Game.Invoker.Invoke<int>(0xBE9B0959FFD0779B, FirstInfoId);

            for (int i = FirstInfoId; RAGE.Game.Invoker.Invoke<int>(0xA6DB27D19ECBB7DA, i) != 0; i = NextInfoId)
            {
                if (RAGE.Game.Invoker.Invoke<int>(0xBE9B0959FFD0779B, i) == 4)
                {
                    Vector3 coord = RAGE.Game.Ui.GetBlipInfoIdCoord(i);
                    float a = 1000.0f;
                    RAGE.Game.Misc.GetGroundZFor3dCoord(coord.X, coord.Y, a, ref a, false);
                    
                        coord.Z = a+2;
                    
                    Chat.Output(coord.X.ToString() + " " + coord.Y.ToString() + " " + coord.Z.ToString()) ;
                     RAGE.Elements.Player.LocalPlayer.Position = coord;
                    return;
                }
            }
        }

        public void OnPlayerStartEnterVehicle(RAGE.Elements.Vehicle vehicle, int seatId, RAGE.Events.CancelEventArgs cancel)
        {
            //KeyManager.block = 99;
            //long tick = TickEvent.tickcount;
            //Task.Factory.StartNew(() =>
            //{
            //    while (TickEvent.tickcount - tick < 1000)
            //    {
            //        KeyManager.block = 0;
            //    }
            //});


        }




        public void OnPlayerWeaponShot(RAGE.Vector3 targetPos, RAGE.Elements.Player target, RAGE.Events.CancelEventArgs cancel)
        {
            Events.CallRemote("Shot");
        }

        public void Time(object[] args)
        {



            try
            {

                RAGE.Game.Clock.SetClockTime((int)args[0], (int)args[1], 0);
                if(Menu.hudCef!=null)
                Menu.hudCef.ExecuteJs("initTime(" + (int)args[0] + "," + (int)args[1] + ");");
            }
            catch
            {

            }

          


        }



            public void GetWeapon(object[] args)
        {
            uint[] weapons =   JsonConvert.DeserializeObject<uint[]>(args[0].ToString());
            int[] count = new int[weapons.Length];
            int i = 0;
            foreach (uint weapon in weapons )
            {
                count[i] =  RAGE.Elements.Player.LocalPlayer.GetAmmoInWeapon(Convert.ToUInt32(weapon));
                i++;
            }


            Events.CallRemote("setweapon",   args[0], JsonConvert.SerializeObject(count));
            
        }
        public void GameInactive(object[] args)
        {

           

        }
        public void GameActive(object[] args)
        {


        }
        public static void Finger()
        {

           
        }
        public  void SetFriends(object[] args)
        {
            Friends = JsonConvert.DeserializeObject<List<Friends>>(args[0].ToString());
            if (args[0] != null)
            {
                Phone.AddFriendInContacts(args);

            }
        }

        public void Loaded(object[] args)
        {
            player.VoiceChat.RemoveAllVoiceNOW();
            player.VoiceChat.loaded = true;
     
         //   RAGE.Game.Misc.DisableAutomaticRespawn(false);
        }
        public void RemoveAmmo(object[] args)
        {
          //  RAGE.Elements.Player.LocalPlayer.GiveWeaponTo((uint)args[0], (int)args[1],false,true);

           // RAGE.Elements.Player.LocalPlayer.SetCurrentWeapon((uint)args[0], true);


            RAGE.Elements.Player.LocalPlayer.SetAmmoToDrop(RAGE.Elements.Player.LocalPlayer.GetAmmoInWeapon((uint)args[0]));
        //    RAGE.Elements.Player.LocalPlayer.SetAmmo((uint)args[0], (int)args[1], 0);


        }



        public void GetWeaponInv(object[] args)
        {
            uint[] weapons = JsonConvert.DeserializeObject<uint[]>(args[0].ToString());
            int[] count = new int[weapons.Length];
            int i = 0;
            foreach (uint weapon in weapons)
            {
                count[i] = RAGE.Elements.Player.LocalPlayer.GetAmmoInWeapon(Convert.ToUInt32(weapon));
                i++;
            }


            Events.CallRemote("setweaponinv", args[0], JsonConvert.SerializeObject(count));

        }

        public void SoundNotification(object[] args)
        {
            RAGE.Game.Audio.PlaySoundFrontend(1, "CHALLENGE_UNLOCKED", "HUD_AWARDS", true);
        }


        public void Fly(object[] args)
        {
     
            var camPos = new Vector3(
               RAGE.Elements.Player.LocalPlayer.Position.X,
               RAGE.Elements.Player.LocalPlayer.Position.Y,
               RAGE.Elements.Player.LocalPlayer.Position.Z
            );
            var camRot = RAGE.Game.Cam.GetGameplayCamRot(2);
            //RAGE.Elements.Player.LocalPlayer = 
              int camera = RAGE.Game.Cam.CreateCameraWithParams(RAGE.Game.Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), camPos.X, camPos.Y, camPos.Z, camRot.X, camRot.Y, camRot.Z, 70.0f, true, 2);// ("default", camPos, camRot, 45);

            RAGE.Game.Cam.SetCamActive(camera, true);
            RAGE.Game.Cam.RenderScriptCams(true, false, 0, true, false, 0);


        
           RAGE.Elements.Player.LocalPlayer.FreezePosition(true);
            RAGE.Elements.Player.LocalPlayer.SetInvincible(true);
            RAGE.Elements.Player.LocalPlayer.SetVisible(false, false);
            RAGE.Elements.Player.LocalPlayer.SetCollision(false, false);

            KeyManager.block = 99;

        }
        public void StopFly(object[] args)
        {
            RAGE.Elements.Player.LocalPlayer.FreezePosition(false);
            RAGE.Elements.Player.LocalPlayer.SetInvincible(false);
            KeyManager.block = 0;





        }
        public void OnPlayerEnterVehicle(RAGE.Elements.Vehicle vehicle, int seatId)
        {

            player.Skills.StaminaTimeSet(true);
          //  if(seatId == -1)
         //  Speed.fuelLevel = (float)RAGE.Elements.Player.LocalPlayer.Vehicle.GetSharedData("FuelLevel");

            //   Speed.Speedometr(null);

            Player_In_Car = true;
            KeyManager.block = 1;
            Events.CallRemote("stopAnimation");

        }
        public void OnPlayerLeaveVehicle()
        {
            player.Skills.Stop = false;
            Player_In_Car = false;
            //Speed.fuelLevel = 0;
            Events.CallRemote("stopAnimation");
            KeyManager.block = 0;
        }




        /// <summary>
        /// Вызов окна логина
        /// </summary>
        /// <param name="args"></param>
        public void LoginCefCreate(object[] args)
        {
            Browser.CreateBrowserEvent(new object[] { "package://auth/assets/registerLogin.html" });


        }
        public void cuffFreeze(object[] args)
        {
          //  RAGE.Elements.Player.LocalPlayer.FreezePosition(true);
            KeyManager.block = 99;
            block = true;
        }
        public void cuffFreezeStop(object[] args)
        {
          //  RAGE.Elements.Player.LocalPlayer.FreezePosition(false);
            KeyManager.block = 0;
            block = false;
            if (PlayerState.block)
            {
                for (int i = 0; i < 33; i++)
                {
                  //  RAGE.Game.Pad.DisableAllControlActions(i);
                    RAGE.Game.Pad.EnableAllControlActions(i);
                }
            }
            //      int a = RAGE.Game.Invoker.Invoke<int>( RAGE.Game.Natives.GetPedTextureVariation, RAGE.Game.Invoker.Invoke<RAGE.Elements.Ped>(RAGE.Game.Natives.PlayerPedId), 4);
            // int a = RAGE.Game.Invoker.Invoke<int>(RAGE.Game.Natives.GetNumberOfPedTextureVariations, 0, 7);
            //    Chat.Output(a.ToString());
        }



        public void Freeze(object[] args)
        {
            RAGE.Elements.Player.LocalPlayer.FreezePosition(true);
            KeyManager.block = 99;
        }
        public void FreezeStop(object[] args)
        {
            RAGE.Elements.Player.LocalPlayer.FreezePosition(false);
            KeyManager.block = 0;
      //      int a = RAGE.Game.Invoker.Invoke<int>( RAGE.Game.Natives.GetPedTextureVariation, RAGE.Game.Invoker.Invoke<RAGE.Elements.Ped>(RAGE.Game.Natives.PlayerPedId), 4);
           // int a = RAGE.Game.Invoker.Invoke<int>(RAGE.Game.Natives.GetNumberOfPedTextureVariations, 0, 7);
        //    Chat.Output(a.ToString());
        }
        public void RegisterLogin(object[] args)
        {
            if(args[1].ToString().Length >= 3 && args[1].ToString().Length >= 5)
            {
               if(args.Length == 4)
                Events.CallRemote("registerLogin.server", args[0], args[1], args[2], args[3]);
                else
                {
                    Events.CallRemote("registerLogin.server", args[0], args[1], args[2], "");
                }
            }
            else
            {
                // вставить оповещение о недостачном количестве символов
            }
        }


        public void LoginDestroy(object[] args)
        {
            Browser.DestroyBrowserEvent(null);
           
            
            //CreateCefForGame();
            //BindKeys();
            //blockKeys = false
            //bindKeys(blockKeys);
            Menu.CreateAllMenu();
            Events.CallRemote("CEFCreatedSuccess");
            KeyManager.block = 0;
            // Events.CallRemote("CEFCreatedSuccess");
        }

        public void RegisterDestroy(object[] args)
        {
            Browser.DestroyBrowserEvent(null);
         //   Menu.CreateAllMenu();
            //CreateCefForGame();
            //BindKeys();

            //blockKeys = false;
            //bindKeys(blockKeys);
         //   Events.CallRemote("CEFCreatedSuccess");
           

        }




    }
}
