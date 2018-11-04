using System;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace CollisionBall
{
    public class AcceptGoSkillArgs : GameEventArgs
    {
        public static readonly int EventId = 100007;

        public override int Id
        {
            get
            {
                return EventId;
            }
        }
        public AcceptGoSkillArgs()
        {

        }

        public override void Clear()
        {
            return;
        }
    }
}
