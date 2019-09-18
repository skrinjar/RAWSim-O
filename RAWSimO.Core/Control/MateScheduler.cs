using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAWSimO.Core.Elements;
using RAWSimO.Core.Interfaces;
using RAWSimO.Core.Waypoints;

namespace RAWSimO.Core.Control
{
    /// <summary>
    /// class representing a mate scheduler
    /// </summary>
    public class MateScheduler: IUpdateable
    {
        #region core
        /// <summary>
        /// creates new mate scheduler
        /// </summary>
        /// <param name="instance">current instance</param>
        public MateScheduler(Instance instance)
        {
            Instance = instance;
            MateBots = Instance.MateBots;
            MovableStations = Instance.MovableStations;
            AvailableMates = new List<MateBot>(MateBots);
        }
        /// <summary>
        /// current instance
        /// </summary>
        private Instance Instance { get; set; }
        /// <summary>
        /// List of all MateBots that exist in this instance
        /// </summary>
        private List<MateBot> MateBots { get; set; }
        /// <summary>
        /// List of all currently available <see cref="MateBot"> objects  
        /// </summary>
        private List<MateBot> AvailableMates { get; set; }
        /// <summary>
        /// List of all <see cref="MovableStation"/> objects in this instance
        /// </summary>
        private List<MovableStation> MovableStations { get; set; }
        /// <summary>
        /// queue of all the requested assistances ordered by placement time
        /// </summary>
        private Queue<Waypoint> requestedAssistanceLocations = new Queue<Waypoint>();
        /// <summary>
        /// Container which maps Bot to a Waypoint on which he needs the assistance
        /// </summary>
        private Dictionary<Bot, Waypoint> assistanceLocation = new Dictionary<Bot, Waypoint>();
        /// <summary>
        /// assignes <see cref="MateBot"/> to assist <see cref="Bot"/> on a given <see cref="Waypoint"/>
        /// </summary>
        /// <param name="bot">Bot which needs assistance</param>
        /// <param name="destinationWaypoint">Waypoint on which assistance will be needed</param>
        public void RequestAssistance(Bot bot, Waypoint destinationWaypoint)
        {
            requestedAssistanceLocations.Enqueue(destinationWaypoint);
            assistanceLocation.Add(bot, destinationWaypoint);
        }
        #endregion

        #region IUpdateable members
        /// <summary>
        /// The next event when this element has to be updated.
        /// </summary>
        /// <param name="currentTime">The current time of the simulation.</param>
        /// <returns>The next time this element has to be updated.</returns>
        public double GetNextEventTime(double currentTime) { return double.PositiveInfinity; }
        /// <summary>
        /// Updates the element to the specified time.
        /// </summary>
        /// <param name="lastTime">The time before the update.</param>
        /// <param name="currentTime">The time to update to.</param>
        public void Update(double lastTime, double currentTime)
        {
            //go through all MateBots and see if any of them is idle an can thus be put in AvailableMates list
            foreach (var bot in MateBots)
                if(bot.CurrentTask.Type == BotTaskType.None)
                    AvailableMates.Add(bot);
            //check if any assistance is needed and if any assistance can be given
            if (requestedAssistanceLocations.Count > 0 && AvailableMates.Count > 0)
            {
                //get first available mate
                var mate = AvailableMates.First();
                AvailableMates.RemoveAt(0);
                //get first requested location
                var location = requestedAssistanceLocations.Dequeue();
                //get first bot in dict that requested an assistance at the location
                var bot = assistanceLocation.First(b => b.Value == location).Key;
                assistanceLocation.Remove(bot); //remove (bot,location) pair
                //create new task
                AssistTask task = new AssistTask(Instance, mate, location, bot);
                //assign new task to mate
                mate.AssignTask(task);
               
            }
        }
        #endregion
    }
}
