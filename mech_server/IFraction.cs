using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serv_RP.Fraction
{
    interface IFraction
    {
        /// <summary>
        ///  Вступить во фракцию
        /// </summary>
        /// <param name="client"></param>
        void AddToFraction(Client client);
        /// <summary>
        /// Убрать из фракции
        /// </summary>
        /// <param name="client"></param>
        void RemoveFromFraction(Client client);
        /// <summary>
        ///  Открыть ноутбук
        /// </summary>
        void OpenNoteBook(Client client);
        /// <summary>
        ///  Загрузить всю информацию с БД
        /// </summary>
        void LoadAllInfo();
        /// <summary>
        /// Установить новый ранк пользователю
        /// </summary>
        /// <param name="client"></param>
        void SetNewRank(Client client);
        /// <summary>
        /// Открыть гардероб
        /// </summary>
        /// <param name="client"></param>
        void OpenWardrobe(Client client);
       
    }
}
