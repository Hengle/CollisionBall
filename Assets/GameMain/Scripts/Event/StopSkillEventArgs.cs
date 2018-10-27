using System;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace CollisionBall
{
    public class StopSkillEventArgs : GameEventArgs
    {
        public static readonly int EventId = 100003;

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public StopSkillEventArgs()
        {
            
        }

        public override void Clear()
        {
            return;
        }
    }
}
