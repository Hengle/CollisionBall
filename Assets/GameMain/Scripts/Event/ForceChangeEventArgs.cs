using System;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace CollisionBall
{
    public class ForceChangeEventArgs : GameEventArgs
    {
        public static readonly int EventId = 100001;

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public float Force
        {
            get;
            private set;
        }

        public override void Clear()
        {
            Force = 0;
        }

        public ForceChangeEventArgs(float force)
        {
            Force = force;
        }
    }
}
