using System;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace CollisionBall
{
    public class AcceptStopSkillArgs : GameEventArgs
    {
        public static readonly int EventId = 100006;

        public override int Id
        {
            get
            {
                return EventId;
            }
        }
        public AcceptStopSkillArgs()
        {

        }

        public override void Clear()
        {
            return;
        }
    }
}
