using System;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace CollisionBall
{
    public class ReleaseBuffSkillEventArgs : GameEventArgs
    {
        public static readonly int EventId = 100010;

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public int BuffType
        {
            get;
            set;
        }


        public ReleaseBuffSkillEventArgs(int type)
        {
            BuffType = type;
        }

        public override void Clear()
        {
            return;
        }
    }
}
