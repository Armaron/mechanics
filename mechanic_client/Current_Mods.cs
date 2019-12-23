using System;
using System.Collections.Generic;
using System.Text;

namespace mechanic_client
{
    public class Current_Mods
    {
        public int id { get; set; }
        public int count { get; set; }

        public int[] price { get; set; }
        public Current_Mods()
        {

        }

        public Current_Mods(int id, int count, int[] price)
        {
            this.id = id;
            this.count = count;
            this.price = price;
        }
    }
}
