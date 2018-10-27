using System;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace CollisionBall
{
    public class GoSkillEventArgs : GameEventArgs
    {
        public static readonly int EventId = 100004;

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public GoSkillEventArgs()
        {

        }

        public override void Clear()
        {
            return;
        }
    }
}
