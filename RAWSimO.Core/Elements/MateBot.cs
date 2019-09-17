using RAWSimO.Core.Control;
using RAWSimO.Core.Bots;
using RAWSimO.Core.Info;
using RAWSimO.Core.Items;
using RAWSimO.Core.Management;
using RAWSimO.Core.Waypoints;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RAWSimO.Core.Elements
{
    /// <summary>
    /// class representing robot's pal
    /// </summary>
    public class MateBot : BotNormal 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MateBot"/> class.
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
        public MateBot(int id, Instance instance, double radius, double acceleration, double deceleration, double maxVelocity, double turnSpeed, double collisionPenaltyTime, double x = 0.0, double y = 0.0)
            : base(id, instance, radius, 99999, acceleration, deceleration, maxVelocity, turnSpeed, collisionPenaltyTime, x, y)
        {

        }

        /// <summary>
        /// Gets BotType
        /// </summary>
        public override BotType Type => BotType.MateBot;

    }
}
