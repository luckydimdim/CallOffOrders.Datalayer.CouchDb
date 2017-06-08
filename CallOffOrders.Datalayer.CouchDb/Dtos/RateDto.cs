using System;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Dtos
{
    public class RateDto
    {
        public string Id;

        public String Name;

        public bool IsRate;

        public string ParentId;

        /// <summary>
        /// Ставка
        /// </summary>
        public double Amount;
        
        /// <summary>
        /// Ед. изм.
        /// </summary>
        public int? Unit;

    }
}
