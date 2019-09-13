using RAWSimO.Core.Control;
using RAWSimO.Core.Bots;
using RAWSimO.Core.Geometrics;
using RAWSimO.Core.Info;
using RAWSimO.Core.Interfaces;
using RAWSimO.Core.Items;
using RAWSimO.Core.Management;
using RAWSimO.Core.Statistics;
using RAWSimO.Core.Waypoints;
using RAWSimO.Toolbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RAWSimO.Core.Elements
{
    ///<summary>
    ///Class that represents movable stations  
    ///</summary>
    public class MovableStation : BotNormal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BotNormal"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="acceleration">The maximum acceleration.</param>
        /// <param name="deceleration">The maximum deceleration.</param>
        /// <param name="maxVelocity">The maximum velocity.</param>
        /// <param name="turnSpeed">The turn speed.</param>
        /// <param name="collisionPenaltyTime">The collision penalty time.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public MovableStation(int id, Instance instance, double radius, double acceleration, double deceleration, double maxVelocity, double turnSpeed, double collisionPenaltyTime, double x = 0.0, double y = 0.0) 
        : base(id, instance, radius, 99999, acceleration, deceleration, maxVelocity, turnSpeed, collisionPenaltyTime, x, y)
        {
            StationPart = new OutputStationAux(instance);
            StationPart.movableStationPart = this;
        }
        ///<summary>
        ///implicitly cast MovableStation object to OutputStation object for compatibility with the rest of the system
        ///</summary>
        public static implicit operator OutputStation(MovableStation ms)
        {
            return ms.StationPart;
        }

        public OutputStationAux StationPart { get; set; }

        #region OutputStation members
        /// <summary>
        /// Indicates whether this station is active
        /// </summary>
        public bool Active {get => StationPart.Active; } 
        /// <summary>
        /// Activates this station.
        /// </summary>
        public void Activate()
        {
            StationPart.Activate();
        }
        /// <summary>
        /// Deactivates this station.
        /// </summary>
        public void Deactivate()
        {
            StationPart.Deactivate();
        }
        /// <summary>
        /// The capacity currently in use at this station.
        /// </summary>
        public int CapacityInUse { get => StationPart.CapacityInUse; }
        /// <summary>
        /// The amount of capacity reserved by a controller.
        /// </summary>
        internal int CapacityReserverd { get => StationPart.CapacityReserved; }
        /// <summary>
        /// The orders currently assigned to this station.
        /// </summary>
        public IEnumerable<Order> AssignedOrders { get => StationPart.AssignedOrders; }
        /// <summary>
        /// The orders currently queued to this station.
        /// </summary>
        public IEnumerable<Order> QueuedOrders { get => StationPart.QueuedOrders; } 
        /// <summary>
        /// Checks whether the specified order can be added for reservation to this station.
        /// </summary>
        /// <param name="order">The order that has to be checked.</param>
        /// <returns><code>true</code> if the bundle fits, <code>false</code> otherwise.</returns>
        public bool FitsForReservation(Order order) { return StationPart.FitsForReservation(order) ;}
         /// <summary>
        /// Reserves capacity of this station for the given order. The reserved capacity will be maintained when the order is allocated.
        /// </summary>
        /// <param name="order">The order for which capacity shall be reserved.</param>
        internal void RegisterOrder(Order order){ StationPart.RegisterOrder(order); }
        /// <summary>
        /// The order to queue in for this station.
        /// </summary>
        /// <param name="order">The order to queue in.</param>
        internal void QueueOrder(Order order){ StationPart.QueueOrder(order); }
        /// <summary>
        /// Assigns a new order to this station.
        /// </summary>
        /// <param name="order">The order to assign to this station.</param>
        /// <returns><code>true</code> if the order was successfully assigned, <code>false</code> otherwise.</returns>
        public bool AssignOrder(Order order){ return StationPart.AssignOrder(order); }
        /// <summary>
        /// Requests the station to pick the given item for the given order.
        /// </summary>
        /// <param name="bot">The bot that requests the pick.</param>
        /// <param name="request">The request to handle.</param>
        public void RequestItemTake(Bot bot, ExtractRequest request){ StationPart.RequestItemTake(bot, request); }
        /// <summary>
        /// Marks a pod as inbound for a station.
        /// </summary>
        /// <param name="pod">The pod that being brought to the station.</param>
        internal void RegisterInboundPod(Pod pod){ StationPart.RegisterInboundPod(pod); }
        /// <summary>
        /// Removes a pod from the list of inbound pods.
        /// </summary>
        /// <param name="pod">The pod that is not inbound anymore.</param>
        internal void UnregisterInboundPod(Pod pod){ StationPart.UnregisterInboundPod(pod); }
        /// <summary>
        /// All pods currently approaching the station.
        /// </summary>
        internal IEnumerable<Pod> InboundPods { get => StationPart.InboundPods; }
        /// <summary>
        /// Register an extract task with this station.
        /// </summary>
        /// <param name="task">The task that shall be done at this station.</param>
        internal void RegisterExtractTask(ExtractTask task){ StationPart.RegisterExtractTask(task); }
    	/// <summary>
        /// Unregister an extract task with this station.
        /// </summary>
        /// <param name="task">The task that was done or cancelled for this station.</param>
        internal void UnregisterExtractTask(ExtractTask task){ StationPart.UnregisterExtractTask(task); } 
        /// <summary>
        /// Register a newly approached bot before picking begins for statistical purposes.
        /// </summary>
        /// <param name="bot">The bot that just approached the station.</param>
        public void RegisterBot(Bot bot){ StationPart.RegisterBot(bot); }
        /// <summary>
        /// The number of requests currently open (not assigned to a bot) for this station.
        /// </summary>
        internal int StatCurrentlyOpenRequests { get => StationPart.StatCurrentlyOpenRequests ; set => StationPart.StatCurrentlyOpenRequests = value;}
        /// <summary>
        /// The number of requests currently open (not assigned to a bot) for this station.
        /// </summary>
        internal int StatCurrentlyOpenQueuedRequests { get => StationPart.StatCurrentlyOpenQueuedRequests ; set => StationPart.StatCurrentlyOpenQueuedRequests = value;}
        /// <summary>
        /// The number of items currently open (not picked yet) for this station.
        /// </summary>
        internal int StatCurrentlyOpenItems { get => StationPart.StatCurrentlyOpenItems; }
        /// <summary>
        /// The number of items currently open (not picked yet) and queued for this station.
        /// </summary>
        internal int StatCurrentlyOpenQueuedItems { get => StationPart.StatCurrentlyOpenQueuedItems; }
        /// <summary>
        /// The (sequential) number of pods handled at this station.
        /// </summary>
        public int StatPodsHandled { get => StationPart.StatPodsHandled; }
        /// <summary>
        /// The time it took to handle one pod in average.
        /// </summary>
        public double  StatPodHandlingTimeAvg { get => StationPart.StatPodHandlingTimeAvg; }
        /// <summary>
        /// The variance in the handling times of the pods.
        /// </summary>
        public double StatPodHandlingTimeVar { get =>StationPart.StatPodHandlingTimeVar; }
        /// <summary>
        /// The minimal handling time of a pod.
        /// </summary>
        public double StatPodHandlingTimeMin { get =>StationPart.StatPodHandlingTimeMin; }
        /// <summary>
        /// The maximal handling time of a pod.
        /// </summary>
        public double StatPodHandlingTimeMax { get =>StationPart.StatPodHandlingTimeMax; }
        /// <summary>
        /// The item pile-on of this station, i.e. the relative number of items picked from the same pod in one 'transaction'.
        /// </summary>
        public double StatItemPileOn { get =>StationPart.StatItemPileOn; }
        /// <summary>
        /// The injected item pile-on of this station, i.e. the relative number of injected items picked from the same pod in one 'transaction'.
        /// </summary>
        public double StatInjectedItemPileOn { get =>StationPart.StatInjectedItemPileOn; }
        /// <summary>
        /// The order pile-on of this station, i.e. the relative number of orders finished from the same pod in one 'transaction'.
        /// </summary>
        public double StatOrderPileOn { get =>StationPart.StatOrderPileOn; }
        /// <summary>
        /// The time this station was active.
        /// </summary>
        public double StatActiveTime { get => StationPart.StatActiveTime; }
        /// <summary>
        /// Returns a simple string identifying this object in its instance.
        /// </summary>
        /// <returns>A simple name identifying the instance element.</returns>
        public override string GetIdentfierString() { return "MovableStation" + this.ID; }
        /// <summary>
        /// Returns a simple string giving information about the object.
        /// </summary>
        /// <returns>A simple string.</returns>
        public override string ToString() { return "MovableStation" + this.ID; }
        /// <summary>
        /// Gets the number of assigned orders.
        /// </summary>
        /// <returns>The number of assigned orders.</returns>
        public int GetInfoAssignedOrders(){ return StationPart.GetInfoAssignedOrders(); }
         /// <summary>
        /// Gets all order currently open.
        /// </summary>
        /// <returns>The enumeration of open orders.</returns>
        public IEnumerable<IOrderInfo> GetInfoOpenOrders(){ return StationPart.GetInfoOpenOrders(); } 
         /// <summary>
        /// Gets all orders already completed.
        /// </summary>
        /// <returns>The enumeration of completed orders.</returns>
        public IEnumerable<IOrderInfo> GetInfoCompletedOrders() { return StationPart.GetInfoCompletedOrders(); }
        /// <summary>
        /// Gets the capacity this station offers.
        /// </summary>
        /// <returns>The capacity of the station.</returns>
        public double GetInfoCapacity() { return StationPart.GetInfoCapacity(); }
        /// <summary>
        /// Gets the absolute capacity currently in use.
        /// </summary>
        /// <returns>The capacity in use.</returns>
        public double GetInfoCapacityUsed() { return StationPart.GetInfoCapacityUsed(); }
         /// <summary>
        /// Indicates the number that determines the overall sequence in which stations get activated.
        /// </summary>
        /// <returns>The order ID of the station.</returns>
        public int GetInfoActivationOrderID() { return StationPart.GetInfoActivationOrderID(); }
        /// <summary>
        /// Gets the information queue.
        /// </summary>
        /// <returns>Queue</returns>
        public string GetInfoQueue() { return StationPart.GetInfoQueue(); }
        /// <summary>
        /// Indicates whether the station is currently activated (available for new assignments).
        /// </summary>
        /// <returns><code>true</code> if the station is active, <code>false</code> otherwise.</returns>
        public bool GetInfoActive(){ return StationPart.GetInfoActive(); }
        /// <summary>
        /// Gets the of requests currently open (not assigned to a bot) for this station.
        /// </summary>
        /// <returns>The number of active requests.</returns>
        public int GetInfoOpenRequests(){ return StationPart.GetInfoOpenRequests(); }
        /// <summary>
        /// Gets the number of queued requests currently open (not assigned to a bot) for this station.
        /// </summary>
        /// <returns>The number of active queued requests.</returns>
        public int GetInfoOpenQueuedRequests(){ return StationPart.GetInfoOpenQueuedRequests(); }
        /// <summary>
        /// Gets the number of currently open items (not yet picked) for this station.
        /// </summary>
        /// <returns>The number of open items.</returns>
        public int GetInfoOpenItems(){ return StationPart.GetInfoOpenItems(); }
        /// <summary>
        /// Gets the number of currently queued and open items (not yet picked) for this station.
        /// </summary>
        /// <returns>The number of queued open items.</returns>
        public int GetInfoOpenQueuedItems(){ return StationPart.GetInfoOpenQueuedItems(); }
        /// <summary>
        /// Gets the number of pods currently incoming to this station.
        /// </summary>
        /// <returns>The number of pods currently incoming to this station.</returns>
        public int GetInfoInboundPods(){ return StationPart.GetInfoInboundPods(); }
        /// <summary>
        /// The Queue starting with the nearest way point ending with the most far away one.
        /// </summary>
        /// <value>
        /// The queue.
        /// </value>
        public Dictionary<Waypoint, List<Waypoint>> Queues { get => StationPart.Queues; set => StationPart.Queues = value; }
        /// <summary>
        /// Completes an order that is ready, if there is one.
        /// </summary>
        /// <param name="currentTime">The current simulation time.</param>
        /// <returns>The completed order if there was one, <code>null</code> otherwise.</returns>
        public Order RemoveAnyCompletedOrder(double currentTime)
        {
            // Remove any orders that are finished
            Order finishedOrder = null;
            foreach (Order order in StationPart.AssignedOrders)
                if ((order is DummyOrder &&
                    (order as DummyOrder).Locations.Count != 0 &&
                    CurrentWaypoint == Instance.Waypoints[(order as DummyOrder).Locations.LastOrDefault()]) 
                    ||
                    (!(order is DummyOrder) && order.IsCompleted()))
                {
                    finishedOrder = order;
                    StatNumOrdersFinished++;
                    // Notify the item manager about this
                    Instance.ItemManager.CompleteOrder(finishedOrder);
                    // Notify completed order
                    Instance.NotifyOrderCompleted(finishedOrder, this);
                    // Break early and block action
                    BlockedUntil = currentTime + OrderCompletionTime;
                    break;
                }
            return finishedOrder;
        }
        public double ItemTransferTime { get => StationPart.ItemTransferTime; set => StationPart.ItemTransferTime = value; }
        public double ItemPickTime { get => StationPart.ItemPickTime; set => StationPart.ItemPickTime = value; }
        public double OrderCompletionTime { get => StationPart.OrderCompletionTime; set => StationPart.OrderCompletionTime = value; }
        public Waypoint Waypoint { get => StationPart.Waypoint; set => StationPart.Waypoint = value; }
        public int ActivationOrderID { get => StationPart.ActivationOrderID; set => StationPart.ActivationOrderID = value; }
        public int Capacity { get => StationPart.Capacity; set => StationPart.Capacity = value; }
        public int StatNumItemsPicked { get => StationPart.StatNumItemsPicked; set => StationPart.StatNumItemsPicked = value; }
        public int StatNumInjectedItemsPicked{ get => StationPart.StatNumInjectedItemsPicked; set => StationPart.StatNumInjectedItemsPicked = value; }
        public int StatNumOrdersFinished{ get => StationPart.StatNumOrdersFinished; set => StationPart.StatNumOrdersFinished = value; }
        public double StatIdleTime{ get => StationPart.StatIdleTime; set => StationPart.StatIdleTime = value; }
        public double StatDownTime{ get => StationPart.StatDownTime; set => StationPart.StatDownTime = value; }

        #endregion
        #region IUpdateable methods
        /// <summary>
        /// The next event when this element has to be updated.
        /// </summary>
        /// <param name="currentTime">The current time of the simulation.</param>
        /// <returns>The next time this element has to be updated.</returns>
        public override double GetNextEventTime(double currentTime)
        {
            var botTime = base.GetNextEventTime(currentTime);
            var stationTIme = StationPart.GetNextEventTime(currentTime);

            return Math.Min(botTime,stationTIme);
        }
        /// <summary>
        /// Updates the element to the specified time.
        /// </summary>
        /// <param name="lastTime">The time before the update.</param>
        /// <param name="currentTime">The time to update to.</param>
        public override void Update(double lastTime, double currentTime)
        {
            //first do everything a station has to do
            StationPart.Update(lastTime, currentTime);
            //then do what robot base hase to
            base.Update(lastTime, currentTime);
        }

        #endregion

        #region MovableStationSpecifics

        

        #endregion
    }

}
