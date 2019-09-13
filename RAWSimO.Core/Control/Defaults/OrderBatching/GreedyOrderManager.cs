using RAWSimO.Core.Configurations;
using RAWSimO.Core.Elements;
using RAWSimO.Core.IO;
using RAWSimO.Core.Items;
using RAWSimO.Core.Metrics;
using RAWSimO.Toolbox;
using System;
using System.Linq;
using System.Collections.Generic;


namespace RAWSimO.Core.Control.Defaults.OrderBatching
{

    /// <summary>
    /// Implements a manager that assigns first available order to first available station
    /// </summary>
    public class GreedyOrderManager : OrderManager
    {
        public GreedyOrderManager(Instance instance) : base(instance){}
        public override void SignalCurrentTime(double currentTime)
        {
          /* Ignore since this simple manager is always ready. */
        }
        /// <summary>
        /// Method that assigns pending orders to stations if any station is available
        /// </summary>
        protected override void DecideAboutPendingOrders()
        {  
            //get all stations which are currently not doing anything
            List<MovableStation> availableStations = Instance.MovableStations.Where(s => s.CapacityInUse == 0).ToList();
            int pendingOrdersCount = _pendingOrders.Count;
            //assign pending orders to stations respectively
            for (int i = 0; i < Math.Min(availableStations.Count, pendingOrdersCount); i++)
            {
                //assign first pending order, after AllocateOrder() _pendingOrders.ElementAt(0) will be removed from it
                AllocateOrder(_pendingOrders.ElementAt(0), availableStations[i]);
            }           
        }
    }

}