using RAWSimO.Core.Info;
using RAWSimO.Core.Management;
using System.Collections.Generic;
using System.Linq;

namespace RAWSimO.Core.Items
{
    public class DummyOrder : Order
    {
        public DummyOrder()
        {
            Locations = new List<int>();
            Type = ItemType.LocationsList;
            Completed = false;
            TimeStamp = 0;//every order is automatically placed
            DueTime = double.MaxValue;//complete whenever
        }
        public DummyOrder(List<int> list)
        {
            Locations = list;
            Type = ItemType.LocationsList;
            Completed = false;
            TimeStamp = 0;
            DueTime = double.MaxValue;
        }

        public bool Completed{ get; set; }
        public override bool IsCompleted(){ return Completed; }
        public List<int> Locations{get; set;}

        public ItemType Type{get; set;}
    }

}