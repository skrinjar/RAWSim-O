using RAWSimO.Core.Configurations;
using RAWSimO.Core.Items;
using RAWSimO.Core.Elements;
using RAWSimO.Core.Geometrics;
using RAWSimO.Core.Metrics;
using RAWSimO.Toolbox;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RAWSimO.Core.Control.Defaults.TaskAllocation
{
    public class DummyBotManager : BotManager
    {
        public double RellocationInterval{ get; set; }

        public DummyBotManager(Instance instance, double reallocation_interval = 30.0) : base(instance)
        {
            Instance = instance;
            RellocationInterval = reallocation_interval;
        }
        /// <summary>
        /// The next event when this element has to be updated.
        /// </summary>
        /// <param name="currentTime">The current time of the simulation.</param>
        /// <returns>The next time this element has to be updated.</returns>
        public override double GetNextEventTime(double currentTime)
        {
           return RellocationInterval + currentTime; //maybe double.MaxValue ??
        }
        /// <summary>
        /// Signals the current time to the mechanism. The mechanism can decide to block the simulation thread in order consume remaining real-time.
        /// </summary>
        /// <param name="currentTime">The current simulation time.</param>
        public override void SignalCurrentTime(double currentTime)
        {
           /* Not necessary */
        }
        /// <summary>
        /// Updates the element to the specified time.
        /// </summary>
        /// <param name="lastTime">The time before the update.</param>
        /// <param name="currentTime">The time to update to.</param>
        public override void Update(double lastTime, double currentTime)
        {
            /*Do nothing, when bot finishes his task it will triget call chain that will lead
            to GetNextTask()*/
        }
        /// <summary>
        /// Gets the next task for the specified bot.
        /// </summary>
        /// <param name="bot">The bot to get a task for.</param>
        protected override void GetNextTask(Bot bot)
        {
            MovableStation station = null;
            if(bot is MovableStation){
                station = bot as MovableStation;
                DummyOrder order = station.AssignedOrders.FirstOrDefault() as DummyOrder;
                if(order != null)
                {
                    EnqueueMultiPointGather(station, order);
                }   
            }else{
                return;
            }

        }
    }
}